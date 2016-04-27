# .NET Client for Docker Remote API

This library allows you to interact with [Docker Remote API][docker-remote-api]  endpoints in your .NET applications. 

It is fully asynchronous, designed to be non-blocking and object-oriented way to interact with your Docker daemon programmatically. Currently, it supports [Docker Remote API v1.16][v1.16].

## Installation

You can add this library to your project using [NuGet][nuget]. This is the only method this library is currently distributed unless you choose to build your own binaries using source code. Run the following command in the “Package Manager Console”:

    PM> Install-Package Docker.DotNet
    
Or right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘Docker.DotNet’ and click ‘Install’. ([see NuGet Gallery][nuget-gallery].)

## Usage

You can initialize the client like the following:

```csharp
using Docker.DotNet;
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

#### Example: Create an image by pulling from Docker Registry

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

#### Example: Start a container

The following code will start the created container with specified host configuration object. This object is optional, therefore you can pass a null.

There is no usage of optional values in the method signatures, mostly because these behavior is undefined in Docker API as well.

```csharp
await client.Containers.StartContainerAsync ("39e3317fd258", new HostConfig(){
	Dns = new[]{"8.8.8.8", "8.8.4.4"}
});
```

#### Example: Stop a container

```csharp
var stopped = await client.Containers.StopContainerAsync ("39e3317fd258",
    new StopContainerParameters(){
        Wait = TimeSpan.FromSeconds(30)
    },
    CancellationToken.None);
```

Above, the `Wait` field is of type `TimeSpan?` which means optional. This code will wait 30 seconds before killing it. If you like to cancel the waiting, you can use the CancellationToken parameter.

#### Example: Dealing with Stream responses

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

#### Example: HTTPS Authentication to Docker

If you are [running Docker with TLS (HTTPS)][docker-tls], you can authenticate to the Docker instance using the [**`Docker.DotNet.X509`**][Docker.DotNet.X509] package. You can get this package from NuGet or by running the following command in the “Package Manager Console”:

    PM> Install-Package Docker.DotNet.X509

Once you add `Docker.DotNet.X509` to your project, use `CertificateCredentials` type:

```csharp
var credentials = new CertificateCredentials (new X509Certificate2 ("CertFile", "Password"));
var config = new DockerClientConfiguration("http://ubuntu-docker.cloudapp.net:4243", credentials);
DockerClient client = config.CreateClient();
```

If you don't want to authenticate you can omit the `credentials` parameter, which defaults to an `AnonymousCredentials` instance.

The `CertFile` in the example above should be a .pfx file (PKCS12 format), if you have .pem formatted certificates which Docker normally uses you can either convert it programmatically or use `openssl` tool to generate a .pfx:

    openssl pkcs12 -export -inkey key.pem -in cert.pem -out key.pfx

(Here, your private key is key.pem, public key is cert.pem and output file is named key.pfx.) This will prompt a password for PFX file and then you can use this PFX file on Windows. If the certificate is self-signed, your application may reject the server certificate, in this case you might want to disable server certificate validation: `ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;`

#### Example: Basic HTTP Authentication to Docker

If the Docker instance is secured with Basic HTTP Authentication, you can use the [**`Docker.DotNet.BasicAuth`**][Docker.DotNet.BasicAuth] package. Get this package from NuGet or by running the following command in the “Package Manager Console”:

    PM> Install-Package Docker.DotNet.BasicAuth

Once you added `Docker.DotNet.BasicAuth` to your project, use `BasicAuthCredentials` type:

```csharp
var credentials = new BasicAuthCredentials ("YOUR_USERNAME", "YOUR_PASSWORD");
var config = new DockerClientConfiguration("tcp://ubuntu-docker.cloudapp.net:4243", credentials);
DockerClient client = config.CreateClient();
```

`BasicAuthCredentials` also accepts `SecureString` for username and password arguments.

#### Example: Specifying Remote API Version

By default this client does not specify version number to the API for the requests it makes. However, if you would like to make use of versioning feature of Docker Remote API You can initialize the client like the following.

```csharp
var config = new DockerClientConfiguration(...);
DockerClient client = config.CreateClient(new Version(1, 16));
```

### Error Handling
	
Here are typical exceptions thrown from the client library:

* **`DockerApiException`** is thrown when Docker API responds with a non-success result. Subclasses:
    * **``DockerContainerNotFoundException``**
    * **``DockerImageNotFoundException``**
* **`TaskCanceledException`** is thrown from `System.Net.Http.HttpClient` library by design. It is not a friendly exception, but it indicates your request has timed out. (default request timeout is 100 seconds.) 
    * Long-running methods (e.g. `WaitContainerAsync`, `StopContainerAsync`) and methods that return Stream (e.g. `CreateImageAsync`, `GetContainerLogsAsync`) have timeout value overridden with infinite timespan by this library.
* **`ArgumentNullException`** is thrown when one of the required parameters are missing/empty.
    * Consider reading the [Docker Remote API reference](docker-remote-api) and source code of the corresponding method you are going to use in from this library. This way you can easily find out which parameters are required and their format.

## Versioning

Development of this client library was initially based on [Docker Remote API v1.14][v1.14]. Its forward compatibility is dependent on Docker's policy on forward compatibility. Feel free to send pull requests as the API changes over time. Please refer to the Changelog below to see what version of Docker Remote API is currently supported.

Backwards compatibility is not tested and therefore not guaranteed.

## Changelog

```
v2.124.0
======
+ Updated to support Docker Engine API 1.24.

