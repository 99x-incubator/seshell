using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.IO;
using System.Web.Services;
using System.Collections.Generic;
using System.Xml.Linq;
using SeShellTestStudio.Utils;

namespace SeShellTestStudio
{
    public partial class dashboard : System.Web.UI.Page
    {
        private string dir = "";       
        private string result;
        private string summary;
        private string acc_name;
        private int acc_num;
        private int openDivCount;
        private int numOfTestFixtures;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetSettings settings = new GetSettings();
            dir = settings.GetResultPath(Server.MapPath("~"));
            showContent();
        }

        private void showContent()
        {
            execute();
            summary_box.InnerHtml = summary;
            result_accordion.InnerHtml = result;
        }

        private void mainUI(TestSuite obj)
        {
            numOfTestFixtures++;
            if (openDivCount != 0)
            {
                result += "</div></div>";
                openDivCount = 0;
            }
            acc_name = "accordion" + acc_num;
            acc_num++;
            result += "<h3><a href='#'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj.Name;
            if (obj.Result.Equals("Success"))
            {
                result += "<img src='images/success.png' ";
            }
            else
            {
                result += "<img src='images/failure.png' ";
            }
            result += "style='position: absolute; left: .5em; top: 50%; margin-top: -8px;'/></a></h3><div>";
            result += "<script>$(document).ready(function (){$('#" + acc_name + "').accordion({ clearStyle: true, autoHeight: false, collapsible: true, active: false });});</script>";
            result += "<div id='" + acc_name + "'>";
        }

        private void subUI(TestSuite obj)
        {
            openDivCount = 2;
            foreach (var TCaseObject in obj.Results.TCase)
            {
                result += "<h3><a href='#'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + TCaseObject.Name;
                if (TCaseObject.Result.Equals("Success"))
                {
                    result += "<img src='images/success.png' ";
                }
                else if (TCaseObject.Result.Equals("NotRunnable"))
                {
                    result += "<img src='images/notrunnable.png' ";
                }
                else
                {
                    result += "<img src='images/failure.png' ";
                }
                result += "style='position: absolute; left: .5em; top: 50%; margin-top: -8px;'/></a></h3><div>";
                result += "<table width='100%'>";
                result += "<tr>";
                result += "<td width='20%' align='center'><b>Executed</b></td>";
                result += "<td width='20%' align='center'><b>Result</b></td>";
                result += "<td width='20%' align='center'><b>Success</b></td>";
                result += "<td width='20%' align='center'><b>Asserts</b></td>";
                result += "<td width='20%' align='center'><b>Time</b></td>";
                result += "</tr>";
                result += "<tr>";
                result += "<td width='20%' align='center'>" + TCaseObject.Executed + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Result + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Success + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Asserts + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Time + "</td>";
                result += "</tr>";
                result += "</table>";
                if (TCaseObject.Result.Equals("Error"))
                {
                    result += "<br/>";
                    result += "<table width='100%'>";
                    result += "<tr>";
                    result += "<td width='20%' align='center'><b>Message</b></td>";
                    result += "<td width='80%'>" + TCaseObject.Failure.Message + "</td>";
                    result += "</tr>";
                    result += "<tr>";
                    result += "<td width='20%' align='center'><b>Stack Trace</b></td>";
                    result += "<td width='80%'>" + TCaseObject.Failure.StackTrace + "</td>";
                    result += "</tr>";
                    result += "</table>";
                }
                result += "</div>";
            }
        }

        private void summaryUI(TestResults TResults)
        {
            summary += "<center><h1>" + TResults.Name + "</h1><h3>Date: " + TResults.Date + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Time Taken: " + TResults.Time + "</h3></center><br/>";
            summary += "<table width=100%>";
            summary += "<tr style='font-size:14px'>";
            summary += "<td width=20% align='center'><b>Total</b></td>";
            summary += "<td width=20% align='center'><b>Successful</b></td>";
            summary += "<td width=20% align='center'><b>Errors</b></td>";
            summary += "<td width=20% align='center'><b>Skipped</b></td>";
            summary += "<td width=20% align='center'><b>Not Run</b></td>";
            summary += "</tr>";
            summary += "<tr style='font-size:56px'>";
            summary += "<td width=20% align='center'>" + TResults.Total + "</td>";
            summary += "<td width=20% align='center'><font color='#00D300'>" + (int.Parse(TResults.Total) - int.Parse(TResults.Errors)) + "</font></td>";
            summary += "<td width=20% align='center'><font color='#FF0000'>" + TResults.Errors + "</font></td>";
            summary += "<td width=20% align='center'><font color='#FFEA00'>" + TResults.Skipped + "</font></td>";
            summary += "<td width=20% align='center'><font color='#FFEA00'>" + TResults.NotRun + "</font></td>";
            summary += "</tr>";
            summary += "</table>";
        }

