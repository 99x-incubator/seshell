using System;

namespace SeShell.Test.PageObjects
{
    public class ContactPage
    {
        public static String FilePathElement()
        {
            return "//*[@id='MainContent_FileUploader']";
        }

        public static String UploadButtonElement()
        {
            return "//*[@id='MainContent_UploadButton']";
        }
    }
}
