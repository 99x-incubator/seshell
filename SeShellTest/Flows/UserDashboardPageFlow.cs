using OpenQA.Selenium;
using SeShell.Test.Core;
using SeShell.Test.PageObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SeShell.Test.Flows
{
    public class UserDashboardPageFlow : BaseClass
    {

        public void DeleteUser(string userName)
        {
            IWebElement table_element = Driver.FindElement(By.XPath(UserDashboardPage.GridTable()));
            ReadOnlyCollection<IWebElement> tr_collection = table_element.FindElements(By.XPath(UserDashboardPage.GridTableRow()));

            for (int i = 2; i < tr_collection.Count; i++)
            {
                if (Driver.FindElement(By.XPath( UserDashboardPage.UserNameColumofRow(i))).Text.Contains(userName))
                {
                    Driver.FindElement(By.XPath(UserDashboardPage.DeleteButtonColumofRow(i))).Click();
                }
                
            }
        }
    }
}
