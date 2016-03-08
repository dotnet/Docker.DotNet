package main

import (
	"bufio"
	"fmt"
	"io"
	"io/ioutil"
	"os"
	"reflect"
	"strings"
	"time"

	"github.com/docker/engine-api/types"
	"github.com/docker/engine-api/types/container"
	"github.com/docker/engine-api/types/network"
)

var emptyStruct = reflect.TypeOf(struct{}{})

type csTypeDef struct {
	Name     string
	NeedsOpt bool
}

var specialTypes = map[reflect.Type]csTypeDef{
	reflect.TypeOf(time.Time{}): {"DateTime", false},
	emptyStruct:                 {"BUG_IN_CONVERSION", false},
}

var kindMap = map[reflect.Kind]csTypeDef{
	reflect.Int:   {"int", true},
	reflect.Int8:  {"sbyte", true},
	reflect.Int16: {"short", true},
	reflect.Int32: {"int", true},
	reflect.Int64: {"long", true},

	reflect.Uint:   {"uint", true},
	reflect.Uint8:  {"byte", true},
	reflect.Uint16: {"ushort", true},
	reflect.Uint32: {"uint", true},
	reflect.Uint64: {"ulong", true},

	reflect.String: {"string", false},

	reflect.Bool: {"bool", true},

	reflect.Float32: {"float", true},
	reflect.Float64: {"double", true},
}

type typeDef struct {
    Type reflect.Type
    CsName string
}

type ContainerCreateRequest struct {
	*container.Config
	HostConfig       *container.HostConfig
	NetworkingConfig *network.NetworkingConfig
}

var dockerTypes = []typeDef{
	// POST /containers/create
	{reflect.TypeOf(ContainerCreateRequest{}), "CreateContainerParameters"},
	{reflect.TypeOf(types.ContainerCreateResponse{}), "CreateContainerResponse"},

	// POST /containers/<ID>/exec
    {reflect.TypeOf(types.ExecConfig{}), "ExecCreateContainerConfig"},
	{reflect.TypeOf(types.ContainerExecCreateResponse{}), "ExecCreateContainerResponse"},

	// POST /containers/<ID>/update
	{reflect.TypeOf(types.ContainerUpdateResponse{}), "UpdateContainerResponse"},

	// POST /auth
	{reflect.TypeOf(types.AuthResponse{}), ""},

	// POST /containers/<ID>/wait
	{reflect.TypeOf(types.ContainerWaitResponse{}), "WaitContainerResponse"},

	// POST /containers/<ID>/commit
	{reflect.TypeOf(types.ContainerCommitResponse{}), "CommitContainerChangesResponse"},

	// GET /containers/<ID>/changes
	{reflect.TypeOf(types.ContainerChange{}), "FilesystemChange"},

	// GET /images/<ID>/history
	{reflect.TypeOf(types.ImageHistory{}), "ImageHistoryResponse"},

	// DELETE /images/<ID>
	{reflect.TypeOf(types.ImageDelete{}), "DeleteImageParameters"},

	// GET /images/json
	{reflect.TypeOf(types.Image{}), "ImageListResponse"},

	// GET /images/<ID>/json
	{reflect.TypeOf(types.ImageInspect{}), "ImageResponse"},

	// GET /containers/json
	{reflect.TypeOf(types.Container{}), "ContainerListResponse"},

	// POST /containers/<id>/copy
	{reflect.TypeOf(types.CopyConfig{}), "CopyFromContainerParameters"},

	// GET /containers/<ID>/archive
	{reflect.TypeOf(types.ContainerPathStat{}), ""},

	// GET /containers/<ID>/top
	{reflect.TypeOf(types.ContainerProcessList{}), "ContainerProcessesResponse"},

	// GET /version
	{reflect.TypeOf(types.Version{}), "VersionResponse"},

	// GET /info
	{reflect.TypeOf(types.Info{}), "SystemInfoResponse"},

	// GET /containers/<ID>/json
	{reflect.TypeOf(types.ContainerJSON{}), ""},
	{reflect.TypeOf(types.ContainerJSONBase{}), "ContainerResponse"},

	// GET /volumes
	{reflect.TypeOf(types.VolumesListResponse{}), "VolumeListResponse"},

	// POST /volumes/create
	{reflect.TypeOf(types.VolumeCreateRequest{}), ""},

	// POST ??? networking
	{reflect.TypeOf(types.NetworkCreate{}), "NetworkCreateRequest"},
	{reflect.TypeOf(types.NetworkCreateResponse{}), ""},
	{reflect.TypeOf(types.NetworkConnect{}), "NetworkConnectRequest"},
	{reflect.TypeOf(types.NetworkDisconnect{}), "NetworkDisconnectRequest"},
}

