<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="COMP229_Assign03.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--6.	The Update Page will:
a.	load already containing the selected student's data.
b.	allow for changing any data fields.
c.	update changed fields in the database.-->
    <asp:DetailsView ID="studentData" runat="server" AutoGenerateColumns="false" 
        OnModeChanging="studentData_ModeChanging" OnItemUpdating="studentData_ItemUpdating"
        DataKeyNames="StudentID" >
        <Fields>
            <asp:BoundField HeaderText="First Name" DataField="FirstMidName" />
            
            <asp:BoundField HeaderText="Last Name" DataField="LastName" />
           
            <asp:BoundField HeaderText="Enrollment Date" DataField="EnrollmentDate" />
           
            <asp:CommandField ShowEditButton="True" />
        </Fields>
        <HeaderTemplate>
            <%#Eval("StudentID")%>
        </HeaderTemplate>
       
    </asp:DetailsView>
    <br />
    <asp:Label ID="errorMsg" runat="server"></asp:Label>

</asp:Content>
