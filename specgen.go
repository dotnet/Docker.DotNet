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
	"github.com/docker/engine-api/types/filters"
	"github.com/docker/engine-api/types/network"
	"github.com/docker/engine-api/types/registry"
)

var emptyStruct = reflect.TypeOf(struct{}{})

type csTypeDef struct {
	Name     string
	NeedsOpt bool
}

var specialTypes = map[reflect.Type]csTypeDef{
	reflect.TypeOf(time.Time{}): {"System.DateTime", false},
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
	Type   reflect.Type
	CsName string
}

type AuthConfigParameters types.AuthConfig

// POST /containers/create
type ContainerCreateOptions struct {
	Name             string                    `rest:"in:query,name:name"`
	Config           *container.Config         `rest:"in:body"`
	HostConfig       *container.HostConfig     `rest:"in:body"`
	NetworkingConfig *network.NetworkingConfig `rest:"in:body"`
}

// GET /containers/(id)/json
type ContainerInspectParameters struct {
	IncludeSize bool `rest:"in:query,name:size"`
}

// POST /containers/(id)/kill
type ContainerKillParameters struct {
	Signal string `rest:"in:query,name:signal`
}

// POST /containers/(id)/rename
type ContainerRenameParameters struct {
	NewName string `rest:"in:query,name:name`
}

// POST /containers/(id)/restart
type ContainerRestartParameters struct {
	WaitBeforeKillSeconds uint32 `rest:"in:query,name:t`
}

// POST /containers/(id)/start
type ContainerStartParameters struct {
	DetachKeys string `rest:"in:query,name:detachKeys`
}

// POST /containers/(id)/stop
type ContainerStopParameters struct {
	WaitBeforeKillSeconds uint32 `rest:"in:query,name:t`
}

// GET /containers/(id)/top
type ContainerListProcessesParameters struct {
	PsArgs string `rest:"in:query,name:ps_args"`
}

// GET /images/(id)/json
type ImageInspectParameters struct {
	IncludeSize bool `rest:"in:query,name:size`
}

// GET /volumes
type VolumesListParameters struct {
	Filters filters.Args `rest:"in:query,name:filters"`
}

type VolumeResponse types.Volume

// GET /volumes
type VolumesListResponse struct {
	Volumes  []*VolumeResponse
	Warnings []string
}

