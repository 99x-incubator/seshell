<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configure.aspx.cs" Inherits="SeShellTestStudio.Configure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>SeShell - Configuration</title>
	<meta name="description" content="Property configuration" />
	<meta name="author" content="Se Shell Team" />

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
                    <center><h1>Property Configuration</h1></center><br/>
                    <div runat="server">
                        <asp:PlaceHolder id="Area1" runat="server"></asp:PlaceHolder>
                        <asp:PlaceHolder id="HiddenArea" runat="server"></asp:PlaceHolder>
                        <asp:PlaceHolder id="Area2" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>           
	    </div>
        <div class="clear"><p><br /></p></div>
    </form>
    <script>
        $(function () {
            $("input:submit").button();
            $("input:checkbox").button();
        });
	</script>   
</body>

</html>
