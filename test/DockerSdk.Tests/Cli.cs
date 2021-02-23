using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DockerSdk.Tests
{
    /// <summary>
    /// Runs commands on in a PowerShell command line. This is meant for setting up tests via the docker CLI.
    /// </summary>
    internal static class Cli
    {
        public static string ReadAndDeleteSingleLineFile(string path)
        {
            var line = File.ReadAllLines(path)[0].Trim();
            File.Delete(path);
            return line;
        }

        /// <summary>
        /// Runs a series of commands in a Powershell prompt.
        /// </summary>
        /// <param name="commands">An array where each line is a command to run.</param>
        /// <returns>An array of lines written to stdout across all commands.</returns>
        /// <remarks>
        /// If any command returns a non-zero exit code or writes to stderr, the subsequent commands do not run. <br/>
        /// Use of stdin is not supported.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// One of the commands either wrote to stdout or returned a non-zero exit code.
        /// </exception>
        public static string[] Run(params string[] commands)
            => Run(false, commands);

        /// <summary>
        /// Runs a series of commands in a Powershell prompt.
        /// </summary>
        /// <param name="ignoreErrors">True to ignore any errors that the commands may raise.</param>
        /// <param name="commands">An array where each line is a command to run.</param>
        /// <returns>An array of lines written to stdout across all commands.</returns>
        /// <remarks>
        /// If any command returns a non-zero exit code or writes to stderr, the subsequent commands do not run, unless
        /// <paramref name="ignoreErrors"/> is true. <br/> Use of stdin is not supported.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// One of the commands either wrote to stdout or returned a non-zero exit code, and <paramref
        /// name="ignoreErrors"/> was false.
        /// </exception>
        public static string[] Run(bool ignoreErrors, params string[] commands)
        {
            var output = new List<string>();
            foreach (var command in commands)
                output.AddRange(Run(command, ignoreErrors));
            return output.ToArray();
        }

        /// <summary>
        /// Runs a command in a Powershell prompt.
        /// </summary>
        /// <param name="command">The command to run.</param>
        /// <returns>An array of lines written to stdout.</returns>
        /// <exception cref="InvalidOperationException">
        /// The command either wrote to stdout or returned a non-zero exit code.
        /// </exception>
        public static string[] Run(string command)
            => Run(command, false);

        /// <summary>
        /// Runs a command in a Powershell prompt.
        /// </summary>
        /// <param name="command">The command to run.</param>
        /// <param name="ignoreErrors">True to ignore any errors that the command may raise.</param>
        /// <returns>An array of lines written to stdout.</returns>
        /// <exception cref="InvalidOperationException">
        /// The command either wrote to stdout or returned a non-zero exit code, and <paramref name="ignoreErrors"/> is
        /// false.
        /// </exception>
        public static string[] Run(string command, bool ignoreErrors)
        {
            var pi = new ProcessStartInfo("pwsh.exe", "-Command -")
            {
                CreateNoWindow = true,
                UseShellExecute = false,  // needed for .Net Framework, which defaults it to true
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };
            using var process = Process.Start(pi);

            process.StandardInput.WriteLine(command);
            process.StandardInput.WriteLine("exit");
            process.WaitForExit();

            if (!ignoreErrors)
            {
                var errors = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(errors))
                    throw new InvalidOperationException(errors);

                if (process.ExitCode != 0)
                    throw new InvalidOperationException($"The process exited with code {process.ExitCode}.");
            }

            return process.StandardOutput.ReadToEnd()
                .Split('\n')
                .Select(line => line.Trim())
                .ToArray();
        }
    }
}
