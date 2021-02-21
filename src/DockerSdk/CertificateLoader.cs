using System.Security.Cryptography.X509Certificates;

namespace DockerSdk
{
    internal static class CertificateLoader
    {
        /// <summary>
        /// Loads certificates from a file.
        /// </summary>
        /// <param name="path">The file to load from.</param>
        /// <returns>The resultant collection of certificates.</returns>
        /// <exception cref="CryptographicException">The file did not represent a valid certificate.</exception>
        public static X509Certificate2Collection Load(string path)
        {
            var collection = new X509Certificate2Collection();
            collection.Import(path);
            return collection;
        }
    }
}
