using System;
using System.Xml.Serialization;
using SeShell.Test.XMLTestResult.Interface;

namespace SeShell.Test.XMLTestResult.XMLObjects
{
    public class Results : ITestResults
    {
        [XmlElement("test-suite", IsNullable = true)]
        public ResultTestSuite[] TestSuites { get; set; }

        [XmlElement("test-case")]
        public TestCase[] TestCases { get; set; }

        public void WriteTagToFile()
        {
            throw new NotImplementedException();
        }
    }
}