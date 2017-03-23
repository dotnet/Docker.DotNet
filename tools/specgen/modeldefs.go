package main

import (
	"github.com/docker/docker/api/types"
	"github.com/docker/docker/api/types/container"
	"github.com/docker/docker/api/types/network"
	"github.com/docker/docker/api/types/swarm"
	"github.com/docker/go-units"
)

// Args map
type Args map[string]map[string]bool

// ImageBuildParameters for POST /build
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

// ContainerCommitParamters for POST /commit
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

// ContainerCreateParameters for POST /containers/create
type ContainerCreateParameters struct {
	Name              string `rest:"query,name"`
	*container.Config `rest:"body"`
	HostConfig        *container.HostConfig     `rest:"body"`
	NetworkingConfig  *network.NetworkingConfig `rest:"body"`
}

// ContainerListParameters for GET /containers/json
type ContainerListParameters struct {
	Size    bool   `rest:"query"`
	All     bool   `rest:"query"`
	Since   string `rest:"query"`
	Before  string `rest:"query"`
	Limit   int    `rest:"query"`
	Filters Args   `rest:"query"`
}

// ContainerRemoveParameters for DELETE /containers/(id)
type ContainerRemoveParameters struct {
	RemoveVolumes bool `rest:"query,v"`
	RemoveLinks   bool `rest:"query,link"`
	Force         bool `rest:"query"`
}

// ContainerPathStatParameters for GET /containers/(id)/archive
type ContainerPathStatParameters struct {
	Path                      string `rest:"query,path,required"`
	AllowOverwriteDirWithFile bool   `rest:"query,noOverwriteDirNonDir"`
}

// ContainerAttachParameters for POST /containers/(id)/attach
type ContainerAttachParameters struct {
	Stream     bool   `rest:"query"`
	Stdin      bool   `rest:"query"`
	Stdout     bool   `rest:"query"`
	Stderr     bool   `rest:"query"`
	DetachKeys string `rest:"query,detachKeys"`
	Logs       string `rest:"query"`
}

// ContainerInspectParameters for GET /containers/(id)/json
type ContainerInspectParameters struct {
	IncludeSize bool `rest:"query,size"`
}

// ContainerKillParameters for POST /containers/(id)/kill
type ContainerKillParameters struct {
	Signal string `rest:"query"`
}

// ContainerLogsParameters for POST /containers/(id)/logs
type ContainerLogsParameters struct {
	ShowStdout bool   `rest:"query,stdout"`
	ShowStderr bool   `rest:"query,stderr"`
	Since      string `rest:"query"`
	Timestamps bool   `rest:"query"`
	Follow     bool   `rest:"query"`
	Tail       string `rest:"query"`
}

// ContainerRenameParameters for POST /containers/(id)/rename
type ContainerRenameParameters struct {
	NewName string `rest:"query,name"`
}

// ContainerResizeParameters for POST /containers/(id)/resize
type ContainerResizeParameters struct {
	Height int `rest:"query,h"`
	Width  int `rest:"query,w"`
}

// ContainerRestartParameters for POST /containers/(id)/restart
type ContainerRestartParameters struct {
	WaitBeforeKillSeconds uint32 `rest:"query,t"`
}

// ContainerStartParameters for POST /containers/(id)/start
type ContainerStartParameters struct {
	DetachKeys string `rest:"query,detachKeys"`
}

// ContainerStopParameters for POST /containers/(id)/stop
type ContainerStopParameters struct {
	WaitBeforeKillSeconds uint32 `rest:"query,t"`
}

// ContainerStatsParameters for GET /containers/(id)/stats
type ContainerStatsParameters struct {
	Stream bool `rest:"query,stream,required,true"`
}

// ContainerListProcessesParameters for GET /containers/(id)/top
type ContainerListProcessesParameters struct {
	PsArgs string `rest:"query,ps_args"`
}

// ContainerUpdateParameters for POST /containers/(id)/update
type ContainerUpdateParameters struct {
	container.UpdateConfig
}

// ContainerUpdateResponse for POST /containers/(id)/update
type ContainerUpdateResponse struct {
	// Warnings are any warnings encountered during the updating of the container.
	Warnings []string `json:"Warnings"`
}

// ContainerWaitResponse for POST /containers/(id)/wait
type ContainerWaitResponse struct {
	// StatusCode is the status code of the wait job
	StatusCode int `json:"StatusCode"`
}

