namespace NessusVulnParser.Models
{
    internal class Port
    {
        public int Number { get; set; }
        public string Protocol { get; set; }

        public Port(int number, string protocol)
        {
            Number = number;
            Protocol = protocol;
        }
    }
}