var dockerTypes = []typeDef{

	// POST /auth
	{reflect.TypeOf(AuthConfigParameters{}), "AuthConfigParameters"},
	{reflect.TypeOf(types.AuthResponse{}), "AuthResponse"},

	// POST /build
	{reflect.TypeOf(types.ImageBuildOptions{}), "ImageBuildParameters"},
	{reflect.TypeOf(types.ImageBuildResponse{}), "ImageBuildResponse"},

	// POST /commit
	{reflect.TypeOf(types.ContainerCommitOptions{}), "CommitContainerChangesParameters"},
	{reflect.TypeOf(types.ContainerCommitResponse{}), "CommitContainerChangesResponse"},

	// POST /containers/create
	{reflect.TypeOf(ContainerCreateOptions{}), "CreateContainerParameters"},
	{reflect.TypeOf(types.ContainerCreateResponse{}), "CreateContainerResponse"},

	// GET /containers/json
	{reflect.TypeOf(types.ContainerListOptions{}), "ContainersListParameters"},
	{reflect.TypeOf(types.Container{}), "ContainerListResponse"},

	// DELETE /containers/(id)
	{reflect.TypeOf(types.ContainerRemoveOptions{}), "ContainerRemoveParameters"},

	// GET /containers/(id)/archive
	{reflect.TypeOf(types.CopyToContainerOptions{}), "ContainerPathStatParameters"},
	{reflect.TypeOf(types.ContainerPathStat{}), "ContainerPathStatResponse"},

	// POST /containers/(id)/attach
	{reflect.TypeOf(types.ContainerAttachOptions{}), "ContainerAttachParameters"},

	// POST /containers/(id)/attach/ws

	// GET /containers/(id)/changes
	{reflect.TypeOf(types.ContainerChange{}), "ContainerFileSystemChangeResponse"},

	// OBSOLETE - POST /containers/(id)/copy

	// GET /containers/(id)/export
	// TODO: TAR Stream

	// POST /containers/(id)/exec
	{reflect.TypeOf(types.ExecConfig{}), "ContainerExecCreateParameters"},
	{reflect.TypeOf(types.ContainerExecCreateResponse{}), "ContainerExecCreateResponse"},

	// GET /containers/(id)/json
	{reflect.TypeOf(ContainerInspectParameters{}), "ContainerInspectParameters"},
	{reflect.TypeOf(types.ContainerJSON{}), "ContainerInspectResponse"},

	// POST /containers/(id)/kill
	{reflect.TypeOf(ContainerKillParameters{}), "ContainerKillParameters"},

	// GET /containers/(id)/logs
	{reflect.TypeOf(types.ContainerLogsOptions{}), "ContainerLogsParameters"},

	// POST /containers/(id)/pause

	// POST /containers/(id)/rename
	{reflect.TypeOf(ContainerRenameParameters{}), "ContainerRenameParameters"},

	// POST /containers/(id)/resize
	{reflect.TypeOf(types.ResizeOptions{}), "ContainerResizeParameters"},

	// POST /containers/(id)/restart
	{reflect.TypeOf(ContainerRestartParameters{}), "ConatinerRestartParameters"},

	// POST /containers/(id)/start
	{reflect.TypeOf(ContainerStartParameters{}), "ContainerStartParameters"},

	// POST /containers/(id)/stop
	{reflect.TypeOf(ContainerStopParameters{}), "ContainerStopParameters"},

	// GET /containers/(id)/stats
	{reflect.TypeOf(types.StatsJSON{}), "ContainerStatsResponse"},

	// GET /containers/(id)/top
	{reflect.TypeOf(ContainerListProcessesParameters{}), "ContainerListProcessesParameters"},
	{reflect.TypeOf(types.ContainerProcessList{}), "ContainerProcessesResponse"},

	// POST /containers/(id)/unpause

	// POST /containers/(id)/update
	{reflect.TypeOf(container.UpdateConfig{}), "ContainerUpdateParameters"},
	{reflect.TypeOf(types.ContainerUpdateResponse{}), "ContainerUpdateResponse"},

	// POST /containers/(id)/wait
	{reflect.TypeOf(types.ContainerWaitResponse{}), "ContainerWaitResponse"},

	// GET /events
	{reflect.TypeOf(types.EventsOptions{}), "ContainerEventsParameters"},

	// POST /images/create
	{reflect.TypeOf(types.ImageCreateOptions{}), "ImagesCreateParameters"},
	{reflect.TypeOf(types.ImageImportOptions{}), "ImagesImportParameters"},
	{reflect.TypeOf(types.ImagePullOptions{}), "ImagesPullParameters"},

	// GET /images/get
	// TODO: stream

	// GET /images/json
	{reflect.TypeOf(types.ImageListOptions{}), "ImagesListParameters"},
	{reflect.TypeOf(types.Image{}), "ImagesListResponse"},

	// POST /images/load
	// TODO: quite:bool application/x-tar body.
	{reflect.TypeOf(types.ImageLoadResponse{}), "ImagesLoadResponse"},

	// GET /images/search
	{reflect.TypeOf(types.ImageSearchOptions{}), "ImagesSearchParameters"},
	{reflect.TypeOf(registry.SearchResult{}), "ImageSearchResponse"},

	// DELETE /images/(id)
	{reflect.TypeOf(types.ImageRemoveOptions{}), "ImageDeleteParameters"},
	{reflect.TypeOf(types.ImageDelete{}), "ImageDeleteResponse"},

	// GET /images/(id)/history
	{reflect.TypeOf(types.ImageHistory{}), "ImageHistoryResponse"},

	// GET /images/(id)/json
	{reflect.TypeOf(ImageInspectParameters{}), "ImageInspectParameters"},
	{reflect.TypeOf(types.ImageInspect{}), "ImageInspectResponse"},

	// POST /images/(id)/push
	{reflect.TypeOf(types.ImagePushOptions{}), "ImagePushParameters"},

	// POST /images/(id)/tag
	{reflect.TypeOf(types.ImageTagOptions{}), "ImageTagParameters"},

	// GET /info
	{reflect.TypeOf(types.Info{}), "SystemInfoResponse"},

	// GET /networks
	{reflect.TypeOf(types.NetworkListOptions{}), "NetworksListParameters"},
	{reflect.TypeOf(types.NetworkResource{}), "NetworkListResponse"},

	// POST /networks/create
	{reflect.TypeOf(types.NetworkCreate{}), "NetworksCreateParameters"},
	{reflect.TypeOf(types.NetworkCreateResponse{}), "NetworksCreateResponse"},

	// GET /networks/(id)
	{reflect.TypeOf(types.NetworkResource{}), "NetworkResponse"},

	// DELETE /networks/(id)

	// POST /networks/(id)/connect
	{reflect.TypeOf(types.NetworkConnect{}), "NetworkConnectParameters"},

	// POST /networks/(id)/disconnect
	{reflect.TypeOf(types.NetworkDisconnect{}), "NetworkDisconnectParameters"},

	// GET /version
	{reflect.TypeOf(types.Version{}), "VersionResponse"},

	// GET /volumes
	{reflect.TypeOf(VolumesListParameters{}), "VolumesListParameters"},
	{reflect.TypeOf(VolumesListResponse{}), "VolumesListResponse"},

	// POST /volumes/create
	{reflect.TypeOf(types.VolumeCreateRequest{}), "VolumesCreateParameters"},

	// GET /volumes/(id)
	{reflect.TypeOf(VolumeResponse{}), "VolumeResponse"},

	// DELETE /volumes/(id)
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
	case reflect.Interface:
		return "object"
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

func writeTypeFields(t reflect.Type, w io.Writer, serialized map[reflect.Type]int, deps []reflect.Type) []reflect.Type {
	n := 0
	for i := 0; i < t.NumField(); i++ {
		f := t.Field(i)

		if f.Type.Kind() == reflect.Struct && f.Type.Name() == "" {
			// TODO: Inline struct definitions. Probably need to write an inline class named the property name?
			continue
		}

		// If the type is anonymous we need to inline its values to this struct.
		if f.Anonymous {
			deps = writeTypeFields(ultimateType(f.Type), w, serialized, deps)
		} else {
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
			if ft == "" {
				ft = f.Name
			}

			if restTag, err := RestTagFromString(f.Tag.Get("rest")); err == nil && restTag.In != Body {

				fmt.Fprintf(w, "        [QueryStringParameter(\"%s\", %t", restTag.Name, restTag.Required)
				switch f.Type.Kind() {
				case reflect.Bool:
					fmt.Fprint(w, ", typeof(BoolQueryStringConverter)")
				}

				fmt.Fprint(w, ")]\n")

				if restTag.Required {
					fmt.Fprintf(w, "        public %s %s { get; set; }\n", ft, f.Name)
				} else {
					if def, ok := kindMap[f.Type.Kind()]; ok && def.NeedsOpt {
						// Struct types in c# require ? for nullable types.
						fmt.Fprintf(w, "        public %s? %s { get; set; }\n", ft, f.Name)
					} else {
						fmt.Fprintf(w, "        public %s %s { get; set; }\n", ft, f.Name)
					}
				}
			} else {
				fmt.Fprintf(w, "        [DataMember(Name = \"%s\")]\n", jsonName)
				fmt.Fprintf(w, "        public %s %s { get; set; }\n", ft, f.Name)
			}
		}

	}

	return deps
}

func writeType(t reflect.Type, name string, w io.Writer, serialized map[reflect.Type]int) []reflect.Type {

	fmt.Fprintln(w, "using System.Collections.Generic;")
	fmt.Fprintln(w, "using System.Runtime.Serialization;")
	fmt.Fprintln(w, "")
	fmt.Fprintln(w, "namespace Docker.DotNet.Models")
	fmt.Fprintln(w, "{")
	fmt.Fprintln(w, "    [DataContract]")
	fmt.Fprintf(w, "    public class %s // (%s)\n", name, t)
	fmt.Fprintln(w, "    {")

	var deps = []reflect.Type{}
	deps = writeTypeFields(t, w, serialized, deps)

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

		source, err := os.Open(f.Name())
		if err != nil {
			os.Remove(f.Name())
			panic(err)
		}

		dest, err := os.Create("D:\\git\\Microsoft\\hyperv\\DockerPS\\src\\Docker.DotNet\\Docker.DotNet\\Models\\" + name + ".Generated.cs")

		if err != nil {
			os.Remove(f.Name())
			panic(err)
		}

		if w, err := io.Copy(dest, source); err != nil {
			os.Remove(f.Name())
			source.Close()
			os.Remove(source.Name())
			dest.Close()
			os.Remove(dest.Name())
			fmt.Println(w)
			panic(err)
		}
	}
}
