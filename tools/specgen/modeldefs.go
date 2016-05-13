package main

import (
	"github.com/docker/engine-api/types"
	"github.com/docker/engine-api/types/container"
	"github.com/docker/engine-api/types/filters"
	"github.com/docker/engine-api/types/network"
	"github.com/docker/go-units"
)

type AuthConfigParameters types.AuthConfig

// POST /build
type ImageBuildParameters struct {
	Tags           []string          `rest:"query,t"`
	SuppressOutput bool              `rest:"query,q"`
	RemoteContext  string            `rest:"query,remote"`
	NoCache        bool              `rest:"query"`
	Remove         bool              `rest:"query,rm"`
	ForceRemove    bool              `rest:"query,forcerm"`
	PullParent     bool              `rest:"query"`
	Isolation      string            `rest:"query"`
	CPUSetCPUs     string            `rest:"query"`
	CPUSetMems     string            `rest:"query"`
	CPUShares      int64             `rest:"query"`
	CPUQuota       int64             `rest:"query"`
	CPUPeriod      int64             `rest:"query"`
	Memory         int64             `rest:"query"`
	MemorySwap     int64             `rest:"query,memswap"`
	CgroupParent   string            `rest:"query"`
	ShmSize        int64             `rest:"query"`
	Dockerfile     string            `rest:"query"`
	Ulimits        []*units.Ulimit   `rest:"query"`
	BuildArgs      map[string]string `rest:"query"`
	AuthConfigs    map[string]types.AuthConfig
	Labels         map[string]string `rest:"query"`
}

// POST /commit
type ContainerCommitParamters struct {
	ContainerID    string   `rest:"query,container,required"`
	RepositoryName string   `rest:"query,repo"`
	Tag            string   `rest:"query"`
	Comment        string   `rest:"query"`
	Author         string   `rest:"query"`
	Changes        []string `rest:"query"`
	Pause          bool     `rest:"query"`
	Config         *container.Config
}

// POST /containers/create
type ContainerCreateParameters struct {
	Name              string `rest:"query,name"`
	*container.Config `rest:"body"`
	HostConfig        *container.HostConfig     `rest:"body"`
	NetworkingConfig  *network.NetworkingConfig `rest:"body"`
}

// GET /containers/json
type ContainerListParameters struct {
	Size   bool         `rest:"query"`
	All    bool         `rest:"query"`
	Since  string       `rest:"query"`
	Before string       `rest:"query"`
	Limit  int          `rest:"query"`
	Filter filters.Args `rest:"query"`
}

// DELETE /containers/(id)
type ContainerRemoveParameters struct {
	RemoveVolumes bool `rest:"query,v"`
	RemoveLinks   bool `rest:"query,link"`
	Force         bool `rest:"query"`
}

// GET /containers/(id)/archive
type ContainerPathStatParameters struct {
	Path                      string `rest:"query,path,required"`
	AllowOverwriteDirWithFile bool   `rest:"query,noOverwriteDirNonDir"`
}

// POST /containers/(id)/attach
type ContainerAttachParameters struct {
	Stream     bool   `rest:"query"`
	Stdin      bool   `rest:"query"`
	Stdout     bool   `rest:"query"`
	Stderr     bool   `rest:"query"`
	DetachKeys string `rest:"query,detachKeys"`
}

// GET /containers/(id)/json
type ContainerInspectParameters struct {
	IncludeSize bool `rest:"query,size"`
}

// POST /containers/(id)/kill
type ContainerKillParameters struct {
	Signal string `rest:"query"`
}

// POST /containers/(id)/logs
type ContainerLogsParameters struct {
	ShowStdout bool   `rest:"query,stdout"`
	ShowStderr bool   `rest:"query,stderr"`
	Since      string `rest:"query"`
	Timestamps bool   `rest:"query"`
	Follow     bool   `rest:"query"`
	Tail       string `rest:"query"`
}

// POST /containers/(id)/rename
type ContainerRenameParameters struct {
	NewName string `rest:"query,name"`
}

// POST /containers/(id)/resize
type ContainerResizeParameters struct {
	Height int `rest:"query,h"`
	Width  int `rest:"query,w"`
}

// POST /containers/(id)/restart
type ContainerRestartParameters struct {
	WaitBeforeKillSeconds uint32 `rest:"query,t"`
}

// POST /containers/(id)/start
type ContainerStartParameters struct {
	DetachKeys string `rest:"query,detachKeys"`
}

// POST /containers/(id)/stop
type ContainerStopParameters struct {
	WaitBeforeKillSeconds uint32 `rest:"query,t"`
}

// GET /containers/(id)/stats
type ContainerStatsParameters struct {
	Stream bool `rest:"query"`
}

// GET /containers/(id)/top
type ContainerListProcessesParameters struct {
	PsArgs string `rest:"query,ps_args"`
}

// POST /containers/(id)/update
type ContainerUpdateParameters struct {
	container.UpdateConfig
}

// GET /events
type ContainerEventsParameters struct {
	Since   string       `rest:"query"`
	Until   string       `rest:"query"`
	Filters filters.Args `rest:"query"`
}

// POST /images/create
type ImageCreateParameters struct {
	Parent       string `rest:"query,fromImage,required"`
	Tag          string `rest:"query"`
	RegistryAuth string `rest:"headers,X-Registry-Auth"`
}

type ImageImportParameters struct {
	SourceName     string   `rest:"query,fromSrc,required"`
	RepositoryName string   `rest:"query,repo"`
	Message        string   `rest:"query"`
	Tag            string   `rest:"query"`
	Changes        []string `rest:"query"`
}

type ImagePullParameters struct {
	Parent       string `rest:"query,fromImage,required"`
	Tag          string `rest:"query"`
	RegistryAuth string `rest:"headers,X-Registry-Auth"`
}

// GET /images/json
type ImageListParameters struct {
	MatchName string       `rest:"query,filter"`
	All       bool         `rest:"query"`
	Filters   filters.Args `rest:"query"`
}

// POST /images/load
type ImageLoadParameters struct {
	Quiet bool `rest:"query"`
}

// GET /images/search
type ImageSearchParameters struct {
	Term         string `rest:"query"`
	RegistryAuth string `rest:"headers,X-Registry-Auth"`
}

// DELETE /images/(id)
type ImageDeleteParameters struct {
	Force         bool `rest:"query"`
	PruneChildren bool `rest:"query,noprune"`
}

// GET /images/(id)/json
type ImageInspectParameters struct {
	IncludeSize bool `rest:"query,size"`
}

// POST /images/(id)/push
type ImagePushParameters struct {
	ImageID      string `rest:"query,fromImage"`
	Tag          string `rest:"query"`
	RegistryAuth string `rest:"headers,X-Registry-Auth"`
}

// POST /images/(id)/tag
type ImageTagParameters struct {
	RepositoryName string `rest:"query,repo"`
	Tag            string `rest:"query"`
	Force          bool   `rest:"query"`
}

// GET /networks
type NetworkListParameters struct {
	Filters filters.Args `rest:"query"`
}

// GET /volumes
type VolumesListParameters struct {
	Filters filters.Args `rest:"query"`
}

type VolumeResponse types.Volume

// GET /volumes
type VolumesListResponse struct {
	Volumes  []*VolumeResponse
	Warnings []string
}
