using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Core = Docker.DotNet;
using CoreModels = Docker.DotNet.Models;

namespace DockerSdk.Registries
{
    /// <summary>
    /// Caches credentials for Docker registries and provides a means to check the credentials against the registry.
    /// </summary>
    public class RegistryAccess
    {
        internal RegistryAccess(DockerClient client)
        {
            _client = client;
            AddBuiltInRegistries();
        }

        /// <summary>
        /// Gets the Docker registries that have cache entries.
        /// </summary>
        public IEnumerable<string> Registries => _entriesByServer.Keys;

        private readonly DockerClient _client;

        private readonly Dictionary<string, RegistryEntry> _entriesByServer = new Dictionary<string, RegistryEntry>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Specifies that you want to use anonymous access to the indicated registry.
        /// </summary>
        /// <param name="registry">The name of the registry, as used in image names.</param>
        /// <exception cref="ArgumentException">The input is null, empty, or malformatted.</exception>
        public void AddAnonymous(string registry)
        {
            // Scrub the input.
            ValidateRegistryName(registry);

            // Get or add an entry for the registry.
            if (!_entriesByServer.TryGetValue(registry, out RegistryEntry entry))
                entry = _entriesByServer[registry] = new RegistryEntry(registry);

            entry.AuthObject = new CoreModels.AuthConfig { ServerAddress = registry };
            entry.IsAnonymous = true;
        }

        /// <summary>
        /// Specifies that you want to use <a href="https://en.wikipedia.org/wiki/Basic_access_authentication">basic
        /// authentication</a> for access to the indicated registry.
        /// </summary>
        /// <param name="registry">The name of the registry, as used in image names.</param>
        /// <param name="username">The username to use for the registry.</param>
        /// <param name="password">The password to use for the registry.</param>
        /// <exception cref="ArgumentException">
        /// The registry input is null, empty, or malformatted; or the username or password are null or empty.
        /// </exception>
        public void AddBasicAuth(string registry, string username, string password)
        {
            // Scrub the inputs.
            ValidateRegistryName(registry);
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException($"'{nameof(username)}' cannot be null or empty", nameof(username));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty", nameof(password));

            // Get or add an entry for the registry.
            if (!_entriesByServer.TryGetValue(registry, out RegistryEntry entry))
                entry = _entriesByServer[registry] = new RegistryEntry(registry);

            entry.AuthObject = new CoreModels.AuthConfig { ServerAddress = registry, Username = username, Password = password };
            entry.IsAnonymous = false;
        }

        /// <summary>
        /// Specifies that you want to use an identity token for authenticating with the indicated registry.
        /// </summary>
        /// <param name="registry">The name of the registry, as used in image names.</param>
        /// <param name="identityToken">An identity token granted by the registry.</param>
        /// <exception cref="ArgumentException">
        /// The registry input is null, empty, or malformatted; or the identity token is null or empty.
        /// </exception>
        public void AddIdentityToken(string registry, string identityToken)
        {
            // Scrub the inputs.
            ValidateRegistryName(registry);
            if (string.IsNullOrEmpty(identityToken))
                throw new ArgumentException($"'{nameof(identityToken)}' cannot be null or empty", nameof(identityToken));

            // Get or add an entry for the registry.
            if (!_entriesByServer.TryGetValue(registry, out RegistryEntry entry))
                entry = _entriesByServer[registry] = new RegistryEntry(registry);

            entry.AuthObject = new CoreModels.AuthConfig { ServerAddress = registry, IdentityToken = identityToken };
            entry.IsAnonymous = false;
        }

