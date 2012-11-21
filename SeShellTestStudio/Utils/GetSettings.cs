using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.Configuration;



namespace SeShellTestStudio.Utils
{
    public class GetSettings
    {
        private Dictionary<string, string> data;
        private string fileName;
        private string DefaultConfigPath = "..\\SeShellTest\\Core\\Settings\\Property.config";

        public string GetResultPath(string appPath)
        {

            string DefalutPath = "C:\\SeTestResults";
            string AppPath = appPath ;
            fileName = AppPath + GetCustomerSetPropertyPath();
            string xml = System.IO.File.ReadAllText(fileName);
            data = XElement.Parse(xml)
            .Elements("add")
            .ToDictionary(
                el => (string)el.Attribute("key"),
                el => (string)el.Attribute("value")
            );

            foreach (var obj in data)
            {
                if (obj.Key == "TestReslutDirectory")
                {
                    if (!string.IsNullOrEmpty(obj.Value))
                    { DefalutPath = obj.Value; }
                    break;
                }
            }
            return DefalutPath;
        }

        public string GetConfigFilePath(string appPath)
        {
            String path = appPath;
            fileName = path + GetCustomerSetPropertyPath();
            return fileName;
        }

        public string GetCustomerSetPropertyPath()
        {
            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["PropertyPath"]))
            {
               return WebConfigurationManager.AppSettings["PropertyPath"].ToString();
            }
            return DefaultConfigPath;
        }

        
    }
}