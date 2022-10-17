using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordSecurity_Test.Support
{
    internal class Enums
    {
        public enum ScanResult_t
        {
            UNDETERMINED,
            MALWARE,
            BENIGN,
            SCAN_ERROR
        }

        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
