using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownTemplating.Models
{
    public class DummyITSMItem
    {
        public string Hostname { get; set; } = "localhost";
        public IPAddress IPAddress { get; set; } = IPAddress.Parse("127.0.0.1");
        public string Customer { get; set; } = "Contoso";
    }
}