Acknowledgements
- Justin Terry (@jterry75)


v1.2.2
======
+ Added no/unless-stopped restart policies (#41)
+ Bugfix: streams are being closed prematurely (#42)
+ Added /containers/(id)/archive methods from Remote API v1.20 (#43)
+ Deprecated: CopyFromContainerAsync (in favor of archival methods) (#43)

Acknowledgements
- Oliver Neal (@ItsVeryWindy)


v1.2.1
======
+ Added missing restart policies no/unless-stopped
+ Added undefined restart policy (#36)
+ Use only Http handler per DockerClient instance (#40)

Acknowledgements
- Tugberk Ugurlu (@tugberkugurlu)


v1.2.0
======
+ Added `docker exec` endpoints support (#35)

Acknowledgements
- Ricardo Peres (@rjperes)

v1.1.2.1
========
+ Added support for Docker Remote API v1.16 (#19)
+ Bugfix: add Microsoft.Bcl.Async dependency (#19)
+ Bugfix: parameter conversion error in ListContainersParameters (#16)
+ Added support for RestartPolicy (#9)

Acknowledgements
- Josh Garverick (@jgarverick)
- Shakirov Ruslan (@build-your-web)

v1.1.1
======
+ Bugfix: infinite timeout for stream requests lost in PCL translation
+ New: support to specify a container name in CreateContainerAsync

Acknowledgemnets
- @jgarverick for implementing container name support
- Iouri Simernitski (@pefferie) for bug report

v1.1.0
======
+ PCL support
+ Support for Basic HTTP authentication
+ CertificateCredentials (X509) is now a separate package

Acknowledgements:
- @jgarverick for helping out extensively on PCL support
- @Gandalis for implementing basic auth

v1.0.0
======
+ Initial release
```

## Known Issues / TODO

* HTTP Hijacking is not implemented yet, therefore "Attach to Container" operation does not exist in the library (expecting pull requests!) A workaround might be using WebSockets (/attach/ws).
* Certificate authentication does not work on Mono. [[StackOverflow question](http://stackoverflow.com/questions/25495056/using-custom-ssl-client-certificates-system-net-httpclient-on-mono)]
* Deserialization of DateTime fields from JSON responses will fail with System.FormatException on Mono due to [Mono bug #22417](https://bugzilla.xamarin.com/show_bug.cgi?id=22417). Any responses contain DateTime fields will fail on Mono.
* Test suite does not exist. Functionality is verified manually. (pull requests are welcomed, I can provide a private test instance on cloud!)
* ~~CertificateCredentials class is not tested.~~
* ~~Ability to specify version and using that the request URI is not implemented.~~
* ~~Fields that could have been DateTime are either long or string because Docker API uses inconsistent date formats in the API ([docker issue #7670](https://github.com/docker/docker/issues/7670)).~~

## License

This work is licensed under [Apache 2.0 License](LICENSE). Copyright 2014 Ahmet Alp Balkan.

> **DISCLAIMER:** This project is neither affiliated with Docker, Inc. nor Microsoft Corporation.

## Authors/Contributors

* [Ahmet Alp Balkan](http://ahmetalpbalkan.com)
* [Josh Garverick](http://blogs.obliteracy.net/)
* [Andreas Bieber](https://github.com/Gandalis)
* [Shakirov Ruslan](https://github.com/build-your-web)

[docker-remote-api]: https://docs.docker.com/reference/api/docker_remote_api/
[v1.14]: https://docs.docker.com/reference/api/docker_remote_api_v1.14/
[v1.16]: https://docs.docker.com/reference/api/docker_remote_api_v1.16/
[docker-tls]: https://docs.docker.com/articles/https/
[nuget]: http://www.nuget.org
[nuget-gallery]: https://www.nuget.org/packages/Docker.DotNet/
[Docker.DotNet.X509]: https://www.nuget.org/packages/Docker.DotNet.X509/
[Docker.DotNet.BasicAuth]: https://www.nuget.org/packages/Docker.DotNet.BasicAuth/