func csType(t reflect.Type, opt bool) string {
	def, ok := specialTypes[t]
	if !ok {
		def, ok = kindMap[t.Kind()]
	}
	if ok {
		if opt && def.NeedsOpt {
			return def.Name + "?"
		}
		return def.Name
	}

	switch t.Kind() {
	case reflect.Array:
		return fmt.Sprintf("%s[]", csType(t.Elem(), false))
	case reflect.Slice:
		return fmt.Sprintf("IList<%s>", csType(t.Elem(), false))
	case reflect.Map:
		if t.Elem() == emptyStruct {
			return fmt.Sprintf("ISet<%s>", csType(t.Key(), false))
		}
		return fmt.Sprintf("IDictionary<%s, %s>", csType(t.Key(), false), csType(t.Elem(), false))
	case reflect.Ptr:
		return csType(t.Elem(), true)
	case reflect.Struct:
		return t.Name()
	default:
		panic(fmt.Errorf("cannot convert type %s", t))
	}
}

func ultimateType(t reflect.Type) reflect.Type {
	for {
		switch t.Kind() {
		case reflect.Array, reflect.Chan, reflect.Map, reflect.Ptr, reflect.Slice:
			t = t.Elem()
		default:
			return t
		}
	}
}

func writeType(t reflect.Type, name string, w io.Writer, serialized map[reflect.Type]int) []reflect.Type {
	var deps []reflect.Type
	fmt.Fprintln(w, "using System.Collections.Generic;")
	fmt.Fprintln(w, "using System.Runtime.Serialization;")
	fmt.Fprintln(w, "")
	fmt.Fprintln(w, "namespace Docker.DotNet.Models")
	fmt.Fprintln(w, "{")
	fmt.Fprintln(w, "    [DataContract]")
	fmt.Fprintf(w, "    public class %s // (%s)\n", name, t)
	fmt.Fprintln(w, "    {")

	n := 0
	for i := 0; i < t.NumField(); i++ {
		f := t.Field(i)

		jsonTag := strings.Split(f.Tag.Get("json"), ",")
		if jsonTag[0] == "-" {
			continue
		}

		jsonName := f.Name
		if jsonTag[0] != "" {
			jsonName = jsonTag[0]
		}

		if n != 0 {
			fmt.Fprintln(w, "")
		}
		n++

		if ft := ultimateType(f.Type); ft.Kind() == reflect.Struct {
			if _, ok := specialTypes[ft]; !ok {
				if _, ok := serialized[ft]; !ok {
					deps = append(deps, ft)
					serialized[ft] = 1
				}
			}
		}

		ft := csType(f.Type, false)

		fmt.Fprintf(w, "        [DataMember(Name = \"%s\")]\n", jsonName)
		fmt.Fprintf(w, "        public %s %s { get; set; }\n", ft, f.Name)
	}

	fmt.Fprintln(w, "    }")
	fmt.Fprintln(w, "}")
	return deps
}

func main() {
	allTypes := dockerTypes
	serialized := make(map[reflect.Type]int)

	for _, t := range allTypes {
		serialized[t.Type] = 1
	}
	for len(allTypes) > 0 {
		t := allTypes[len(allTypes)-1]
		allTypes = allTypes[:len(allTypes)-1]
		f, err := ioutil.TempFile("", "ser")
		if err != nil {
			panic(err)
		}
		defer f.Close()
		b := bufio.NewWriter(f)
        name := t.CsName
        if name == "" {
            name = t.Type.Name()
        }
        for _, d := range writeType(t.Type, name, b, serialized) {
            allTypes = append(allTypes, typeDef{d, ""})
        }
		err = b.Flush()
		if err != nil {
			os.Remove(f.Name())
			panic(err)
		}
		f.Close()
		os.Rename(f.Name(), name+".cs")
	}
}
