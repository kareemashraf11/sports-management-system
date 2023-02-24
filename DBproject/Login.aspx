﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DBProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        form {
            border: 3px solid #f1f1f1;
        }

        input[type=text], input[type=password] {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }

        .login {
            font-weight: bold;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 50%;
            background-color: #5cb85c;
        }

        .back {
            font-weight: bold;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 50%;
            background-color: #d9534f;
        }

        button:hover {
            opacity: 0.8;
        }

        .container {
            padding: 16px;
        }

        .container2 {
            padding: 16px;
            text-align: center
        }

        

        .form {
            margin: auto;
            width: 40%;
        }

        .err {
            color: firebrick;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form">
        <div class="container">
            <center>
                <h2>Please Log In</h2>
            </center>

            <br />
            <h3>Username</h3>
            <asp:TextBox ID="username" runat="server" placeholder="Enter Username"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text=" " class="err"></asp:Label>
            <br />
            <br />
            <h3>Password</h3>
            <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text=" " class="err"></asp:Label>
            <br />
            <br />
        </div>
        <div class="container2">
            <asp:Button ID="loginB" runat="server" OnClick="login" Text="Login" class="login" />
            <br />
            <asp:Button ID="back" runat="server" Text="Go Back" class="back" OnClick="back_Click" />
            <br />
            <asp:Label ID="Label3" runat="server" Text=" " class="err"></asp:Label>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
