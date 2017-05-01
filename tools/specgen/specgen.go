package main

import (
	"bufio"
	"fmt"
	"io/ioutil"
	"os"
	"path"
	"reflect"
	"strconv"
	"strings"

	"github.com/docker/docker/api/types"
	"github.com/docker/docker/api/types/container"
	"github.com/docker/docker/api/types/registry"
	"github.com/docker/docker/api/types/swarm"
	"github.com/docker/docker/pkg/jsonmessage"
)

var typeCustomizations = map[typeCustomizationKey]CSType{
	{reflect.TypeOf(container.RestartPolicy{}), "Name"}:    {"", "RestartPolicyKind", false},
	{reflect.TypeOf(jsonmessage.JSONMessage{}), "Time"}:    {"System", "DateTime", false},
	{reflect.TypeOf(types.Container{}), "Created"}:         {"System", "DateTime", false},
	{reflect.TypeOf(types.ContainerChange{}), "Kind"}:      {"", "FileSystemChangeKind", false},
	{reflect.TypeOf(types.ContainerJSONBase{}), "Created"}: {"System", "DateTime", false},
	{reflect.TypeOf(types.ImageSummary{}), "Created"}:      {"System", "DateTime", false},
	{reflect.TypeOf(types.ImageHistory{}), "Created"}:      {"System", "DateTime", false},
	{reflect.TypeOf(types.ImageInspect{}), "Created"}:      {"System", "DateTime", false},
}

type typeCustomizationKey struct {
	Type         reflect.Type
	PropertyName string
}

type typeDef struct {
	Type   reflect.Type
	CsName string
}

