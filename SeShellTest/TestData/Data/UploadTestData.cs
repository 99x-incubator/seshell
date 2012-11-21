using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using SeShell.Test.Core;

namespace SeShell.Test.TestData.Data
{
    public class UploadTestData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginWelcome { get; set; }
        public string FileName { get; set; }
        
        public static IEnumerable UploadTestCases
        {
            get
            {
                string inputLine;
                using (FileStream inputStream =
                    new FileStream(Configuration.TestDataFilePath + @"\SSUploadData.csv",
                        FileMode.Open,
                        FileAccess.Read))
                {
                    StreamReader streamReader = new StreamReader(inputStream);

                    while ((inputLine = streamReader.ReadLine()) != null)
                    {
                        var data = inputLine.Split(',');
                        yield return new UploadTestData
                        {
                            UserName = Convert.ToString((data[0])),
                            Password = Convert.ToString((data[1])),
                            LoginWelcome= Convert.ToString((data[2])),
                            FileName = Convert.ToString((data[3]))
                        };
                    }

                    streamReader.Close();
                    inputStream.Close();
                }
            }
        }
    }
}
