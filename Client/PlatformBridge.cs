using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using net.sf.jni4net;

namespace NPhoenix.Client
{
    internal static class PlatformBridge
    {
        private static bool IsRunning = false;
        private static readonly object LockObj = new object();
        private const string PhoenixClientJarPattern = "phoenix-*-*-client.jar";

        internal static void Setup()
        {
            if (!IsRunning)
            {
                lock (LockObj)
                {
                    if (!IsRunning)
                    {
                        string phoenixClientJarPath = FindPhoenixClientJar();
                        SetupPhoenixClientJar(phoenixClientJarPath);

                        IsRunning = true;
                    }
                }
            }
        }

        private static string FindPhoenixClientJar()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(directoryName, PhoenixClientJarPattern, SearchOption.TopDirectoryOnly);
            if (!files.Any())
            {
                throw new PlatformBridgeSetupException("No files matching '{0}' pattern found in executing assembly folder '{1}'", PhoenixClientJarPattern, directoryName);
            }

            if (files.Count() > 1)
            {
                throw new PlatformBridgeSetupException(
                    "Expected one phoenix client file, but found '{0}' matching pattern '{1}', in executing assembly folder '{2}'. Matching names are: {3}",
                    files.Count(),
                    PhoenixClientJarPattern,
                    directoryName,
                    String.Join(",", files.Select(x => string.Format(CultureInfo.InvariantCulture, "'{0}'", x))));
            }

            return files.First();
        }

        private static void SetupPhoenixClientJar(string phoenixClientJarPath)
        {
            var bridgeSetup = new BridgeSetup() { Verbose = true };
            bridgeSetup.AddClassPath(phoenixClientJarPath);
            Bridge.CreateJVM(bridgeSetup);
        }
    }
}