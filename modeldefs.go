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
	Name              string `rest:"in:query,name:name"`
	*container.Config `rest:"in:body"`
	HostConfig        *container.HostConfig     `rest:"in:body"`
	NetworkingConfig  *network.NetworkingConfig `rest:"in:body"`
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
