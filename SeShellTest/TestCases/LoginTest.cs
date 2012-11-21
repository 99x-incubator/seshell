using System;
using System.Threading;
using NUnit.Framework;
using SeShell.Test.Core;
using SeShell.Test.Enums;
using SeShell.Test.Flows;
using SeShell.Test.PageObjects;
using SeShell.Test.TestData.Data;

namespace SeShell.Test.TestCases
{
    class LoginTest : LoginPageFlow
    {
        [SetUp]
        public void Setup()
        {
            GetDriver(Configuration.BrowserType);
            Driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }


        [Test]
        [Category("Smoke")]
        [TestCaseSource(typeof(LoginTestData), "TestCases")]
        public void LoginSuccessful(LoginTestData data)
        {
            try
            {
                LoginPageFlow loginPage = NavigateToLogin();
                Thread.Sleep(3000);
                HomePageFlow homePage = LoginAsSucess(data.UserName, data.Password);
                ThreadWait.WaitUntilElementAppears(HomePage.LogoutElement());
                Assert.IsTrue(Driver.PageSource.Contains(data.ExpectedResult),
                    "Failed User Login" + data.UserName);
                homePage.UserLogout();
                LogEvent("LoginTest -Login_Successful() - Passed");

            }
            catch (Exception e)
            {
                new ScreenShotImage().CaptureScreenShot(data.ErrImage);
                LogEvent("LoginTest -Login Failed\n\r" + e.Message +
                    Environment.NewLine + e.StackTrace, EventTypes.Error);
                throw;
            }
        }


        [Test]
        [Category("Smoke")]
        [TestCaseSource(typeof(LoginErrorTestData), "TestCases")]
        public void LoginUnSuccessful(LoginErrorTestData data)
        {
            try
            {
                LoginPageFlow loginPage = NavigateToLogin();
                Thread.Sleep(3000);
                LoginPageFlow homePage = LoginAsUnSucess(data.UserName, data.Password);
                Assert.IsTrue(Driver.PageSource.Contains(data.ExpectedResult),
                    "Failed User Login" + data.UserName);
                LogEvent("LoginTest -Login Unsuccessful - Passed ");

            }
            catch (Exception e)
            {
                new ScreenShotImage().CaptureScreenShot(data.ErrImage);
                LogEvent("LoginTest -Login Unsuccessful Failed\n\r" + e.Message +
                    Environment.NewLine + e.StackTrace, EventTypes.Error);
                throw;
            }
        }



        [TearDown]
        public void TearDown()
        {
            // closing the browser.
            Driver.Quit();
        }
    }
}
