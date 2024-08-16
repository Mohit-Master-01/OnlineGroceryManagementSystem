<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineGroceryManagementSystem.View.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <%--<link href="../Assets/Lib/BootStrap/css/bootstrap.min.css" rel="stylesheet" />--%>

    <title>Login Form</title>

    <style>
        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            width: 100%;
            font-family: Arial, sans-serif;
            display: flex;
            align-items: center;
            justify-content: center;
            overflow: hidden; /* Prevent scrolling */
        }

        .container {
            position: relative;
            width: 100%;
            height: 100%;
        }

        .head {
            text-align: center;
            color: darkgreen;
        }

        .image-container {
            background: url('https://img.freepik.com/premium-photo/variety-fresh-vegetables-grocery-bag-black-background-healthy-food-shopping_210545-6907.jpg') no-repeat center center fixed;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }

        .logo-container {
            position: absolute;
            top: 9%;
            right: 17.5%; /* Position the logo to the right side */
            z-index: 10;
        }

        .logo {
            height: 50px; /* Reduced size */
            width: auto;
        }

        .form-container {
            position: absolute;
            top: 50%;
            right: 10%;
            transform: translateY(-50%);
            background: rgba(255, 255, 255, 0.9);
            padding: 30px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 400px; /* Increased width */
            height:auto;
            border-radius: 10px;
            opacity: 80%;
        }

        .login-form {
            display: flex;
            flex-direction: column;
        }

            .login-form h2 {
                margin-bottom: 20px;
                color: #333;
            }

            .login-form label {
                margin-bottom: 5px;
                color: #555;
            }

            .login-form input[id="txtUsername"],
            .login-form input[id="txtPassword"] {
                width: 90%;
                padding: 10px;
                margin-bottom: 15px;
                border: 1px solid #ccc;
                border-radius: 4px;
                text-align: left;
            }

            .login-form .radio-group {
                top: 5%;
                display: flow;
                margin-bottom: 50px; /* Increased space between radio buttons and login button */
            }

                .login-form .radio-group label {
                    margin-left: 5px;
                    margin-right: 20px;
                    color: #555;
                }

            .login-form input[id="btnSubmit"] {
                top: 40%;
                display: block; /* Center the button */
                width: 40%;
                margin: 0 auto; /* Center the button */
                padding: 10px;
                background-color: #28a745;
                border: none;
                border-radius: 4px;
                color: white;
                font-size: 16px;
                cursor: pointer;
            }

            .login-form button:hover {
                background-color: #218838;
                            
            }
    </style>

</head>
<body>
    <div class="container">
        <div class="image-container"></div>
        <div class="logo-container">
            <img src="../Assets/images/logo.png" alt="FreshCart Logo" class="logo">
        </div>

        <div class="form-container">
            <form class="login-form" runat="server">
                <h2 class="head" style="color: darkgreen">Welcome To Freshcart!</h2>

                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Enter username" Text="Enter username" ControlToValidate="txtUsername" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                <label>Enter Username: </label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox> 

                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtPassword" Text="Enter Password" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                <label>Enter Your Password: </label>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox> 


                <div class="radio-group">
                    <asp:RadioButtonList ID="rblUser" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Admin</asp:ListItem>
                        <asp:ListItem>Seller</asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <label id="lblMsg" runat="server" class="text-success"></label><br />
                <asp:Button ID="btnSubmit" runat="server" Text="Login" OnClick="btnSubmit_Click"  />

            </form>
        </div> 
    </div>

</body>
</html>
