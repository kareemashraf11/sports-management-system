﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="purchaseTicket.aspx.cs" Inherits="DBProject.purchaseTicket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Ticket</title>
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
                <h2>Purchase a Ticket</h2>
                <br />
                <h3>Host Club</h3>
                <br />
                <asp:TextBox ID="host" runat="server" placeholder="Enter Host Club Name"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text=" " class="err"></asp:Label>
                <br /> <br />
                <h3>Guest Club</h3>
                <br />
                <asp:TextBox ID="guest" runat="server" placeholder="Enter Guest Club Name"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text=" " class="err"></asp:Label>
                <br /> <br />
                <h3>Start Time</h3>
                <br />
                <asp:TextBox ID="start" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text=" " class="err"></asp:Label>
                <br /> <br /> <br />
                <asp:Button ID="purchase" runat="server" Text="Purchase" class="btn" OnClick="purchase_Click" />
                <br />
                <asp:Button ID="back" runat="server" Text="Go Back" class="back" OnClick="back_Click" />
                <br />
                <asp:Label ID="Label4" runat="server" Text=" " class="err"></asp:Label>

            </center>
        </div>
    </form>
</body>
</html>
