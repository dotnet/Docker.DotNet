# .NET Client for Docker Remote API

This library allows you to interact with [Docker Remote API][docker-remote-api]  endpoints in your .NET applications. 

It is fully asynchronous, designed to be non-blocking and object-oriented way to interact with your Docker daemon programmatically. At the time of writing, it supports [Docker Remote API v1.14][v1.14]

## Installation

You can add this library to your project using [NuGet](http://www.nuget.org). This is the only method this library is currently distributed unless you choose to build your own binaries using source code. Run the following command in the “Package Manager Console”:

    PM> Install-Package Docker.DotNet
    
Or right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘Docker.DotNet’ and click ‘Install’.

## Usage

`<<TODO>>`

#### Dealing with Stream responses

`<<TODO>>`

#### Authentication

`<<TODO>>`

## Versioning

Development of this client library is based on [Docker Remote API v1.14][v1.14]. Its forward compatibility is dependent on Docker's policy on forward compatibility. Feel free to send pull requests as the API changes over time.

Backwards compatibility is not tested and therefore not guaranteed.

## Changes

`<<TODO>>`

## Known Issues

`<<TODO>>`

## License

This work is licensed under [Apache 2.0 License](LICENSE). Copyright 2014 Ahmet Alp Balkan.

> **DISCLAIMER:** This project is not affiliated with Docker, Inc or Microsoft Corporation. By using this you accep

## Authors

* [Ahmet Alp Balkan](http://ahmetalpbalkan.com)


[docker-remote-api]: https://docs.docker.com/reference/api/docker_remote_api/
[v1.14]: https://docs.docker.com/reference/api/docker_remote_api_v1.14/
