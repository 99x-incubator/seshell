using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeShell.Test.Core;
using SeShell.Test.PageObjects;

namespace SeShell.Test.Flows
{
    public class CreateUserPageFlow : BaseClass
    {
        public void RegisterInformation(string userName, string firstName, string lastName, string dateOfBirth, string countryName )
        {
            Driver.FindElement(By.XPath(CreateUserPage.UserNameElement())).SendKeys(userName);
            Driver.FindElement(By.XPath(CreateUserPage.FirstNameElement())).SendKeys(firstName);
            Driver.FindElement(By.XPath(CreateUserPage.LastNameElement())).SendKeys(lastName);
            Driver.FindElement(By.XPath(CreateUserPage.SexMaleElement())).Click();
            Driver.FindElement(By.XPath(CreateUserPage.DobElement())).SendKeys(dateOfBirth);

            //country selection
            SelectElement country = new SelectElement(Driver.FindElement(By.XPath(CreateUserPage.CountryDropDownElement())));
            country.SelectByText(countryName);

            Driver.FindElement(By.XPath(CreateUserPage.AdminCheckBoxElement())).Click();
        }

        public void SaveUserInformation()
        {
            Driver.FindElement(By.XPath(CreateUserPage.SaveButtonElement())).Click();
        }

        public void CleareUserInformation()
        {
            Driver.FindElement(By.XPath(CreateUserPage.ClearButtonElement())).Click();
        }

        public void CancelUserInformation()
        {
            Driver.FindElement(By.XPath(CreateUserPage.CancelButtonElement())).Click();
        }
    }
}
