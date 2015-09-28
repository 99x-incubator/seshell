using System.Xml.Serialization;
using SeShell.Test.XMLTestResult.Interface;

namespace SeShell.Test.XMLTestResult.XMLObjects
{
    [XmlRoot("test-suite")]
    public class ResultTestSuite : ITestResults
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

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

        [XmlElement("results")]
        public Results Results { get; set; }

        public void WriteTagToFile()
        {
            throw new System.NotImplementedException();
        }
    }
}