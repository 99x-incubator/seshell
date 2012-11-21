using System;
using OpenQA.Selenium;

namespace SeShell.Test.Core
{
    internal class ScreenShotImage : BaseClass
    {
        /// <summary>
        /// Capture and store the image in jpg format
        /// </summary>
        /// <param name="imageName"></param>
        public void CaptureScreenShot(String imageName)
        {
            String imagePath = Configuration.ErrorImagePath +"\\" + imageName + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss.")+DateTime.Now.Millisecond.ToString()+".jpg";
            Screenshot ss = ((ITakesScreenshot) Driver).GetScreenshot();
            ss.SaveAsFile(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        /// <summary>
        /// delete the given file
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(String filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
