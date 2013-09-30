using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlDiffXmlFileFinder
{
    public class ServiceConfig
    {
        public string ServiceName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ResponseFileName { get; set; }

        public override string ToString()
        {
            return string.Format("[ServiceName:{0}][Username:{1}][Password:{2}][OutputFileName:{3}]", ServiceName, Username, Password, ResponseFileName);
        }
    }
}
