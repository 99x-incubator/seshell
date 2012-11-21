

using System;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using SeShell.Test.Core;
using SeShell.Test.Enums;
using SeShell.Test.Flows;
using SeShell.Test.PageObjects;

namespace SeShell.Test.TestCases
{
    class CreateUserTest : CreateUserPageFlow
    {
        //public new static WebBrowsers WebBrws = WebBrowsers.None;

        [SetUp]
        public void Setup()
        {
            GetDriver(Configuration.BrowserType);
            Driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }


        [Test]
        [Category("Smoke")]
        public void CreateUser()
        {
            try
            {
                LoginPageFlow loginPage =new LoginPageFlow().NavigateToLogin();
                ThreadWait.WaitUntilElementAppears(LoginPage.LoginElement());
                HomePageFlow homePage =loginPage.LoginAsSucess("admin", "admin@123");
                ThreadWait.WaitUntilElementAppears(HomePage.LogoutElement());
                Assert.IsTrue(Driver.PageSource.Contains("Welcome admin"),
                    "Failed User Login" + "userName");

                if (WebBrws == WebBrowsers.FireFox)
                {
                    Driver.Navigate().GoToUrl("http://localhost:57081/CrystalRunner/AdminPages/CreateUser.aspx");
                }
                else
                {
                    homePage.SelectAdminMenu();
                    Thread.Sleep(3000);
                    homePage.SelectCreateUserMenu();
                }

                CreateUserPageFlow createUser = new CreateUserPageFlow();
                createUser.RegisterInformation("tom","Tom","Larry","10/11/1990","Sri Lanka");

                String parentWindowHandle = Driver.CurrentWindowHandle;
                ReadOnlyCollection<string> handles = Driver.WindowHandles;
                createUser.SaveUserInformation();


                foreach (string handle in handles)
                {

                }

                Driver.SwitchTo().Alert().Accept();
                Thread.Sleep(3000);
                homePage.UserLogout();
                LogEvent("Create User - CreateUser Successful() - Passed");

            }
            catch (Exception e)
            {
                new ScreenShotImage().CaptureScreenShot("CreateUserError");
                LogEvent("CreateUser -CreateUser() Failed\n\r" + e.Message +
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
