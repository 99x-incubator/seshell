using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.SessionState;
using SeShellTestStudio.Utils;

namespace SeShellTestStudio
{
    public partial class Configure : System.Web.UI.Page
    {
        private List<TextBox> txtBoxList = new List<TextBox>();
        private List<Control> hiddenControlsList = new List<Control>();
        private const string cIexplorer = "IE";
        private const string cFirefox = "Firefox";
        private const string cChrome = "Chrome";
        private const string cSafari = "Safari";
        private const string cOpera = "Opera";
        private DropDownList drpList;
        private Dictionary<string, string> data;
        private const int noOfNewControls = 3;
        private string fileName;
        private List<String> baseValues = new List<string>();
        private Boolean canContinue;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetSettings settings = new GetSettings();
            fileName = settings.GetConfigFilePath(Server.MapPath("~"));
            InitBaseValues();
            ReadConfigData(settings.GetConfigFilePath(Server.MapPath("~")));
        }

        private void InitBaseValues()
        {
            baseValues.Add("BaseUrl");
            baseValues.Add("LoginPage");
            baseValues.Add("AdminSiteUrl");
            baseValues.Add("BrowserType");
            baseValues.Add("PortNumber");
            baseValues.Add("ErrorImagePath");
            baseValues.Add("TestDataDirectory");
            baseValues.Add("TestDataUploadDirectory");
            baseValues.Add("ResponseTimeInSeconds");
            baseValues.Add("TestReslutDirectory");
        }

        void ReadConfigData(string filePathName)
        {
            string xml = System.IO.File.ReadAllText(filePathName);
            data = XElement.Parse(xml)
            .Elements("add")
            .ToDictionary(
                el => (string)el.Attribute("key"),
                el => (string)el.Attribute("value")
            );

            foreach (var obj in data)
            {
                Area1.Controls.Add(new LiteralControl("<p><dd>"));
               
                Label tempLabel = new Label();
                tempLabel.Text = ConvertToHumanReadable(obj.Key) + ":";
                tempLabel.CssClass = "ui-widget-header";
                Area1.Controls.Add(tempLabel);
                Area1.Controls.Add(new LiteralControl("</dd>"));
                Area1.Controls.Add(new LiteralControl("<dd>"));

                if (obj.Key.Equals("BrowserType"))
                {
                    drpList = new DropDownList();
                    drpList.ID = "drpList";
                    drpList.Items.Add(cIexplorer);
                    drpList.Items.Add(cFirefox);
                    drpList.Items.Add(cChrome);
                    //drpList.Items.Add(cSafari);
                    //drpList.Items.Add(cOpera);

                    if (!obj.Value.Equals(""))
                    {
                        drpList.Items.FindByValue(obj.Value).Selected = true;
                    }
                    else
                    {
                        drpList.Items.FindByValue(cIexplorer).Selected = true;
                    }
                    drpList.CssClass = "ui-widget input";
                    Area1.Controls.Add(drpList);
                }
                else
                {
                    TextBox tempTxtbox = new TextBox();
                    tempTxtbox.Text = obj.Value;
                    tempTxtbox.ID = "txt" + obj.Key;
                    switch (obj.Key)
                    {
                        case "BaseUrl":
                            tempTxtbox.Width = 300;
                            break;

                        case "PortNumber": 
                        case "ResponseTimeInSeconds":
                            tempTxtbox.Width = 60;
                            break;

                        case "ErrorImagePath":
                        case "TestDataUploadDirectory": 
                        case "TestDataDirectory":
                            tempTxtbox.Width = 750;
                            break;

                         default:
                            tempTxtbox.Width = 200;
                            break;
                    }
                    tempTxtbox.Height = 25;
                    tempTxtbox.CssClass = "ui-widget input";
                    Area1.Controls.Add(tempTxtbox);
                    txtBoxList.Add(tempTxtbox);
                    if (!BelongsToBaseValues(obj.Key))
                    {
                        CheckBox chkRemove = new CheckBox();
                        chkRemove.ID = "chk" + obj.Key;
                        Area1.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                        Area1.Controls.Add(chkRemove);
                        Area1.Controls.Add(new LiteralControl("<label for='" + "chk" + obj.Key + "'><font color='#FF0000'>X</font></label>"));
                    }
                }
                Area1.Controls.Add(new LiteralControl("</dd></p><br />"));
            }

            AddHiddenControls();

            Area2.Controls.Add(new LiteralControl("<table width=60% align='center'>"));
            Area2.Controls.Add(new LiteralControl("<tr align='center'>"));
            
            Area2.Controls.Add(new LiteralControl("<td width=33%>"));                      
            Button btnReset = new Button();
            btnReset.Text = "Remove";
            btnReset.Click += new System.EventHandler(this.btnRemove_Click);
            Area2.Controls.Add(btnReset);
            Area2.Controls.Add(new LiteralControl("</td>"));

            Area2.Controls.Add(new LiteralControl("<td width=33%>"));  
            Button btnAdd = new Button();
            btnAdd.Text = "Add";
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            Area2.Controls.Add(btnAdd);
            Area2.Controls.Add(new LiteralControl("</td>"));

            Area2.Controls.Add(new LiteralControl("<td width=33%>"));  
            Button btnSubmit = new Button();
            btnSubmit.Text = "Save";
            btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            Area2.Controls.Add(btnSubmit);
            Area2.Controls.Add(new LiteralControl("</td>"));
            
            Area2.Controls.Add(new LiteralControl("</tr>"));
            Area2.Controls.Add(new LiteralControl("</table><br />"));
        }

