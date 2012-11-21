
using System;

namespace SeShell.Test.PageObjects
{
    public class UserDashboardPage
    {

        public static String GridTable()
        {
            return "//*[@id='MainContent_GridView1']";
        }

        public static String GridTableRow()
        {
            return "//html/body/form/div[3]/div[2]/div/table/tbody/tr";
        }

        public static String UserNameColumofRow(int rowId)
        {
            return GridTableRow()+ "[" + rowId.ToString() + "]/td[2]";
        }

        public static String DeleteButtonColumofRow(int rowId)
        {
            return GridTableRow()+ "[" + rowId.ToString() + "]//td[9]/center/a";
        }
    }
}
