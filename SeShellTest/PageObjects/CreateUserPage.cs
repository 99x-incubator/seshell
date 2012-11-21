

using System;

namespace SeShell.Test.PageObjects
{
    public class CreateUserPage : LoginPage
    {
        public static String UserNameElement()
        {
            return "//td[2]/input";
        }

        public static String FirstNameElement()
        {
            return "//tr[2]/td[2]/input";
        }

        public static String LastNameElement()
        {
            return "//tr[3]/td[2]/input";
        }

        public static String SexMaleElement()
        {
            return "//span/input";
        }

        public static String SexFemaleElement()
        {
            return "//span/input[2]";
        }

        public static String DobElement()
        {
            return "//tr[5]/td[2]/input";
        }

        public static String CountryDropDownElement()
        {
            return "//select";
        }

        public static String AdminCheckBoxElement()
        {
            return "//tr[7]/td[2]/input";
        }

       
        //****************************
        public static String SaveButtonElement()
        {
            return "//fieldset/input";
        }
        
        public static String ClearButtonElement()
        {
            return "//fieldset/input[2]";
        }
       
        public static String CancelButtonElement()
        {
            return "//fieldset/input[3]";
        }
    }
}