        /// <summary>
        /// Tests whether the client can authenticate with the indicated registry.
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <remarks>
        /// Use the various <c>Add</c>* methods to supply authentication instructions. If no such instructions are
        /// provided, this method will try anonymous access.
        /// </remarks>
        /// <exception cref="ArgumentException">The registry input is null, empty, or malformatted.</exception>
        /// <exception cref="System.Net.Http.HttpRequestException">
        /// The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate
        /// validation, or timeout.
        /// </exception>
        public async Task<bool> CheckAuthenticationAsync(string registry, CancellationToken ct = default)
        {
            // Scrub the inputs.
            ValidateRegistryName(registry);

            // If we have an entry for that registry, get the auth object. Otherwise create a new auth object, and set a
            // flag indicating that the registry isn't already known.
            bool isInCache = TryGetAuthObject(registry, out CoreModels.AuthConfig authObject);
            if (!isInCache)
                authObject = new CoreModels.AuthConfig { ServerAddress = registry };

            try
            {
                // Try to authenticate.
                await _client.Core.System.AuthenticateAsync(authObject, ct).ConfigureAwait(false);
            }
            catch (Core.DockerApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                    return false;
                if (ex.StatusCode == HttpStatusCode.InternalServerError && ex.Message.Contains("401 Unauthorized"))
                    return false;
                if (ex.StatusCode == HttpStatusCode.InternalServerError && ex.Message.Contains("no basic auth credentials"))
                    return false; // the registry only accepts basic auth and was given something else

                throw DockerException.Wrap(ex);
            }

            // TODO after https://github.com/dotnet/Docker.DotNet/issues/493 is resolved: if we're given an auth token,
            // clear username/pass and start using that instead

            // If it wasn't in cache but it accepted anonymous access, cache it now.
            if (!isInCache)
                AddAnonymous(registry);

            return true;
        }

        /// <summary>
        /// Removes all custom registry entries.
        /// </summary>
        public void Clear()
        {
            _entriesByServer.Clear();
            AddBuiltInRegistries();
        }

        /// <summary>
        /// Parses an image's name to determine which registry the image is associated with.
        /// </summary>
        /// <param name="imageName">The name of image. (Not the ID.)</param>
        /// <returns>The registry hostname, possibly including a port.</returns>
        public string GetRegistryName(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                throw new ArgumentException("Must not be null or empty.", nameof(imageName));

            // There's always at least one non-host component, so if there's only one component we know there's no host.
            // In that case Dockerhub is the registry.
            var components = imageName.Split('/');
            if (components.Length == 1)
                return "docker.io";

            // If there's a host component, it's always the first component.
            var candidate = components[0];

            // The only components that are allowed a : character are host components and final components. We're
            // looking at a non-final component, so if we find a : we know that we have a registry.
            if (candidate.Contains(':'))
                return candidate;

            // Only host components are allowed to use uppercase letters, so that's the next most straightforward way to
            // test.
            if (candidate.Any(char.IsUpper))
                return candidate;

            // The remaining criteria are ambiguous, so we're basically making educated guesses. First, check for
            // well-known references to the current host. Note that we don't need to check the IP v6 loopback address
            // because it can't be part of a valid image name.
            if (candidate == "localhost" || candidate == "127.0.0.1")
                return candidate;

            // Otherwise, if we have a cached registry with this name, treat it as a registry.
            if (_entriesByServer.ContainsKey(candidate))
                return candidate;

            // Otherwise, if it contains any . characters, assume that it's a hostname.
            if (candidate.Contains('.'))
                return candidate;

            // Otherwise, assume that it isn't a host name, in which case we use Dockerhub.
            return "docker.io";
        }

        /// <summary>
        /// Removes an entry from the cache.
        /// </summary>
        /// <param name="registry">The host name of the registry to remove.</param>
        /// <returns>True if the entry was removed, or false if the entry was not present.</returns>
        /// <remarks>This method is equivalent to <c>docker logout</c>.</remarks>
        public bool Remove(string registry) => _entriesByServer.Remove(registry);

        /// <summary>
        /// Gets the auth information for the given image's registry.
        /// </summary>
        /// <param name="imageName">The name of image. ( <i>Not</i> the ID.)</param>
        /// <returns>Auth details for communicating with the image's registry, if known.</returns>
        internal bool TryGetAuthObject(string registry, out CoreModels.AuthConfig authObject)
        {
            if (!_entriesByServer.TryGetValue(registry, out RegistryEntry entry))
            {
                authObject = null;
                return false;
            }

            authObject = entry.AuthObject;
            return true;
        }

        private static void ValidateRegistryName(string registry)
        {
            if (string.IsNullOrEmpty(registry))
                throw new ArgumentException($"'{nameof(registry)}' cannot be null or empty", nameof(registry));
            if (registry.Contains("//"))
                throw new ArgumentException("The registry name must not include the protocol.", nameof(registry));
            if (registry.Contains('/'))
                throw new ArgumentException("The registry name must not include a path.", nameof(registry));
        }

        private void AddBuiltInRegistries()
        {
            // These common registries allow anonymous access for public images.
            AddAnonymous("docker.io"); // Dockerhub
            AddAnonymous("ghcr.io"); // Github
            AddAnonymous("mcr.microsoft.com"); // Microsoft Azure
            AddAnonymous("public.ecr.aws"); // Amazon AWS
        }
    }
}
