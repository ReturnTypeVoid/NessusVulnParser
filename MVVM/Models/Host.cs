using System.Collections.Generic;
using System.Net;

namespace NessusVulnParser.Models
{
    public class Host
    {
        public IPAddress IPAddress { get; set; }
        public List<Vulnerability>? vulnerabilities { get; set; }

        public Host(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }
    }
}
