using System;

namespace SeShell.Test.PageObjects
{
    public class LoginPage
    {
        public static String UserNameElement()
        {
            return "//p/input";
        }

        public static String PasswordElement()
        {
            return "//p[2]/input";
        }

        public static String LoggedInCheckboxElement()
        {
            return "//p[3]/input";
        }

        public static String LoginElement()
        {
            return "//div[2]/p/input";
        }

    }
}