// ContainerEventsParameters for GET /events
type ContainerEventsParameters struct {
	Since   string `rest:"query"`
	Until   string `rest:"query"`
	Filters Args   `rest:"query"`
}

// ExecCreateParameters for POST /containers/(id)/exec
type ExecCreateParameters struct {
	*types.ExecConfig
}

// ExecStartParameters for POST /exec/(id)/start
type ExecStartParameters struct {
	*types.ExecConfig
}

// ImageCreateParameters for POST /images/create
type ImageCreateParameters struct {
	FromImage string `rest:"query,fromImage"`
	FromSrc   string `rest:"query,fromSrc"`
	Repo      string `rest:"query"`
	Tag       string `rest:"query"`
}

// ImageListParameters for GET /images/json
type ImageListParameters struct {
	MatchName string `rest:"query,filter"`
	All       bool   `rest:"query"`
	Filters   Args   `rest:"query"`
}

// ImageLoadParameters for POST /images/load
type ImageLoadParameters struct {
	Quiet bool `rest:"query,quiet,required"`
}

// ImageSearchParameters for GET /images/search
type ImageSearchParameters struct {
	Term         string `rest:"query"`
	RegistryAuth string `rest:"headers,X-Registry-Auth"`
}

// ImageDeleteParameters for DELETE /images/(id)
type ImageDeleteParameters struct {
	Force         bool `rest:"query"`
	PruneChildren bool `rest:"query,noprune"`
}

// ImageInspectParameters for GET /images/(id)/json
type ImageInspectParameters struct {
	IncludeSize bool `rest:"query,size"`
}

// ImagePushParameters for POST /images/(id)/push
type ImagePushParameters struct {
	ImageID      string `rest:"query,fromImage"`
	Tag          string `rest:"query"`
	RegistryAuth string `rest:"headers,X-Registry-Auth"`
}

// ImageTagParameters for POST /images/(id)/tag
type ImageTagParameters struct {
	RepositoryName string `rest:"query,repo"`
	Tag            string `rest:"query"`
	Force          bool   `rest:"query"`
}

// NetworkListParameters for GET /networks
type NetworkListParameters struct {
	Filters Args `rest:"query"`
}

// NetworkDeleteUnusedParameters for POST /networks/prune
type NetworksDeleteUnusedParameters struct {
	Filters Args `rest:"query"`
}

// VolumeCreateRequest for POST /volumes/create
type VolumeCreateRequest struct {
	Name       string            // Name is the requested name of the volume
	Driver     string            // Driver is the name of the driver that should be used to create the volume
	DriverOpts map[string]string // DriverOpts holds the driver specific options to use for when creating the volume.
	Labels     map[string]string // Labels holds metadata specific to the volume being created.
}

// VolumesListParameters for GET /volumes
type VolumesListParameters struct {
	Filters Args `rest:"query"`
}

// VolumeResponse for volume list.
type VolumeResponse types.Volume

// VolumesListResponse for GET /volumes
type VolumesListResponse struct {
	Volumes  []*VolumeResponse
	Warnings []string
}

// SwarmLeaveParameters for POST /swarm/leave
type SwarmLeaveParameters struct {
	Force bool `rest:"body"`
}

// SwarmUpdateParameters for POST /swarm/update
type SwarmUpdateParameters struct {
	Spec                   swarm.Spec `rest:"body,spec,required"`
	Version                int64      `rest:"query,version,required"`
	RotateWorkerToken      bool       `rest:"query"`
	RotateManagerToken     bool       `rest:"query"`
	RotateManagerUnlockKey bool       `rest:"query"`
}

// ServiceCreateParameters for POST /services/create
type ServiceCreateParameters struct {
	Service      swarm.ServiceSpec `rest:"body,service,required"`
	RegistryAuth types.AuthConfig  `rest:"headers,X-Registry-Auth"`
}

// ServiceUpdateParameters for POST /services/{id}/update
type ServiceUpdateParameters struct {
	Service          swarm.ServiceSpec `rest:"body,service,required"`
	Version          int64             `rest:"query,version,required"`
	RegistryAuthFrom string            `rest:"query"`
	RegistryAuth     types.AuthConfig  `rest:"headers,X-Registry-Auth"`
}
