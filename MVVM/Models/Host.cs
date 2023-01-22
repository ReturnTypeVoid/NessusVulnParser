using System.Collections.Generic;
using System.Net;
namespace NessusVulnParser.MVVM.Models
{
    public class Host
    {
        private IPAddress? IpAddress { get; set; }
        public List<Vulnerability>? Vulnerabilities { get; set; }

        public Host(IPAddress ipAddress)
        {
            IpAddress = ipAddress;
        }
    }
}
