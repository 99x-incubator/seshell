<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="SeShellTestStudio.dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>SeShell - Dashboard</title>
	<meta name="description" content="Test result dashboard" />
	<meta name="author" content="SeShell Team" />

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
</head>

<body>    
    <form id="form1" runat="server">
        <asp:ScriptManager id="ScriptManager" runat="server" enablepagemethods="true" />
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
                    <%--Dynamically generated summary box--%>        
                </div>
            </div>            
		    <div id="result_accordion" class="main" runat="server">
                <%--Dynamically generated accordions--%>
            </div>
            <div id="selectButton" class="main" runat="server">
                <%--Dynamically generated select button and dialog box--%>
            </div>           
	    </div>
        <div class="clear"><p><br /></p></div>
    </form>
    <script>
        $.fx.speeds._default = 1000;
        $(function () {
            $("input:submit").button();

            $("#dialog").dialog({
                autoOpen: false,
                show: "blind",
                hide: "explode",
                resizable: false,
                draggable: true,
                modal: true,
                buttons: {
                    "Load Results":
                    function () {
                        var fileName = $("#dropdownlistFiles").val();
                        PageMethods.reloadResults(fileName);
                        window.setTimeout('var url = window.location.href;window.location.href = url', 100);
                        $(this).dialog("close");
                        return false;
                    },
                    "Cancel":
                    function () {
                        $(this).dialog("close");
                    }
                }
            });

            $("#selectFiles").click(function () {
                $("#dialog").dialog("open");
                return false;
            });
        });
	</script>    
</body>

</html>
