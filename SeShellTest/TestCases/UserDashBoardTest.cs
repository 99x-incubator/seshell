using System;
using System.Threading;
using NUnit.Framework;
using SeShell.Test.Core;
using SeShell.Test.Flows;
using SeShell.Test.PageObjects;
using SeShell.Test.Enums;

namespace SeShell.Test.TestCases
{
    class UserDashBoardTest:UserDashboardPageFlow
    {
        [SetUp]
        public void Setup()
        {
            GetDriver(Configuration.BrowserType);
            Driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }


        [Test]
        [Category("Smoke")]
        //[TestCaseSource(typeof(SSLoginTestData), "TestCases")]
        public void DeleteUser()
        {
            try
            {
                LoginPageFlow loginPage =new LoginPageFlow().NavigateToLogin();
                ThreadWait.WaitUntilElementAppears(LoginPage.UserNameElement());

                HomePageFlow homePage =loginPage.LoginAsSucess("admin", "admin@123");
                ThreadWait.WaitUntilElementAppears(HomePage.LogoutElement());
                Assert.IsTrue(Driver.PageSource.Contains("Welcome admin"), "Failed User Login" + "userName");

                homePage.SelectAdminMenu();
                homePage.SelectUserDashboardMenu();
               
                UserDashboardPageFlow userdashboard = new UserDashboardPageFlow();
                ThreadWait.WaitUntilElementAppears(UserDashboardPage.GridTable());
                userdashboard.DeleteUser("john123");

                Thread.Sleep(3000);
                Driver.SwitchTo().Alert().Accept();
                Thread.Sleep(3000);
                ThreadWait.WaitUntilElementAppears(HomePage.LogoutElement());
                homePage.UserLogout();
                LogEvent("User dashbaord - Delete user_Successful() - Passed");

            }
            catch (Exception e)
            {
                new ScreenShotImage().CaptureScreenShot("DashboardDeleteUser");
                LogEvent("Userdashboard -Delete User Failed\n\r" + e.Message +
                    Environment.NewLine + e.StackTrace, EventTypes.Error);
                throw;
            }
        }


        [TearDown]
        public void TearDown()
        {
            //closing the browser.
            Driver.Quit();
        }
    }
}
