﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stadHome.aspx.cs" Inherits="DBProject.stadHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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

        .register {
            font-weight: bold;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 50%;
            background-color: #5cb85c;
        }

        .btn {
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
    </style></head>
<body>
    <form id="form1" runat="server" class="form">
        <div class="container2">
            <center>
                <h2>Stadium Manager Home</h2>
                <br />
                <asp:Button ID="info" runat="server" Text="View Stadium Info" OnClick="info_Click" class="btn"/>
                <br />
                <asp:Button ID="requests" runat="server" Text="View Received Requests" OnClick="requests_Click" class="btn"/>
                <br />
                <asp:Button ID="accept" runat="server" Text="Accept a Request" OnClick="accept_Click" class="btn"/>
                <br />
                <asp:Button ID="reject" runat="server" Text="Reject a Request" OnClick="reject_Click" class="btn"/>
                <br />
                <asp:Button ID="logout" runat="server" Text="Log Out" OnClick="logout_Click" class="back"/>
             </center>
        </div>
    </form>
</body>
</html>
