using System;
using System.Collections;
using System.IO;
using SeShell.Test.Core;

namespace SeShell.Test.TestData.Data
{
    class LoginTestData
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String ExpectedResult { get; set; }
        public String ErrImage { get; set; }

        public static IEnumerable TestCases
        {
            get
            {
                string inputLine;
                using (FileStream inputStream =
                    new FileStream(Configuration.TestDataFilePath+@"\SSLoginData.csv",
                        FileMode.Open,
                        FileAccess.Read))
                {
                    StreamReader streamReader = new StreamReader(inputStream);

                    while ((inputLine = streamReader.ReadLine()) != null)
                    {
                        var data = inputLine.Split(',');
                        yield return new LoginTestData
                        {
                          
                          UserName = Convert.ToString((data[0])),
                          Password = Convert.ToString((data[1])),
                          ExpectedResult= Convert.ToString(data[2]),
                          ErrImage = Convert.ToString(data[3])
                        };
                    }

                    streamReader.Close();
                    inputStream.Close();
                }
            }
        }
    }
    }


