using System;

namespace SeShell.Test.PageObjects
{
    public class HomePage
    {
        
        public static String LogoutElement()
        {
            return "//a";
        }


        public static String AdministrationMenuElement()
        {
            return "Administration";
        }

        public static String CreateUserMenuElement()
        {
            return "Create User";
        }

        public static String GoToContactMenuElement()
        {
            return "Contact";
        }

        public static String DashboardMenuElement()
        {
            return "Dashboard";
        }
        
    }
}
