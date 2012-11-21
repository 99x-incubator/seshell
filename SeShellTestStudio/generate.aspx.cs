using System;
using SeShellTestStudio.Utils;

namespace SeShellTestStudio
{
    public partial class generate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //No code
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Do any related intialization work.
            GetSettings settings = new GetSettings();
            hfResultFolder.Value = settings.GetResultPath(Server.MapPath("~"));
        }
    }
}