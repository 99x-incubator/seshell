using System;
using System.Xml.Serialization;
using SeShell.Test.XMLTestResult.Interface;

namespace SeShell.Test.XMLTestResult.XMLObjects
{
    [XmlRoot("test-case", Namespace = "",
IsNullable = false)]
    public class TestCase : ITestResults
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "executed")]
        public bool Executed { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public string Result { get; set; }

        [XmlAttribute(AttributeName = "success")]
        public bool Success { get; set; }

        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }

        [XmlAttribute(AttributeName = "asserts")]
        public string Asserts { get; set; }

        [XmlElement("failure")]
        public Failure Failure { get; set; }

        [XmlIgnore]
        public bool FailureSpecified = false;

        public void WriteTagToFile()
        {
            throw new NotImplementedException();
        }
    }
}