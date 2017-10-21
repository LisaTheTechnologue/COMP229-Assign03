<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="COMP229_Assign03.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--6.	The Update Page will:
a.	load already containing the selected student's data.
b.	allow for changing any data fields.
c.	update changed fields in the database.-->

    <ul>
        <asp:Repeater ID="listSt" runat="server" OnItemCommand="listSt_ItemCommand" >
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