        protected void execute()
        {
            TestResults TResults = null;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var directory = new DirectoryInfo(dir);
            string path = "";

            if (directory.GetFiles("*.xml").Length > 0)
            {
                if (Data.xmlFileName == null)
                {
                    var latestFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                    path = directory + "\\" + latestFile.ToString();
                }
                else
                {
                    path = directory + "\\" + Data.xmlFileName + ".xml";
                    Data.xmlFileName = null;
                }
                               
                var serializer = new XmlSerializer(typeof(TestResults));

                var reader = new StreamReader(path);
                TResults = (TestResults)serializer.Deserialize(reader);
                reader.Close();

                result = "<script>$(document).ready(function (){$('#accordion').accordion({ clearStyle: true, autoHeight: false, collapsible: true, active: false});});</script><div id='accordion'>";

                if (TResults != null && TResults.TSuite != null)
                {
                    //TestSuite-Depth:1
                    foreach (var TSuiteObject1 in TResults.TSuite)
                    {
                        //Test-Fixture:Depth 1
                        if (TSuiteObject1.Type.Equals("TestFixture"))
                        {
                            mainUI(TSuiteObject1);
                        }

                        //Test-Case:Depth 1
                        if (TSuiteObject1.Results != null && TSuiteObject1.Results.TCase != null)
                        {
                            subUI(TSuiteObject1);
                        }

                        if (TSuiteObject1.Results != null && TSuiteObject1.Results.TSuite != null)
                        {
                            //TestSuite-Depth:2
                            foreach (var TSuiteObject2 in TSuiteObject1.Results.TSuite)
                            {
                                //Test-Fixture:Depth 2
                                if (TSuiteObject2.Type.Equals("TestFixture"))
                                {
                                    mainUI(TSuiteObject2);
                                }

                                //Test-Case:Depth 2
                                if (TSuiteObject2.Results != null && TSuiteObject2.Results.TCase != null)
                                {
                                    subUI(TSuiteObject2);
                                }

                                if (TSuiteObject2.Results != null && TSuiteObject2.Results.TSuite != null)
                                {
                                    //TestSuite-Depth:3
                                    foreach (var TSuiteObject3 in TSuiteObject2.Results.TSuite)
                                    {
                                        //Test-Fixture:Depth 3
                                        if (TSuiteObject3.Type.Equals("TestFixture"))
                                        {
                                            mainUI(TSuiteObject3);
                                        }

                                        //Test-Case:Depth 3
                                        if (TSuiteObject3.Results != null && TSuiteObject3.Results.TCase != null)
                                        {
                                            subUI(TSuiteObject3);
                                        }

                                        if (TSuiteObject3.Results != null && TSuiteObject3.Results.TSuite != null)
                                        {
                                            //TestSuite-Depth:4
                                            foreach (var TSuiteObject4 in TSuiteObject3.Results.TSuite)
                                            {
                                                //Test-fixture:Depth 4
                                                if (TSuiteObject4.Type.Equals("TestFixture"))
                                                {
                                                    mainUI(TSuiteObject4);
                                                }

                                                //Test-Case:Depth 4
                                                if (TSuiteObject4.Results != null && TSuiteObject4.Results.TCase != null)
                                                {
                                                    subUI(TSuiteObject4);
                                                }

                                                if (TSuiteObject4.Results != null && TSuiteObject4.Results.TSuite != null)
                                                {
                                                    //TestSuite-Depth:5
                                                    foreach (var TSuiteObject5 in TSuiteObject4.Results.TSuite)
                                                    {
                                                        //Text-Fixture:Depth 5
                                                        if (TSuiteObject5.Type.Equals("TestFixture"))
                                                        {
                                                            mainUI(TSuiteObject5);
                                                        }

                                                        //Test-Case:Depth 5
                                                        if (TSuiteObject5.Results != null && TSuiteObject5.Results.TCase != null)
                                                        {
                                                            subUI(TSuiteObject5);
                                                        }

                                                        if (TSuiteObject5.Results != null && TSuiteObject5.Results.TSuite != null)
                                                        {
                                                            //TestSuite-Depth:6
                                                            foreach (var TSuiteObject6 in TSuiteObject5.Results.TSuite)
                                                            {
                                                                //Test-Fixture:Depth 6
                                                                if (TSuiteObject6.Type.Equals("TestFixture"))
                                                                {
                                                                    mainUI(TSuiteObject6);
                                                                }

                                                                //Test-Case:Depth 6
                                                                if (TSuiteObject6.Results != null && TSuiteObject6.Results.TCase != null)
                                                                {
                                                                    subUI(TSuiteObject6);
                                                                }

                                                                if (TSuiteObject6.Results != null && TSuiteObject6.Results.TSuite != null)
                                                                {
                                                                    //TestSuite-Depth:7
                                                                    foreach (var TSuiteObject7 in TSuiteObject6.Results.TSuite)
                                                                    {
                                                                        //Test-Fixture:Depth 7
                                                                        if (TSuiteObject7.Type.Equals("TestFixture"))
                                                                        {
                                                                            mainUI(TSuiteObject7);
                                                                        }

                                                                        //Test-Case:Depth 7
                                                                        if (TSuiteObject7.Results != null && TSuiteObject7.Results.TCase != null)
                                                                        {
                                                                            subUI(TSuiteObject7);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                result += "</div></div></div><br />";

                selectButton.Controls.Add(new LiteralControl("<input type='submit' id='selectFiles' value='Select File' />"));
                selectButton.Controls.Add(new LiteralControl("<div id='dialog' title='Select Test Result File'>"));

                DropDownList drplist = new DropDownList();
                drplist.ID = "dropdownlistFiles";
                foreach (var file in directory.GetFiles("*.xml"))
                {
                    drplist.Items.Add(file.Name.Replace(".xml", ""));
                }

                selectButton.Controls.Add(drplist);
                selectButton.Controls.Add(new LiteralControl("</div>"));                
                
                summaryUI(TResults);
            }
            else
            {
                summary += "<h1><center>THERE ARE NO XML FILES IN THIS DIRECTORY</center></h1>";
                selectButton.Visible = false;
            }
        }

        [WebMethod]
        public static void reloadResults(string selectedfileName)
        {
            Data.xmlFileName = selectedfileName;
        }
    }

    [Serializable]
    public class TestSuite
    {
        [XmlElement("categories")]
        public Categories Categories { get; set; }

        [XmlElement("results")]
        public Results Results { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("executed")]
        public string Executed { get; set; }

        [XmlAttribute("result")]
        public string Result { get; set; }

        [XmlAttribute("success")]
        public string Success { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlAttribute("asserts")]
        public string Asserts { get; set; }
    }

    [Serializable]
    public class Categories
    {
        [XmlElement("category")]
        public Category Category { get; set; }
    }

    [Serializable]
    public class Category
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    [Serializable]
    public class TestCase
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("executed")]
        public string Executed { get; set; }

        [XmlAttribute("result")]
        public string Result { get; set; }

        [XmlAttribute("success")]
        public string Success { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlAttribute("asserts")]
        public string Asserts { get; set; }

        [XmlElement("failure")]
        public Failure Failure { get; set; }
    }

    [Serializable]
    public class Failure
    {
        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("stack-trace")]
        public string StackTrace { get; set; }
    }

    [Serializable]
    public class Results
    {
        [XmlElement("test-suite")]
        public TestSuite[] TSuite { get; set; }

        [XmlElement("test-case")]
        public TestCase[] TCase { get; set; }
    }

    [Serializable]
    public class Environmentt
    {
        [XmlAttribute("nunit-version")]
        public string NunitVersion { get; set; }

        [XmlAttribute("clr-version")]
        public string ClrVersion { get; set; }

        [XmlAttribute("os-version")]
        public string OsVersion { get; set; }

        [XmlAttribute("platform")]
        public string Platform { get; set; }

        [XmlAttribute("cwd")]
        public string Cwd { get; set; }

        [XmlAttribute("machine-name")]
        public string MachineName { get; set; }

        [XmlAttribute("user")]
        public string User { get; set; }

        [XmlAttribute("user-domain")]
        public string UserDomain { get; set; }
    }

    [Serializable]
    public class CultureInfo
    {
        [XmlAttribute("current-culture")]
        public string Culture { get; set; }

        [XmlAttribute("current-uiculture")]
        public string CurrentUiCulture { get; set; }
    }

    [Serializable]
    [XmlRoot("test-results")]
    public class TestResults
    {
        [XmlElement("environment")]
        public Environmentt Ement { get; set; }

        [XmlElement("culture-info")]
        public CultureInfo CInfo { get; set; }

        [XmlElement("test-suite")]
        public TestSuite[] TSuite { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("total")]
        public string Total { get; set; }

        [XmlAttribute("errors")]
        public string Errors { get; set; }

        [XmlAttribute("failures")]
        public string Failures { get; set; }

        [XmlAttribute("not-run")]
        public string NotRun { get; set; }

        [XmlAttribute("inconclusive")]
        public string Inconclusive { get; set; }

        [XmlAttribute("ignored")]
        public string Ignored { get; set; }

        [XmlAttribute("skipped")]
        public string Skipped { get; set; }

        [XmlAttribute("invalid")]
        public string Invalid { get; set; }

        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }
    }
}