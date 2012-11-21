<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="generate.aspx.cs" Inherits="SeShellTestStudio.generate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Selenium Shell - Generate</title>
	<meta name="description" content="Generate batch file" />
	<meta name="author" content="Selenium Shell Team" />

	<link href="css/basic_styles.css" rel="Stylesheet" type="text/css"/>
	<style type="text/css">
	    body 
	    {
		    margin-top: 0px;
		    background-image: url(images/top.jpg);
		    background-repeat: repeat-x;
		    background-position: top;
		    background-color: #e4e4e4;
	    }
	</style>
    <link href="jquery/css/custom-theme/jquery-ui-1.9.0.custom.css" rel="Stylesheet" type="text/css"/>

    <script src="jquery/js/jquery-1.8.2.js"></script>
    <script src="jquery/js/jquery-ui-1.9.0.custom.min.js"></script> 
    <script type="text/javascript" src="downloadify/js/swfobject.js"></script>
	<script type="text/javascript" src="downloadify/src/downloadify.js"></script> 
    <script type="text/javascript">
        function load() {
            Downloadify.create('downloader', {
                filename: function () {
                    return document.getElementById('<%=textboxAssembly.ClientID%>').value + ".bat";                    
                },
                data: function () {
                    //return "start /d \"" + document.getElementById('<%=textboxPath.ClientID%>').value + "\" nunit-console /xml:TestResult_%date:~-4,4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%.xml /fixture:" + document.getElementById('<%=textboxNamespace.ClientID%>').value + "." + document.getElementById('<%=textboxSuiteClass.ClientID%>').value + " " + document.getElementById('<%=textboxAssembly.ClientID%>').value + ".dll /work:\"C:\\AdraMatch_TestResults\"";
                    return "start /d \"" + document.getElementById('<%=textboxPath.ClientID%>').value + "\" nunit-console /xml:TestResult_%date:~-4,4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%.xml /fixture:" + document.getElementById('<%=textboxNamespace.ClientID%>').value + "." + document.getElementById('<%=textboxSuiteClass.ClientID%>').value + " " + document.getElementById('<%=textboxAssembly.ClientID%>').value + ".dll /work:\""+ document.getElementById('hfResultFolder').value + "\"";
                },
                onComplete: function () { alert('Your file has been saved successfully'); clear(); },
                onCancel: function () { },
                onError: function () { alert('There was an error when saving the file'); },
                swf: 'downloadify/media/downloadify.swf',
                downloadImage: 'downloadify/images/generate.png',
                width: 115,
                height: 41,
                transparent: true,
                append: false
            });
        }

        function resetFunction() {
            clear();
        }

        function clear() {
            document.getElementById('<%=textboxPath.ClientID%>').value = "";
            document.getElementById('<%=textboxNamespace.ClientID%>').value = "";
            document.getElementById('<%=textboxSuiteClass.ClientID%>').value = "";
            document.getElementById('<%=textboxAssembly.ClientID%>').value = "";
        }
    </script>       
</head>

<body onload="load();">           
    <form id="form1" runat="server">
	    <div class="wrapper-container">
		    <div id="header">
		        <center><img src="images/logo.png" alt="logo"/></center>
		    </div>  
		    <div id="content" class="content-wrap">
			    <div id="nav">
				    <div class="menu">
					    <ul>
						    <li><a href="dashboard.aspx"><span>DASHBOARD</span></a></li>
						    <li><a href="configure.aspx"><span>CONFIG</span></a></li>
						    <li><a href="generate.aspx"><span>GENERATE</span></a></li>
					    </ul>
				    </div>    
			    </div>
		    </div>           
            <div class="main" runat="server">
                <div id="summary_box" class="ui-widget-box" runat="server">
                    <center><h1>Generate Test Fixture Bat File</h1></center><br/>
                    <p>
                        <dd><asp:Label id="labelboxPath" text="Path of the N-Unit Console: " class="ui-widget-header" runat="server" />
                        <dd><asp:TextBox id="textboxPath" runat="server" width="800px" height="25px" class="ui-widget input"/></dd></dd>
                    </p>
                    <br />
                    <p>
                        <dd><asp:Label id="labelboxSuiteClass" text="Suite Class: " class="ui-widget-header" runat="server" />
                        <dd><asp:TextBox id="textboxSuiteClass" runat="server" width="500px" height="25px" class="ui-widget input"/></dd></dd>
                    </p>
                    <br />
                    <p>
                        <dd><asp:Label id="labelboxNamespace" text="Namespace of the Suite Class: " class="ui-widget-header" runat="server" />
                        <dd><asp:TextBox id="textboxNamespace" runat="server" width="500px" height="25px" class="ui-widget input"/></dd></dd>
                    </p>
                    <br />
                    <p>
                        <dd><asp:Label id="labelboxAssembly" text="Assembly name (DLL): " class="ui-widget-header" runat="server" />
                        <dd><asp:TextBox id="textboxAssembly" runat="server" width="500px" height="25px" class="ui-widget input"/></dd></dd>
                    </p>
                    <br />
                    <table width=50% align="center">
                        <tr align="center">
                            <td width=50%>
                                <p id="downloader"></p>
                            </td>
                            <td width=50%>
                                <input type="submit" id="buttonReset" onclick="resetFunction(); return false;" value="Reset" />
                                <asp:HiddenField ID="hfResultFolder" runat="server" />
                            </td>
                        </tr>    
                    </table>                                                                                                                  
                </div>
            </div>          
	    </div>
        <div class="clear"><p><br /></p></div>
    </form>
     <script>
         $(function () {
             $("input:submit").button();
         });
	</script>
</body>

</html>
