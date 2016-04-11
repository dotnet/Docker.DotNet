package main

import (
	"github.com/docker/engine-api/types"
	"github.com/docker/engine-api/types/container"
	"github.com/docker/engine-api/types/filters"
	"github.com/docker/engine-api/types/network"
)

type AuthConfigParameters types.AuthConfig

// POST /containers/create
type ContainerCreateOptions struct {
	Name              string `rest:"query,name"`
	*container.Config `rest:"body"`
	HostConfig        *container.HostConfig     `rest:"body"`
	NetworkingConfig  *network.NetworkingConfig `rest:"body"`
}

// GET /containers/(id)/json
type ContainerInspectParameters struct {
	IncludeSize bool `rest:"query,size"`
}

// POST /containers/(id)/kill
type ContainerKillParameters struct {
	Signal string `rest:"query,signal"`
}

// POST /containers/(id)/rename
type ContainerRenameParameters struct {
	NewName string `rest:"query,name"`
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

// GET /containers/(id)/top
type ContainerListProcessesParameters struct {
	PsArgs string `rest:"query,ps_args"`
}

// GET /images/(id)/json
type ImageInspectParameters struct {
	IncludeSize bool `rest:"query,size"`
}

// GET /volumes
type VolumesListParameters struct {
	Filters filters.Args `rest:"query,filters"`
}

type VolumeResponse types.Volume

// GET /volumes
type VolumesListResponse struct {
	Volumes  []*VolumeResponse
	Warnings []string
}
