# .NET Client for Docker Remote API

This library allows you to interact with [Docker Remote API][docker-remote-api]  endpoints in your .NET applications. 

It is fully asynchronous, designed to be non-blocking and object-oriented way to interact with your Docker daemon programmatically. At the time of writing, it supports [Docker Remote API v1.14][v1.14]

## Installation

You can add this library to your project using [NuGet](http://www.nuget.org). This is the only method this library is currently distributed unless you choose to build your own binaries using source code. Run the following command in the “Package Manager Console”:

    PM> Install-Package Docker.DotNet
    
Or right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘Docker.DotNet’ and click ‘Install’.

## Usage

You can initialize the client like the following:

```csharp

using Docker.DotNet;
...

DockerClient client = new DockerClientConfiguration("http://ubuntu-docker.cloudapp.net:4243")
     .CreateClient();
```

#### Example: List containers

```csharp

IList<ContainerResponse> containers = await client.Containers.ListContainersAsync(
	new ListContainersParameters(){
		Limit = 10,
    });

```

### Example: Create container

The code below pulls `fedora/memcached` image to your Docker instance using your Docker Hub account. You can anonymously download the image as well by passing `null` instead of AuthConfig object:

```csharp
Stream stream  = await client.Images.CreateImageAsync (new CreateImageParameters () {
	FromImage = "fedora/memcached",
	Tag = "alpha",
}, new AuthConfig(){
	Email = "ahmetb@microsoft.com",
	Username = "ahmetalpbalkan",
	Password = "pa$$w0rd"
});
```

### Example: Start a container

The following code will start the created container with specified host configuration object. This object is optional, therefore you can pass a null.

There is no usage of optional values in the method signatures, mostly because these behavior is undefined in Docker API as well.

```csharp
await client.Containers.StartContainerAsync ("39e3317fd258", new HostConfig(){
	Dns = new[]{"8.8.8.8", "8.8.4.4"}
});
```

### Example: Stop a container

```csharp
var stopped = await client.Containers.StopContainerAsync ("39e3317fd258",
    new StopContainerParameters(){
        Wait = TimeSpan.FromSeconds(30)
    },
    CancellationToken.None);
```

Above, the `Wait` field is of type `TimeSpan?` which means optional. This code will wait 30 seconds before killing it. If you like to cancel the waiting, you can use the CancellationToken parameter.

### Example: Dealing with Stream responses

Some Docker API endpoints are designed to return stream responses. For example [Monitoring Docker events](https://docs.docker.com/reference/api/docker_remote_api_v1.13/#monitor-dockers-events) continuously streams the status in a format like :

```json
{"status":"create","id":"dfdf82bd3881","from":"base:latest","time":1374067924}
{"status":"start","id":"dfdf82bd3881","from":"base:latest","time":1374067924}
{"status":"stop","id":"dfdf82bd3881","from":"base:latest","time":1374067966}
{"status":"destroy","id":"dfdf82bd3881","from":"base:latest","time":1374067970}
...
```

To obtain this stream you can use:

```csharp
CancellationTokenSource cancellation = new CancellationTokenSource();
Stream stream = await client.Miscellaneous.MonitorEventsAsync(new MonitorDockerEventsParameters(), cancellation.Token);
// Initialize a StreamReader...
```

You can cancel streaming using the CancellationToken. On the other hand, if you wish to continuously stream, you can simply pass `CancellationToken.None`.

### Example: HTTPS Authentication to Docker

If you are [running Docker with TLS (HTTPS)][docker-tls], you can authenticate to the Docker instance using `CertificateCredentials`:

```csharp
var credentials = new CertificateCredentials (new X509Certificate2 ("CertFile", "Password"));
var config = new DockerClientConfiguration("http://ubuntu-docker.cloudapp.net:4243", credentials);
DockerClient client = config.CreateClient();
```

If you don't want to authenticate you can omit the `credentials` parameter, which defaults to an `AnonymousCredentials` instance.

### Error Handling
	
Here are typical errors from the client library:

* **`DockerAPIException`** is thrown when Docker API responds with non-success result. You can use `StatusCode` and `ResponseBody` properties of the exception for further logging.
* **`TaskCanceledException`** is thrown from `System.Net.Http` library by design. It is not a friendly exception but usually means your request has timed out. (HttpClient has 100 seconds of default timeout per request.) 
> Long-running methods (e.g. `WaitContainerAsync`, `StopContainerAsync`) and methods that return Stream (e.g. `CreateImageAsync`, `GetContainerLogsAsync`) have infinite timeout configured by the library.
* **`ArgumentNullException`** is thrown when one of the required parameters are missing/empty.

> Consider reading Docker API reference and source code of the corresponding method you are going to use in from this library. This way you can easily find out which parameters are required and their format.

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
[docker-tls]: https://docs.docker.com/articles/https/