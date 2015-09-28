using SeShell.Test.XMLTestResult.Interface;
using SeShell.Test.XMLTestResult.Resources;
using SeShell.Test.XMLTestResult.XMLObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace SeShell.Test.XMLTestResult
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Report(string.Empty, TestResultResources.FinalResultLocation);
        }

        public static bool Report(string tempFileName, string reportLocation)
        {
            ClearXmlRepo(tempFileName);

            // get a list of all xml files witin the folder based on the class name
            string[] classNames = TestResultResources.ClassesToExecute.Split(',');
            var classNameTestSuiteDictionary = new Dictionary<string, ResultTestSuite>();

            foreach (var classString in classNames)
            {
                string[] dirs = Directory.GetFiles(TestResultResources.NUnitModifiedTestResultLocation, string.Format("{0}*", classString));
                if (dirs.Length > 0)
                {
                    var suitesPerClass = dirs.Select(DeserializeXmlObject).ToList();

                    classNameTestSuiteDictionary.Add(classString, CombinedResultsPerTest(classString, suitesPerClass));
                }
            }

            var res = CreateSkeletonXmlStructure();

            var idk = classNameTestSuiteDictionary.Select(x => x.Value);
            res.TestSuite.Results.TestSuites[0].Results.TestSuites = idk.ToArray();

            res.Total =
                res.TestSuite.Results.TestSuites[0].Results.TestSuites.Sum(x => x.Results.TestCases.Count()).ToString();
            res.Errors = res.TestSuite.Results.TestSuites[0].Results.TestSuites.Sum(x => x.Results.TestCases.Count(n => !n.Success)).ToString();

            // get a list of multiple tests and then sort the tests according to the classname
            // on completion of that proceed to write the whole bunch of tests into one large xml file
            // save the xml file in the location where jenkins would be looking for
            WriteFinalToFile(res, reportLocation);

            return (classNameTestSuiteDictionary.Count > 0);
        }

        public static TestResults CreateSkeletonXmlStructure()
        {
            DateTime currentReportTime = DateTime.Now;
            var res = new TestResults
            {
                TestSuite = new ResultTestSuite { Results = new Results { TestSuites = new ResultTestSuite[1] }, Executed = true, Success = true},
                Date = currentReportTime.ToString("d"),
                Time = currentReportTime.ToString("hh:mm:ss tt"),
                Name = TestResultResources.AutomationResultNamespace,                
            };

            res.TestSuite.Results.TestSuites[0] = new ResultTestSuite
            {
                Name = TestResultResources.AutomationResultNamespace,
                Success = true,
                Executed = true,
                Results = new Results()
            };

            res.TestSuite.Results.TestSuites[0].Results = new Results();
            return res;
        }

        public static void WriteFinalToFile(ITestResults foo, string fileName)
        {
            var serializer =
                new XmlSerializer(foo.GetType());

            using (var writer = new StreamWriter(string.Format("{0}{1}.xml", fileName, DateTime.Now.Ticks)))
            {
                serializer.Serialize(writer, foo);
            }
        }

        public static void ClearXmlRepo(string fileName = null)
        {
            // delete files from the shared location
            var directory = new DirectoryInfo(TestResultResources.FinalResultLocation);
            foreach (FileInfo file in directory.GetFiles())
            {
                if (fileName == null || file.Name == fileName)
                {
                    while (IsFileLocked(file))
                        Thread.Sleep(1000);
                    file.Delete();
                }
            }
        }

        protected static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// Combineds the results per test.
        /// </summary>
        /// <param name="testClassName">Name of the test class.</param>
        /// <param name="list">The list.</param>
        /// <returns>
        /// A cumulative object which contains all the tests
        /// executed for a particular test class
        /// </returns>
        public static ResultTestSuite CombinedResultsPerTest(string testClassName, List<ResultTestSuite> list)
        {
            var suite = new ResultTestSuite();
            var results = new Results();
            var testCaseList = new List<TestCase>();

            foreach (var items in list)
            {
                testCaseList.AddRange(items.Results.TestCases);
            }

            suite.Name = testClassName;
            results.TestCases = testCaseList.ToArray();
            suite.Results = results;
            suite.Time = testCaseList.Sum(x => Convert.ToInt32(x.Time)).ToString(CultureInfo.InvariantCulture);
            suite.Executed = true;
            suite.Asserts = testCaseList.Sum(x => int.Parse(x.Asserts)).ToString();
            suite.Success = !testCaseList.Any(x => !x.Success);

            // check and write this to file
            return suite;
        }

        public static void WriteToFile(ITestResults foo, string fileName)
        {
            var serializer =
                new XmlSerializer(foo.GetType());

            var writer = new StreamWriter(string.Format("{0}-{1}.xml", fileName, DateTime.Now.Ticks));

            serializer.Serialize(writer, foo);
            writer.Close();
            writer.Dispose();
        }

        public static ResultTestSuite DeserializeXmlObject(string fileName)
        {
            var mySerializer =
                new XmlSerializer(typeof(ResultTestSuite));
            bool fileLock = true;
            FileStream myFileStream = null;
            while (fileLock)
            {
                try
                {
                    myFileStream =
                  new FileStream(fileName, FileMode.Open);
                    fileLock = false;
                }
                catch (IOException)
                {
                    //the file is unavailable because it is:
                    //still being written to
                    //or being processed by another thread
                    //or does not exist (has already been processed)
                    // return true;
                    Thread.Sleep(2);
                }
            }
           
          
            return (ResultTestSuite)
                mySerializer.Deserialize(myFileStream);
        }
    }
}