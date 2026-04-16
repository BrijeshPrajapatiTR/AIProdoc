<%@ Register TagPrefix="uc" TagName="login" Src="Controls/FilerLogin.ascx" %>
<%@ Register TagPrefix="uc" TagName="footer" Src="Controls/PageFooter.ascx" %>
<%@ Register TagPrefix="uc" TagName="header" Src="Controls/PageHeader.ascx" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="ProDoc.Web.Default" %>
<!DOCTYPE>
<HTML>
	<HEAD>
		<title>ProDoc® eFiling 2 - Welcome!</title>
	    <meta http-equiv="x-ua-compatible" content="IE=9"/>
	    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0"/>
	    <meta name="CODE_LANGUAGE" content="C#"/>
	    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/xhtml-11"/>
	    <meta name="vs_defaultClientScript" content="JavaScript"/>
	    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
	    <link href="css/smoothness/jquery-ui.css" rel="stylesheet" />
        <link href="css/site.css" rel="stylesheet" />
        <link rel="stylesheet" href="style.css" type="text/css"/>
	    <style type="text/css">

         .adminlRow
        {
	        font-family: Verdana;
	        font-size: 10px;
	        letter-spacing: 0px;
	        line-height: 14px;
        }
         .ui-accordion .ui-accordion-content {
    	        margin: 0;
    	        list-style: none;
    	        line-height: 14px;
	            padding: 1em;
	            border-top: 0;
	            overflow: auto;
                font-size: 12px;
            }
            .ui-accordion .ui-accordion-header {
                background-image:none ;
	            display: block;
	            cursor: pointer;
	            position: relative;
	            margin-top: 2px;
	            padding: 0 0 0 0;
	            min-height: 0;  /*support: IE7*/ 
                font-weight:bold;
                background-color: #767676 ;
            	color:white ;
	            font-family: Verdana ;
                height:20px;
                line-height:normal !important;
            }
	         /*Corner radius*/ 
	        .ui-corner-all,
	        .ui-corner-top,
	        .ui-corner-left,
	        .ui-corner-tl {
	            border-top-left-radius: 0px;
	        }

	        .ui-corner-all,
	        .ui-corner-top,
	        .ui-corner-right,
	        .ui-corner-tr {
	            border-top-right-radius: 0px;
	        }

	        .ui-corner-all,
	        .ui-corner-bottom,
	        .ui-corner-left,
	        .ui-corner-bl {
	            border-bottom-left-radius: 0px;
	        }

	        .ui-corner-all,
	        .ui-corner-bottom,
	        .ui-corner-right,
	        .ui-corner-br {
	            border-bottom-right-radius: 0px;
	        }

            .ui-accordion-header a {
                color: white !important;
                line-height: normal !important;
                display: block ;
                font-size: 14px ;
                width: 100% ;
                text-indent: 10px ;
            }
            .ui-accordion .ui-accordion-header .ui-accordion-header-icon {
                float:right;
	            position:relative;
                left:0;
               margin-top:0;
               top:0;
            }
        </style>

        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-61695488-1', 'auto');
            ga('send', 'pageview');

        </script>
        
        <script type="text/javascript" src="js/jquery-3.4.1.js"></script>
        <script type="text/javascript" src="js/jquery-ui-1.12.1.js"></script>

	    <script type="text/javascript">
    	    $(document).ready(function() {
    	        $("#lockedUserDialogAdmin").dialog({
    	            title: "Your account is locked",
    	            width: 550,
    	            modal: true,
    	            autoOpen: false,
    	            buttons: {
    	                Dismiss: function () {
    	                    $(this).dialog("close");
    	                }
    	            }
    	        });
    	        $("#lockedUserDialogFiler").dialog({
    	            title: "Your account is locked",
    	            width: 600,
    	            modal: true,
    	            autoOpen: false,
    	            buttons: {
    	                Dismiss: function() {
    	                    $(this).dialog("close");
    	                }
    	            }
    	        })
    	        ;
    	        if ($("#lblError").text().indexOf("79(User is locked out.)") != -1) {
    	            var eMail = $("#txtUserId").val();
    	            $.ajax({
    	                type: "POST",
    	                async: true,
    	                url: "Default.aspx/GetUserRole",
    	                contentType: "application/json; charset=utf-8",
    	                data: JSON.stringify({ eMail: eMail }),
    	                dataType: "json",
    	                success: function(response) {
    	                    var userRole = response.d;
    	                    $("#prodocEmailIns").append("<a href=\"PasswordReset.aspx\">Click here to reset your password</a> using your password reset question answer.");
    	                    if (userRole == "" || userRole.indexOf("Admin") != -1) {
     	                        $("#lockedUserDialogAdmin").dialog("open");
    	                        $(".ui-widget-overlay").css("opacity", "0.8");
    	                    }
    	                    else {
    	                        $.ajax({
    	                            type: "POST",
    	                            async: true,
    	                            url: "Default.aspx/GetUserFirmAdmins",
    	                            contentType: "application/json; charset=utf-8",
    	                            data: JSON.stringify({ eMail: eMail }),
    	                            dataType: "json",
    	                            success: function(response) {
    	                                var adminList = (typeof response.d) == 'string' ? JSON.parse(response.d) : response.d;
    	                                if (adminList.length > 0) {
    	                                    $("#admins").append("<tr><th> Name </th><th>Email Address</th></tr>");
    	                                    for (var i = 0; i < adminList.length; i++) {
    	                                        $("#admins").append("<tr><td class='adminlRow'>" + adminList[i]["FirstName"] + " " + adminList[i]["LastName"] + "</td><td class='adminlRow'> <a href=\"mailto:" + adminList[i]["Email"] + "?subject=My eFiling account is locked out\">" + adminList[i]["Email"] + "</a></td></tr>");
    	                                    }
    	                                    $("#lockedUserDialogFiler").dialog("open");
    	                                    $(".ui-widget-overlay").css("opacity", "0.8");
    	                                }
    	                                else {
    	                                    $("#lockedUserDialogAdmin").dialog("open");
    	                                    $(".ui-widget-overlay").css("opacity", "0.8");
    	                                }
    	                            }

    	                        });

    	                    }},
    	                error: function(xhr, ajaxOptions, thrownError) {
    	                    $("#lockedUserDialogAdmin").dialog("open");
    	                    $(".ui-widget-overlay").css("opacity", "0.8");
    	                }
    	            });


                   
    	        }

    	            $("#header_lnkLogout").css("display", "none");

    	        }
             );


    	</script>        
	</HEAD>
	<body>

		<form method="post" runat="server" id="Default">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td align="center">
						<uc:header id="header" runat="server" section="HOME" HelpPage="EfilingHome"></uc:header>
						<table class="content">
							<tr>
								<td align="left" valign="top">
									<table cellpadding="0" cellspacing="12" border="0" width="100%">
										<tr>
											<td class="sidemenu">
												<uc:login id="login" runat="server"></uc:login>
												<br>
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
												    <tr>
												    <th> General Information</th>
											        </tr>
												    <tr>
												        <td class="section" style="padding:4px">
												            <p>
                                                                <a href="Information/AboutProDoceFiling.aspx" class="sectionlink">eFiling 2 General Information</a>
                                                                <br />												            
                                                                <a href="Information/Registration Guide-eFiling in Texas State Courts.pdf" target="_blank" class="sectionlink">Registration Guide</a>
                                                                <br />												            
                                                                <a href="Information/eFiling Guide.pdf" target="_blank" class="sectionlink">eFiling Guide</a>
                                                                <br />												            
                                                            </p>
                                                        </td>
											        </tr>
											    </table>

												<table cellpadding="2" cellspacing="0" border="0" width="100%">
													<tr>
														<th>Quick Links</th>
                                                        </tr>
                                                    <tr>
														<td class="section">
															<a href="Information/cle_training.aspx" class="sectionlink">MCLE eFiling Training</a>
                                                            <br/>
															<a href="Information/Pricing.aspx" class="sectionlink">Fees</a>
                                                            <br/>
															<%--<a href="http://www.supreme.courts.state.tx.us/miscdocket/13/13916500.pdf" class="sectionlink" target="_blank">eFiling Rules</a>--%>
                                                            <a href="http://www.txcourts.gov/media/124904/statewide-efiling-rules.pdf" class="sectionlink" target="_blank">eFiling Rules</a>
                                                            <br/>
															<%--<a href="http://www.courts.state.tx.us/pubs/pubs-home.asp" target="_blank" class="sectionlink">Case Information Sheets</a>--%>
                                                            <a href="http://www.txcourts.gov/rules-forms/forms.aspx" target="_blank" class="sectionlink">Case Information Sheets</a>
														</td>
													</tr>
												</table>
												<br>
												<table cellpadding="2" cellspacing="0" border="0" width="100%">
													<tr>
														<td class="section">
															
															<img src="Images/e-filing3.jpg" align="left" height="164" width="196">
														</td>
													</tr>
												</table>
											</td>
											<td valign="top">
												<table cellpadding="1" cellspacing="0" border="0" width="100%">
													<tr>
														<td valign="top" class="auto-style1">
															<img src="Images/e-filing1.jpg">
														</td>
														<td valign="top"  align="center" class="auto-style1">
															<table  cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: 2px">
																<tr>
																	<td>
																		<center><asp:label id="lblAlertHeader" runat="server" cssclass="bolderrorLg" visible="false"></asp:label> </center>

																	</td>
																</tr>
																<tr>
																	<td>
																		<p><asp:label id="lblAlert" runat="server" cssclass="bolderror" visible="false" Height=20px  ></asp:label></p>
                                                                        <br/>
																	</td>
																</tr>
																<tr style="background-color:#F6F6F6; height:100%">
																	<td>
																		<center><p class="normal" style="padding-top:2px;"><big>Welcome to <strong style="color: red ">ProDoc</strong><strong> eFiling</strong><strong style="color: red "> 2!</strong></big></p></center>
																		<p class="normal" align="left">
																			ProDoc eFiling allows you to register and electronically file in 
                                                                            Texas State Courts.&nbsp; Texas Courts that hear Civil, Family law, Probate and some Criminal Law Courts accept or mandate 
                                                                            eFiling.
																		</p>
                                                                        <p class="normal" align="left">
																			ProDoc eFiling is a Certified eFiling Service provider in Texas. 
                                                                            &nbsp;Learn more about eFiling in Texas and why ProDoc is a leader in 
                                                                            eFiling by exploring the links on this page.
																		</p>

