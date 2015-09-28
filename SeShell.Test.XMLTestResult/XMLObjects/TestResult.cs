using SeShell.Test.XMLTestResult.Interface;
using System;
using System.Xml.Serialization;

namespace SeShell.Test.XMLTestResult.XMLObjects
{
    [XmlRoot("test-results")]
    public class TestResults : ITestResults
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }

        [XmlAttribute(AttributeName = "errors")]
        public string Errors { get; set; }

        [XmlAttribute(AttributeName = "not-run")]
        public string NotRun { get; set; }

        [XmlAttribute(AttributeName = "inconclusive")]
        public string Inconclusive { get; set; }

        [XmlAttribute(AttributeName = "ignored")]
        public string Ignored { get; set; }

        [XmlAttribute(AttributeName = "skipped")]
        public string Skipped { get; set; }

        [XmlAttribute(AttributeName = "invalid")]
        public string Invalid { get; set; }

        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }

        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }

        [XmlElement("test-suite")]
        public ResultTestSuite TestSuite;

        public void WriteTagToFile()
        {
            throw new NotImplementedException();
        }
    }
}