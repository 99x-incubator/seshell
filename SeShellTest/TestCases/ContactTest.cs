using System;
using System.Threading;
using NUnit.Framework;
using SeShell.Test.Core;
using SeShell.Test.Flows;
using SeShell.Test.PageObjects;
using SeShell.Test.TestData.Data;
using SeShell.Test.Enums;

namespace SeShell.Test.TestCases
{
    internal class ContactTest : ContactPageFlow
    {
        [SetUp]
        public void Setup()
        {
            GetDriver(Configuration.BrowserType);
            Driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }

        [Test]
        [Category("Smoke")]
        public void ContactUploader()
        {
            try
            {
                LoginPageFlow loginPage = new LoginPageFlow();
                Thread.Sleep(3000);
                loginPage.NavigateToLogin();
                HomePageFlow homePage = loginPage.LoginAsAdminSucess();
                ThreadWait.WaitUntilElementAppears(HomePage.LogoutElement());
                Assert.IsTrue(Driver.PageSource.Contains("Welcome admin"), "Failed User Login admin");
                
                homePage.SelectContactMenu();

                ContactPageFlow contacts = new ContactPageFlow();
                contacts.UploadFile(Configuration.TestDataUploadDirectory+ "\\SeShellDemo1.txt");
                Thread.Sleep(3000);
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception e)
            {
                new ScreenShotImage().CaptureScreenShot("Upload");
                LogEvent("Upload -Upload Failed\n\r" + e.Message +
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
