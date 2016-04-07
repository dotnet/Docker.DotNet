package main

import (
	"fmt"
	"io"
	"reflect"
	"sort"
	"time"
)

var EmptyStruct = reflect.TypeOf(struct{}{})

// CSInboxTypesMap The Go type kind to C# type map.
var CSInboxTypesMap = map[reflect.Kind]CSType{
	reflect.Int:   {"", "int", true},
	reflect.Int8:  {"", "sbyte", true},
	reflect.Int16: {"", "short", true},
	reflect.Int32: {"", "int", true},
	reflect.Int64: {"", "long", true},

	reflect.Uint:   {"", "uint", true},
	reflect.Uint8:  {"", "byte", true},
	reflect.Uint16: {"", "ushort", true},
	reflect.Uint32: {"", "uint", true},
	reflect.Uint64: {"", "ulong", true},

	reflect.String: {"", "string", false},

	reflect.Bool: {"", "bool", true},

	reflect.Float32: {"", "float", true},
	reflect.Float64: {"", "double", true},
}

// CSCustomTypeMap The Go type to C# type map.
var CSCustomTypeMap = map[reflect.Type]CSType{
	reflect.TypeOf(time.Time{}): {"System", "DateTime", true},
	EmptyStruct:                 {"", "BUG_IN_CONVERSION", false},
}

type CSArgument struct {
	Value string
	Type  CSType
}

func (a CSArgument) toString() string {
	if a.Type.Name == "string" {
		return fmt.Sprintf("\"%s\"", a.Value)
	}

	return a.Value
}

type CSNamedArgument struct {
	Name     string
	Argument CSArgument
}

func (a CSNamedArgument) toString() string {
	return fmt.Sprintf("%s = %s", a.Name, a.Argument.toString())
}

// CSAttribute A representation of a C# attribute.
type CSAttribute struct {
	Type           CSType
	Arguments      []CSArgument
	NamedArguments []CSNamedArgument
}

// NewAttribute Creates a new C# attribute with valid slices.
func NewAttribute(t CSType) *CSAttribute {
	a := CSAttribute{Type: t}
	a.Arguments = make([]CSArgument, 0)
	a.NamedArguments = make([]CSNamedArgument, 0)

	return &a
}

func (a *CSAttribute) toString() string {
	s := fmt.Sprintf("[%s", a.Type.Name)

	lenA := len(a.Arguments)
	lenN := len(a.NamedArguments)
	hasArgs := lenA > 0 || lenN > 0
	if hasArgs {
		s += "("
	}

	for i, a := range a.Arguments {
		s += a.toString()

		if i != lenA-1 {
			s += ", "
		}
	}

	for i, n := range a.NamedArguments {
		s += n.toString()

		if i != lenN-1 {
			s += ", "
		}
	}

	if hasArgs {
		s += ")"
	}

	return s + "]"
}

// CSType A representation of a C# type.
type CSType struct {
	Namespace  string
	Name       string
	IsNullable bool
}

// CSParameter A representation of a C# parameter to a function/constructor.
type CSParameter struct {
	Type *CSModelType
	Name string
}

func (p CSParameter) toString() string {
	return fmt.Sprintf("%s %s", p.Type.Name, p.Name)
}

// CSConstructor A representation of a C# instance constructor.
type CSConstructor struct {
	Parameters []CSParameter
}

func NewConstructor() CSConstructor {
	c := CSConstructor{}
	c.Parameters = make([]CSParameter, 0)

	return c
}

// CSProperty A representation of a C# property type.
type CSProperty struct {
	Name       string
	Type       CSType
	IsOpt      bool
	Attributes []CSAttribute
}

// NewProperty Creates a new property with a valid attributes slice.
func NewProperty(name string, t CSType) *CSProperty {
	p := CSProperty{Name: name, Type: t, IsOpt: false}
	p.Attributes = make([]CSAttribute, 0)

	return &p
}

// CSModelType A representation of a golang reflected struct that has been transformed to a C# class.
type CSModelType struct {
	Name         string
	SourceName   string
	Constructors []CSConstructor
	Properties   []CSProperty
	Attributes   []CSAttribute
}

