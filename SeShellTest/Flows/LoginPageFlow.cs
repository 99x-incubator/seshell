using OpenQA.Selenium;
using SeShell.Test.Core;
using SeShell.Test.PageObjects;

namespace SeShell.Test.Flows
{
    public class LoginPageFlow : BaseClass
    {
        public LoginPageFlow NavigateToLogin()
        {
            NavigateTo(Configuration.LoginPage.ToString());
            return new LoginPageFlow();
        }

        public void LoginAs(string username, string password)
        {
           
            Driver.FindElement(By.XPath(LoginPage.UserNameElement())).SendKeys(username);
            Driver.FindElement(By.XPath(LoginPage.PasswordElement())).SendKeys(password);
            Driver.FindElement(By.XPath(LoginPage.LoginElement())).Click();

        }

        public HomePageFlow LoginAsSucess(string username, string password)
        {
            LoginAs(username, password);
            return new HomePageFlow();
        }

        public LoginPageFlow LoginAsUnSucess(string username, string password)
        {
            LoginAs(username, password);
            return new LoginPageFlow();
        }

        public HomePageFlow LoginAsAdminSucess()
        {
            LoginAs("admin", "admin@123");
            return new HomePageFlow();
        }


    }
}
