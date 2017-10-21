<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="COMP229_Assign03._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--3.	Your Landing Page will: 
a.	identify the brand.
b.	provide a list of all students’ names from the database.
c.	allow for the addition of new students to the database.
d.	allow a user to click on a student, loading the Student Page.
-->
    <div class="jumbotron" id="hero">
    </div>
    <ol>
        <asp:Repeater ID="listSt" runat="server" OnItemCommand="listSt_ItemCommand">
            <ItemTemplate>
                <li>
                    <asp:LinkButton ID="stName" runat="server"
                        Text='<%#Eval("FirstName") + " " + Eval("LastName")%>'
                        CommandName="MoreDetail"
                        CommandArgument='<%#Eval("StudentID")%>' />

                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ol>

</asp:Content>