var dockerTypesToReflect = []typeDef{

	// POST /auth
	{reflect.TypeOf(types.AuthConfig{}), "AuthConfig"},
	{reflect.TypeOf(registry.AuthenticateOKBody{}), "AuthResponse"},

	// POST /build
	{reflect.TypeOf(ImageBuildParameters{}), "ImageBuildParameters"},
	{reflect.TypeOf(types.ImageBuildResponse{}), "ImageBuildResponse"},

	// POST /commit
	{reflect.TypeOf(ContainerCommitParamters{}), "CommitContainerChangesParameters"},
	{reflect.TypeOf(types.IDResponse{}), "CommitContainerChangesResponse"},

	// POST /containers/create
	{reflect.TypeOf(ContainerCreateParameters{}), "CreateContainerParameters"},
	{reflect.TypeOf(container.ContainerCreateCreatedBody{}), "CreateContainerResponse"},

	// GET /containers/json
	{reflect.TypeOf(ContainerListParameters{}), "ContainersListParameters"},
	{reflect.TypeOf(types.Container{}), "ContainerListResponse"},

	// DELETE /containers/(id)
	{reflect.TypeOf(ContainerRemoveParameters{}), "ContainerRemoveParameters"},

	// GET /containers/(id)/archive
	{reflect.TypeOf(ContainerPathStatParameters{}), "ContainerPathStatParameters"},
	{reflect.TypeOf(types.ContainerPathStat{}), "ContainerPathStatResponse"},

	// POST /containers/(id)/attach
	{reflect.TypeOf(ContainerAttachParameters{}), "ContainerAttachParameters"},

	// POST /containers/(id)/attach/ws

	// GET /containers/(id)/changes
	{reflect.TypeOf(types.ContainerChange{}), "ContainerFileSystemChangeResponse"},

	// OBSOLETE - POST /containers/(id)/copy

	// GET /containers/(id)/export
	// TODO: TAR Stream

	// POST /containers/(id)/exec
	{reflect.TypeOf(ExecCreateParameters{}), "ContainerExecCreateParameters"},
	{reflect.TypeOf(types.IDResponse{}), "ContainerExecCreateResponse"},

	// GET /containers/(id)/json
	{reflect.TypeOf(ContainerInspectParameters{}), "ContainerInspectParameters"},
	{reflect.TypeOf(types.ContainerJSON{}), "ContainerInspectResponse"},

	// POST /containers/(id)/kill
	{reflect.TypeOf(ContainerKillParameters{}), "ContainerKillParameters"},

	// GET /containers/(id)/logs
	{reflect.TypeOf(ContainerLogsParameters{}), "ContainerLogsParameters"},

	// POST /containers/(id)/pause

	// POST /containers/(id)/rename
	{reflect.TypeOf(ContainerRenameParameters{}), "ContainerRenameParameters"},

	// POST /containers/(id)/resize
	// POST /exec/(id)/resize
	{reflect.TypeOf(ContainerResizeParameters{}), "ContainerResizeParameters"},

	// POST /containers/(id)/restart
	{reflect.TypeOf(ContainerRestartParameters{}), "ConatinerRestartParameters"},

	// POST /containers/(id)/start
	{reflect.TypeOf(ContainerStartParameters{}), "ContainerStartParameters"},

	// POST /containers/(id)/stop
	{reflect.TypeOf(ContainerStopParameters{}), "ContainerStopParameters"},

	// GET /containers/(id)/stats
	{reflect.TypeOf(ContainerStatsParameters{}), "ContainerStatsParameters"},
	{reflect.TypeOf(types.StatsJSON{}), "ContainerStatsResponse"},

	// GET /containers/(id)/top
	{reflect.TypeOf(ContainerListProcessesParameters{}), "ContainerListProcessesParameters"},
	{reflect.TypeOf(types.ContainerProcessList{}), "ContainerProcessesResponse"},

	// POST /containers/(id)/unpause

	// POST /containers/(id)/update
	{reflect.TypeOf(ContainerUpdateParameters{}), "ContainerUpdateParameters"},
	{reflect.TypeOf(ContainerUpdateResponse{}), "ContainerUpdateResponse"},

	// POST /containers/(id)/wait
	{reflect.TypeOf(ContainerWaitResponse{}), "ContainerWaitResponse"},

	// POST /exec/(id)/start
	{reflect.TypeOf(ExecStartParameters{}), "ContainerExecStartParameters"},

	// GET /exec/(id)/json
	{reflect.TypeOf(types.ContainerExecInspect{}), "ContainerExecInspectResponse"},

	// GET /events
	{reflect.TypeOf(ContainerEventsParameters{}), "ContainerEventsParameters"},
	{reflect.TypeOf(jsonmessage.JSONMessage{}), "JSONMessage"},

	// POST /images/create
	{reflect.TypeOf(ImageCreateParameters{}), "ImagesCreateParameters"},

	// GET /images/get
	// TODO: stream

	// GET /images/json
	{reflect.TypeOf(ImageListParameters{}), "ImagesListParameters"},
	{reflect.TypeOf(types.ImageSummary{}), "ImagesListResponse"},

	// POST /images/load
	// TODO: headers: application/x-tar body.
	{reflect.TypeOf(ImageLoadParameters{}), "ImageLoadParameters"},
	{reflect.TypeOf(types.ImageLoadResponse{}), "ImagesLoadResponse"},

	// GET /images/search
	{reflect.TypeOf(ImageSearchParameters{}), "ImagesSearchParameters"},
	{reflect.TypeOf(registry.SearchResult{}), "ImageSearchResponse"},

	// DELETE /images/(id)
	{reflect.TypeOf(ImageDeleteParameters{}), "ImageDeleteParameters"},
	{reflect.TypeOf(types.ImageDelete{}), "ImageDeleteResponse"},

	// GET /images/(id)/history
	{reflect.TypeOf(types.ImageHistory{}), "ImageHistoryResponse"},

	// GET /images/(id)/json
	{reflect.TypeOf(ImageInspectParameters{}), "ImageInspectParameters"},
	{reflect.TypeOf(types.ImageInspect{}), "ImageInspectResponse"},

	// POST /images/(id)/push
	{reflect.TypeOf(ImagePushParameters{}), "ImagePushParameters"},

	// POST /images/(id)/tag
	{reflect.TypeOf(ImageTagParameters{}), "ImageTagParameters"},

	// GET /info
	{reflect.TypeOf(types.Info{}), "SystemInfoResponse"},

	// GET /networks
	{reflect.TypeOf(NetworkListParameters{}), "NetworksListParameters"},
	{reflect.TypeOf(types.NetworkResource{}), "NetworkListResponse"},

	// POST /networks/create
	{reflect.TypeOf(types.NetworkCreateRequest{}), "NetworksCreateParameters"},
	{reflect.TypeOf(types.NetworkCreateResponse{}), "NetworksCreateResponse"},

	// POST /networks/prune
	{reflect.TypeOf(NetworksDeleteUnusedParameters{}), "NetworksDeleteUnusedParameters"},

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
	{reflect.TypeOf(VolumeCreateRequest{}), "VolumesCreateParameters"},

	// GET /volumes/(id)
	{reflect.TypeOf(VolumeResponse{}), "VolumeResponse"},

	// DELETE /volumes/(id)

	//
	// Swarm API
	//

	// POST /swarm/init
	{reflect.TypeOf(swarm.InitRequest{}), "SwarmInitParameters"},

	// POST /swarm/join
	{reflect.TypeOf(swarm.JoinRequest{}), "SwarmJoinParameters"},

	// POST /swarm/leave
	{reflect.TypeOf(SwarmLeaveParameters{}), "SwarmLeaveParameters"},

	// GET /swarm

	// GET /swarm/unlockkey
	{reflect.TypeOf(swarm.UnlockRequest{}), "SwarmUnlockResponse"},

	// POST /swarm/update
	{reflect.TypeOf(SwarmUpdateParameters{}), "SwarmUpdateParameters"},

	// POST /swarm/unlock
	{reflect.TypeOf(swarm.UnlockRequest{}), "SwarmUnlockParameters"},

	// GET /services
	// {reflect.TypeOf([]swarm.Service{}), "SwarmServices"},

	// GET /services/(id)
	{reflect.TypeOf(swarm.Service{}), "SwarmService"},

	// POST /services/create
	{reflect.TypeOf(ServiceCreateParameters{}), "ServiceCreateParameters"},
	{reflect.TypeOf(types.ServiceCreateResponse{}), "ServiceCreateResponse"},

	// POST /services/(id)/update
	{reflect.TypeOf(ServiceUpdateParameters{}), "ServiceUpdateParameters"},
	{reflect.TypeOf(types.ServiceUpdateResponse{}), "ServiceUpdateResponse"},

	// DELETE /services/(id)
}