// NewModel Creates a new model type with valid slices
func NewModel(name, sourceName string) *CSModelType {
	s := CSModelType{}
	s.Name = name
	s.SourceName = sourceName
	s.Constructors = make([]CSConstructor, 0)
	s.Properties = make([]CSProperty, 0)
	s.Attributes = make([]CSAttribute, 0)
	s.Attributes = append(s.Attributes, *NewAttribute(CSType{"System.Runtime.Serialization", "DataContract", false}))

	return &s
}

// ToFile Writes the specific model type to the io writer given.
func (t *CSModelType) ToFile(w io.Writer) {
	usings := calcUsings(t)
	for _, u := range usings {
		fmt.Fprintf(w, "using %s;\n", u)
	}

	fmt.Fprintln(w, "")

	fmt.Fprintln(w, "namespace Docker.DotNet.Models")
	fmt.Fprintln(w, "{")

	writeClass(w, t)

	fmt.Fprintln(w, "}")
}

func calcUsings(t *CSModelType) []string {
	added := make(map[string]bool)
	var usings []string

	for _, a := range t.Attributes {
		usings = safeAddUsing(a.Type.Namespace, usings, added)
	}

	for _, o := range t.Properties {
		usings = safeAddUsing(o.Type.Namespace, usings, added)
	}

	// TODO: System sort order them.
	sort.Strings(usings)

	return usings
}

func safeAddUsing(using string, usings []string, added map[string]bool) []string {
	if using != "" {
		if _, ok := added[using]; !ok {
			added[using] = true
			return append(usings, using)
		}
	}

	return usings
}

func writeClass(w io.Writer, t *CSModelType) {
	for _, a := range t.Attributes {
		fmt.Fprintf(w, "    %s\n", a.toString())
	}

	fmt.Fprintf(w, "    public class %s // (%s)\n", t.Name, t.SourceName)
	fmt.Fprintln(w, "    {")

	if len(t.Constructors) > 0 {
		writeConstructors(w, t.Name, t.Constructors)

		if len(t.Properties) > 0 {
			fmt.Fprintln(w, "")
		}
	}

	if len(t.Properties) > 0 {
		writeProperties(w, t.Properties)
	}

	fmt.Fprintln(w, "    }")
}

func writeConstructors(w io.Writer, typeName string, constructors []CSConstructor) {
	l := len(constructors)
	for i, c := range constructors {
		fmt.Fprintf(w, "        public %s(", typeName)

		plen := len(c.Parameters)
		for pi, p := range c.Parameters {
			fmt.Fprintf(w, p.toString())

			if pi != plen-1 {
				fmt.Fprint(w, ", ")
			}
		}

		fmt.Fprintf(w, ")\n")
		fmt.Fprintln(w, "        {")

		// If we had parameters we need to handle the copy of the data for the structs.
		if plen > 0 {
			for pi, p := range c.Parameters {
				fmt.Fprintf(w, "            if (%s != null)", p.Name)
				fmt.Fprintln(w, "            {")

				// Assign each of the types.
				for _, elem := range p.Type.Properties {
					fmt.Fprintf(w, "                this.%s = %s.%s;\n", elem.Name, p.Name, elem.Name)
				}

				fmt.Fprintln(w, "            }")

				if pi != plen-1 {
					fmt.Fprintln(w, "")
				}
			}
		}

		fmt.Fprintln(w, "        }")
		if i != l-1 {
			fmt.Fprintln(w, "")
		}
	}
}

func writeProperties(w io.Writer, properties []CSProperty) {
	len := len(properties)
	for i, p := range properties {
		for _, a := range p.Attributes {
			fmt.Fprintf(w, "        %s\n", a.toString())
		}

		if p.Type.IsNullable && p.IsOpt {
			fmt.Fprintf(w, "        public %s? %s { get; set; }\n", p.Type.Name, p.Name)
		} else {
			fmt.Fprintf(w, "        public %s %s { get; set; }\n", p.Type.Name, p.Name)
		}

		if i != len-1 {
			fmt.Fprintln(w, "")
		}
	}
}