<%--                                                                        <p class="normal"><a href="Information/Support issues SFS.pdf" target="_blank"><b>Current Support Issues</b></a></p>
                                                                        <p class="normal" align="left">
																		For all eFile videos, click <a target="_blank" href="videos.html"><strong>here</strong></a>
									                                    <br /><br />
																		</p>--%>
                                                                        <%--<p class="normal" align="left">
																			What happened to ProDoc eFile 1?  <a target="_blank" href="Information/WhatsHappeningToProDocEFiling1.pdf"><strong>Find out here</strong></a>
																		</p>--%>
																	</td>
																</tr>
															</table>
														</td>
														<td valign="top" class="auto-style1"><img src="Images/e-filing2.jpg"></td>
													</tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
													<%--<tr>
														<th>
															News &amp; Information
														</th>
													</tr>
                                                    <tr class="adminlRow" >
                                                        <td class="section" valign="top">
                                                            Jurisdiction Specific eFiling Information has been added to the Resources menu to help
                                                            speed up your eFiling and avoid having your eFiling returned for correction.
                                                        </td>
                                                    </tr>--%>
													<tr>
														<td class="section" valign="top">																												
														</td>
													</tr>
													<tr>
														<td class="sectionbottom" align="left">
															&nbsp;
														</td>
													</tr>
												</table>
                                                        </td>
                                                    </tr>
													<tr>
														<td colspan="3">
															<span id="showDiv"></span>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<uc:footer id="footer" runat="server"></uc:footer>
					</td>
				</tr>
			</table>
		</form>
        <div id="lockedUserDialogAdmin">
            <p>For security purposes, your account has been locked.</p>
            <div id="prodocEmailIns"></div>
            <p>The reset email will appear in your mailbox from the State Court's email address, no-reply@txcourts.gov</p>
        </div>
        <div id="lockedUserDialogFiler">
            <p>For security purposes, your account has been locked.</p>
            <p>Please contact one of your firm administrator(s), listed below to unlock your account, or <a href="PasswordReset.aspx">Click here to reset your password</a> using your password reset question answer.</p>
            <br />
            <table id="admins" cellpadding="2px" cellspacing="2px" border="0" width="100%">
                
            </table>

        </div>
	</body>
</HTML>
