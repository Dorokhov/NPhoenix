using System;
using System.Globalization;

namespace NPhoenix.Client
{
    [Serializable]
    public class PlatformBridgeSetupException : Exception
    {
        internal PlatformBridgeSetupException(string message)
            : base(message)
        {   
        }

        internal PlatformBridgeSetupException(string format, params object[] parameters)
            : base(string.Format(CultureInfo.InvariantCulture, format, parameters))
        {
        }
    }
}