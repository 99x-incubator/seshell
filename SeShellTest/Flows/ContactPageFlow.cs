using System.Threading;
using OpenQA.Selenium;
using SeShell.Test.Core;
using SeShell.Test.PageObjects;

namespace SeShell.Test.Flows
{
    public class ContactPageFlow : BaseClass
    {
        public void UploadFile(string filePath)
        {
            Driver.FindElement(By.XPath(ContactPage.FilePathElement())).SendKeys(filePath);
            Thread.Sleep(5000);
            Driver.FindElement(By.XPath(ContactPage.UploadButtonElement())).Click();
        }
    }
}
