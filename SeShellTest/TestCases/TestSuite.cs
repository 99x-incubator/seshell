
using System.Collections;
using NUnit.Framework;

namespace SeShell.Test.TestCases
{
    [TestFixture]
    class TestSuite
    {
        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();

                suite.Add(typeof(LoginTest));
                suite.Add(typeof(CreateUserTest));
                suite.Add(typeof(ContactTest));
                suite.Add(typeof(UserDashBoardTest));


                return suite;


            }
        }
    }
}
