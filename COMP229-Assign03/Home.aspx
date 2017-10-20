<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="COMP229_Assign03._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--3.	Your Landing Page will: 
a.	identify the brand.
b.	provide a list of all students’ names from the database.
c.	allow for the addition of new students to the database.
d.	allow a user to click on a student, loading the Student Page.
-->
    <div class="jumbotron" id="hero">
        <h1>La Vie</h1>
        <h4>School of Engineering</h4>
    </div>
    <ul>
        <asp:Repeater ID="myRepeater" runat="server" OnItemCommand="myRepeater_ItemCommand" >
            <ItemTemplate>
                <li>
                    <span id="class"><%# Eval("Title") %></span>
                    <asp:Button runat="server" Text="Update" CommandName="updateCommand" CommandArgument='<%# Eval("Title") %>' /> | 
                    <asp:Button runat="server" Text="Delete" CommandName="deleteCommand" CommandArgument='<%# Eval("CourseID") %>' />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>    

</asp:Content>
