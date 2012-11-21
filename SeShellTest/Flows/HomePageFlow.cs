using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeShell.Test.Core;
using SeShell.Test.PageObjects;

namespace SeShell.Test.Flows
{
    public class HomePageFlow : BaseClass

    {
        public void UserLogout()
        {
            Driver.FindElement(By.XPath(HomePage.LogoutElement())).Click();

        }

        public void SelectAdminMenu()
        {

            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(
                By.LinkText(HomePage.AdministrationMenuElement()))).Build().Perform();

        }

        public void SelectCreateUserMenu()
        {
            Driver.FindElement(By.LinkText(HomePage.CreateUserMenuElement())).Click();
        
        }

        public void SelectUserDashboardMenu()
        {
            Driver.FindElement(By.LinkText(HomePage.DashboardMenuElement())).Click();

        }

        public void SelectContactMenu()
        {
            Driver.FindElement(By.LinkText(HomePage.GoToContactMenuElement())).Click();

        }



    }
}
