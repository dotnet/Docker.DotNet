using System;
using Xunit.Abstractions;

namespace Docker.DotNet.Tests
{
    public class TestOutput
    {
        private readonly ITestOutputHelper _outputHelper;

        public TestOutput(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
            _outputHelper.WriteLine(line);
            System.Diagnostics.Debug.WriteLine(line);
        }
    }
}
