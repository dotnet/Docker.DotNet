using System;
using Xunit;

namespace DockerSdk.Tests
{
    public sealed class Fixture : IDisposable
    {
        public Fixture()
        {
            // Start up the test environment.
            Cli.Run("cd scripts && docker-compose up --build --detach --no-color", ignoreErrors: true);
        }

        public void Dispose()
        {
            // Shut down the test environment.
            Cli.Run("cd scripts && docker-compose down", ignoreErrors: true);
        }
    }

    [CollectionDefinition("Common")]
    public class FixtureCollection : ICollectionFixture<Fixture>
    {
        // This class is never instantiated. It's simply a marker class used by XUnit.
    }
}
