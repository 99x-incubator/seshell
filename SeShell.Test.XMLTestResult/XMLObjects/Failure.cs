using System;
using System.Xml.Serialization;
using SeShell.Test.XMLTestResult.Interface;

namespace SeShell.Test.XMLTestResult.XMLObjects
{
    [XmlRoot("failure", Namespace = "",
IsNullable = false)]
    public class Failure : ITestResults
    {
        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("stack-trace")]
        public string StackTrace { get; set; }

        public void WriteTagToFile()
        {
            throw new NotImplementedException();
        }
    }
}