func csType(t reflect.Type, opt bool) CSType {
	def, ok := CSCustomTypeMap[t]
	if !ok {
		def, ok = CSInboxTypesMap[t.Kind()]
	}

	if ok {
		return def
	}

	switch t.Kind() {
	case reflect.Array:
		return CSType{"", fmt.Sprintf("%s[]", csType(t.Elem(), false).Name), false}
	case reflect.Slice:
		return CSType{"System.Collections.Generic", fmt.Sprintf("IList<%s>", csType(t.Elem(), false).Name), false}
	case reflect.Map:
		if t.Elem() == EmptyStruct {
			return CSType{"System.Collections.Generic", fmt.Sprintf("IDictionary<%s, EmptyStruct>", csType(t.Key(), false).Name), false}
		}

		return CSType{"System.Collections.Generic", fmt.Sprintf("IDictionary<%s, %s>", csType(t.Key(), false).Name, csType(t.Elem(), false).Name), false}
	case reflect.Ptr:
		return csType(t.Elem(), true)
	case reflect.Struct:
		return CSType{"", t.Name(), false}
	case reflect.Interface:
		return CSType{"", "object", false}
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

func reflectTypeMembers(t reflect.Type, m *CSModelType, reflectedTypes map[string]*CSModelType) {
	for i := 0; i < t.NumField(); i++ {
		f := t.Field(i)

		switch f.Type.Kind() {
		case reflect.Func, reflect.Uintptr:
			continue
		}

		if f.Type.Kind() == reflect.Struct && f.Type.Name() == "" {
			// TODO: Inline struct definitions. Probably need to write an inline class named the property name?
			continue
		}

		// If the type is anonymous we need to inline its values to this model.
		if f.Anonymous {
			clen := len(m.Constructors)
			if clen == 0 {
				// We need to add a default constructor and a custom one since its the first time.
				m.Constructors = append(m.Constructors, CSConstructor{}, CSConstructor{})
			}

			ut := ultimateType(f.Type)
			reflectType(ut.Name(), ut, reflectedTypes)
			newType := reflectedTypes[ut.Name()]
			m.Constructors[1].Parameters = append(m.Constructors[1].Parameters, CSParameter{newType, f.Name})

			// Now we need to add in all of the inherited types parameters
			for _, p := range newType.Properties {
				m.Properties = append(m.Properties, p)
			}
		} else {
			// If we are referencing a struct that isnt inline or anonymous we need to update it too.
			if ut := ultimateType(f.Type); ut.Kind() == reflect.Struct {
				if _, ok := CSInboxTypesMap[f.Type.Kind()]; !ok {
					if _, ok := CSCustomTypeMap[f.Type]; !ok {
						reflectType(ut.Name(), ut, reflectedTypes)
					}
				}
			}

			// If the json tag says to omit we skip generation.
			jsonTag := strings.Split(f.Tag.Get("json"), ",")
			if jsonTag[0] == "-" {
				continue
			}

			// Create our new property.
			csProp := CSProperty{Name: f.Name, Type: csType(f.Type, false)}

			jsonName := f.Name
			if jsonTag[0] != "" {
				jsonName = jsonTag[0]
			}

			if ft, ok := typeCustomizations[typeCustomizationKey{t, f.Name}]; ok {
				// We have a custom modification. Change the type.
				csProp.Type = ft
			}

			if restTag, err := RestTagFromString(f.Tag.Get("rest")); err == nil && restTag.In != body {
				if restTag.Name == "" {
					restTag.Name = strings.ToLower(f.Name)
				}

				a := CSAttribute{Type: CSType{"", "QueryStringParameter", false}}
				a.Arguments = append(
					a.Arguments,
					CSArgument{
						restTag.Name,
						CSInboxTypesMap[reflect.String]},
					CSArgument{strconv.FormatBool(restTag.Required),
						CSInboxTypesMap[reflect.Bool]})

				switch f.Type.Kind() {
				case reflect.Bool:
					a.Arguments = append(a.Arguments, CSArgument{Value: "typeof(BoolQueryStringConverter)"})
				case reflect.Slice, reflect.Array:
					a.Arguments = append(a.Arguments, CSArgument{Value: "typeof(EnumerableQueryStringConverter)"})
				case reflect.Map:
					a.Arguments = append(a.Arguments, CSArgument{Value: "typeof(MapQueryStringConverter)"})
				}

				csProp.IsOpt = !restTag.Required
				csProp.Attributes = append(csProp.Attributes, a)
				csProp.DefaultValue = restTag.Default
			} else {
				a := CSAttribute{Type: CSType{"", "DataMember", false}}
				a.NamedArguments = append(a.NamedArguments, CSNamedArgument{"Name", CSArgument{jsonName, CSInboxTypesMap[reflect.String]}})
				a.NamedArguments = append(a.NamedArguments, CSNamedArgument{"EmitDefaultValue", CSArgument{strconv.FormatBool(false), CSInboxTypesMap[reflect.Bool]}})
				csProp.IsOpt = f.Type.Kind() == reflect.Ptr
				csProp.Attributes = append(csProp.Attributes, a)
			}

			// Lastly assign the property to our type.
			m.Properties = append(m.Properties, csProp)
		}
	}
}

func reflectType(name string, t reflect.Type, reflectedTypes map[string]*CSModelType) {
	if _, ok := reflectedTypes[name]; ok {
		return
	} else if name == "" {
		return
	} else if _, ok := CSCustomTypeMap[t]; ok {
		return
	}

	m := NewModel(name, fmt.Sprintf("%s", t))
	reflectedTypes[name] = m

	reflectTypeMembers(t, m, reflectedTypes)
}

func reflectDockerType(t typeDef, reflectedTypes map[string]*CSModelType) {
	reflectType(t.CsName, t.Type, reflectedTypes)
}

func reflectDockerTypes() map[string]*CSModelType {
	reflectedTypes := make(map[string]*CSModelType)

	for _, t := range dockerTypesToReflect {
		reflectDockerType(t, reflectedTypes)
	}

	return reflectedTypes
}

func main() {
	argsLen := len(os.Args)
	sourcePath := ""
	if argsLen >= 2 {
		sourcePath = os.Args[1]
		fmt.Println(sourcePath)
		if _, err := os.Stat(sourcePath); err != nil {
			if os.IsNotExist(err) {
				panic(sourcePath + ", is not a valid directory.")
			}
		}
	} else {
		sourcePath, _ = os.Getwd()
	}

	// Delete any previously generated files.
	if files, err := ioutil.ReadDir(sourcePath); err != nil {
		panic(err)
	} else {
		for _, file := range files {
			if strings.HasSuffix(file.Name(), ".Generated.cs") {
				if err := os.Remove(path.Join(sourcePath, file.Name())); err != nil {
					panic(err)
				}
			}
		}
	}

	csTypes := reflectDockerTypes()

	for k, v := range csTypes {
		f, err := ioutil.TempFile(sourcePath, "ser")
		if err != nil {
			panic(err)
		}

		defer f.Close()

		b := bufio.NewWriter(f)
		v.Write(b)
		err = b.Flush()
		if err != nil {
			os.Remove(f.Name())
			panic(err)
		}

		f.Close()
		os.Rename(f.Name(), path.Join(sourcePath, k+".Generated.cs"))
	}
}
