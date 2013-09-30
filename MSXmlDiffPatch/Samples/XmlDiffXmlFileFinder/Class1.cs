using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3.Security.Tokens;
using System.Xml;

namespace XmlDiffXmlFileFinder
{
    /// <summary>
    /// This application is used to find/generate the xml files from the SOAP web services available on CrmOnDemand
    /// </summary>
    public class Class1
    {
        private const string FIELD_MANAGEMENT_SERVICE = "FieldManagementService";
        private const string FIELD_MANAGEMENT_READ_ALL = FIELD_MANAGEMENT_SERVICE + ".FieldManagementReadAll";
        
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("XmlDiffXmlFileFinder");

        /// <summary>
        /// main function. code starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var curArgsIndex = 0;
            if (args.Length < 1)
            {
                WriteUsage();
                return;
            }

            try
            {
                string servicesFileName = args[curArgsIndex];
                var services = LoadServiceConfig(servicesFileName);
                var msg = "";
                foreach (var service in services)
                {
                    switch (service.ServiceName)
                    {
                        case FIELD_MANAGEMENT_SERVICE:
                            {
                                msg = "Loading xml data from " + FIELD_MANAGEMENT_SERVICE + " ..."; 
                                log.Info(msg);
                                Console.WriteLine(msg);
                                CallFieldManagementService(service);
                                break;
                            }
                        default:
                            {
                                msg = "Error: Unsupported service '" + service.ServiceName + "' in service config file.";
                                log.Error(msg);
                                Console.WriteLine(msg);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// load files from FieldManagmentService
        /// </summary>
        /// <param name="service"></param>
        private static void CallFieldManagementService(ServiceConfig service)
        {
            var crmOnDemandFieldManagementService = new CrmOnDemandFieldManagementService.FieldManagementService();

            var requestContext = crmOnDemandFieldManagementService.RequestSoapContext;

            UsernameToken userToken = new UsernameToken(service.Username, service.Password, PasswordOption.SendPlainText);

            requestContext.Security.Tokens.Add(userToken);

            var fieldManagementReadAll_Input = new CrmOnDemandFieldManagementService.FieldManagementReadAll_Input();
            fieldManagementReadAll_Input.IncludeAll = true;

            log.Info("Making service call to " + FIELD_MANAGEMENT_READ_ALL);
            var fieldManagementReadAll_Output = crmOnDemandFieldManagementService.FieldManagementReadAll(fieldManagementReadAll_Input);

            log.Debug("Service call to " + FIELD_MANAGEMENT_READ_ALL + " returned successfully");

            XmlTextWriter writer = new XmlTextWriter(service.ResponseFileName, null);
            writer.Formatting = Formatting.Indented;
            crmOnDemandFieldManagementService.ResponseSoapContext.Envelope.WriteContentTo(writer);
            writer.Flush();

            var msg = FIELD_MANAGEMENT_READ_ALL + " output saved to file: " + service.ResponseFileName;
            log.Info(msg);
            Console.WriteLine(msg);            
        }

        /// <summary>
        /// load the web service configuration.
        /// each wb service is identified by name. it also needs username and pwd and the name of the output file.
        /// </summary>
        /// <param name="ignoreFile"></param>
        /// <returns></returns>
        static private List<ServiceConfig> LoadServiceConfig(string serviceFile)
        {
            var Services = new List<ServiceConfig>();

            XmlDocument doc = new XmlDocument();
            doc.Load(serviceFile);
            XmlNodeList nodes = doc.SelectNodes("/services/service");
            int i = 0;
            foreach (XmlNode service in nodes)
            {
                var serviceConfig = new ServiceConfig
                {
                    ServiceName = service.SelectSingleNode("name").InnerText,
                    Username = service.SelectSingleNode("username").InnerText,
                    Password = service.SelectSingleNode("password").InnerText,
                    ResponseFileName = service.SelectSingleNode("responseFullPathFileName").InnerText
                };

                Services.Add(serviceConfig);
                i++;
                log.InfoFormat("Service[{0}] loaded: {1}", i, serviceConfig.ToString());
            }
            return Services;
        }

        static private void WriteUsage()
        {
            var msg = "USAGE: XmlDiffXmlFileFinder <services_config_xml>\n\n" +
                "services_config_xml - full file path name to the file with the services configuration";
            log.Error("USAGE: XmlDiffXmlFileFinder <services_config_xml>");
            Console.WriteLine(msg);
        }
    }
}