        private bool BelongsToBaseValues(string p)
        {
            Boolean res = false;
            if (baseValues.Contains(p))
            {
                res = true;
            }
            return res;
        }

        private void AddHiddenControls()
        {
            for (int i = 0; i < noOfNewControls; i++)
            {
                TextBox txtKey = new TextBox();
                txtKey.Width = 200;
                txtKey.ID = "txtKey" + i;
                txtKey.CssClass = "ui-widget input";

                TextBox txtValue = new TextBox();
                txtValue.Width = 200;
                txtValue.ID = "txtValue" + i;
                txtValue.CssClass = "ui-widget input";

                HiddenArea.Controls.Add(new LiteralControl("<div><dd>"));

                Label labelKey = new Label();
                labelKey.Text = "Key:";                 
                labelKey.CssClass = "ui-widget-header";
                HiddenArea.Controls.Add(labelKey);
                HiddenArea.Controls.Add(new LiteralControl("&nbsp;"));
                HiddenArea.Controls.Add(txtKey);
                HiddenArea.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));

                Label labelValue = new Label();
                labelValue.Text = "Value:";
                labelValue.CssClass = "ui-widget-header";
                HiddenArea.Controls.Add(labelValue);
                HiddenArea.Controls.Add(new LiteralControl("&nbsp;"));
                HiddenArea.Controls.Add(txtValue);
                HiddenArea.Controls.Add(new LiteralControl("</dd></div><br/>"));

                hiddenControlsList.Add(txtKey);
                hiddenControlsList.Add(txtValue);
            }
            HiddenArea.Visible = false;
        }

        static String ConvertToHumanReadable(String s)
        {
            Regex r = new Regex(@"(?!^)(?=[A-Z])");
            return r.Replace(s, " ");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveAllConfig();
        }

        private void SaveAllConfig()
        {
            SaveChanges();
            WriteToFile();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',100);", true);
        }

        private void WriteToFile()
        {
            try
            {
                string[] tmpData = data.Select(x => "\t<add key=\"" + x.Key + "\" value=\"" + x.Value + "\"/>").ToArray();
                string[] arrCopy = new string[500];
                arrCopy[0] = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                arrCopy[1] = "<appSettings>";
                for (int i = 0; i < tmpData.Length; i++)
                {
                    arrCopy[i + 2] = tmpData[i];
                }
                arrCopy[tmpData.Length + 2] = "</appSettings>";
                File.WriteAllLines(fileName, arrCopy);                
                Area1.Controls.Add(new LiteralControl("<script>alert('Configuration is successfully saved')</script>"));
            }
            catch (Exception e)
            {
                Area1.Controls.Add(new LiteralControl("<script>alert('An error occured when saving the configurations')</script>"));
            }
        }

        private void ReadFromUI()
        {
            int i = 0;
            for (int j = 0; j < data.Count; j++)
            {
                var obj = data.ElementAt(j);
                if (obj.Key.Equals("BrowserType"))
                {
                    data[obj.Key] = drpList.SelectedItem.Text;
                }
                else
                {
                    data[obj.Key] = txtBoxList[i].Text;
                    i++;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ShowHiddenControls();
        }

        private void ShowHiddenControls()
        {
            HiddenArea.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "window.scrollTo(0, document.body.scrollHeight);", true);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tmpDataD = new Dictionary<string, string>();
            foreach (var it in data)
            {
                tmpDataD.Add(it.Key, it.Value);
            }

            foreach (var item in data)
            {
                if (!BelongsToBaseValues(item.Key))
                {
                    CheckBox chk = (CheckBox)Area1.FindControl("chk" + item.Key);
                    if (chk.Checked == true)
                    {
                        tmpDataD.Remove(item.Key);
                    }
                }
            }
            data = tmpDataD;
            SaveAllConfig();
        }

        private void SaveChanges()
        {
            for (int i = 0; i < data.Count; i++)
            {
                string value;
                if (i < baseValues.Count && baseValues[i].Equals("BrowserType"))
                {
                    value = drpList.SelectedItem.Text;
                    data[baseValues[i]] = value;
                }
                else
                {
                    if (i < baseValues.Count)
                    {
                        value = ((TextBox)Area1.FindControl("txt" + baseValues[i])).Text;
                        data[baseValues[i]] = value;
                    }
                    else
                    {
                        value = ((TextBox)Area1.FindControl("txt" + data.ElementAt(i).Key)).Text;
                        data[data.ElementAt(i).Key] = value;
                    }
                }
            }

            for (int i = 0; i < noOfNewControls; i++)
            {
                TextBox tmpKey = (TextBox)HiddenArea.FindControl("txtKey" + i);
                TextBox tmpValue = (TextBox)HiddenArea.FindControl("txtValue" + i);
                String str = tmpKey.Text;
                str = str.Replace(" ", "");

                canContinue = Validate(tmpKey.Text);

                if (canContinue)
                {
                    string key = str;
                    string value = tmpValue.Text;

                    data.Add(key, value);

                    tmpKey.Text = str;
                    tmpKey.Enabled = false;
                    tmpKey.BorderWidth = 0;
                    tmpKey.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private Boolean Validate(String tmpKey)
        {
            Boolean result = false;
            if (!tmpKey.Equals("") && !checkForDuplicates(tmpKey))
            {
                result = true;
            }
            else if (tmpKey.Equals(""))
            {
                //No need
            }
            else
            {
                Area1.Controls.Add(new LiteralControl("<script>alert('Key " + tmpKey + " already exists!')</script>"));
            }
            return result;
        }

        private bool checkForDuplicates(String str)
        {
            if (data.ContainsKey(str))
                return true;
            else
                return false;
        }
    }
}