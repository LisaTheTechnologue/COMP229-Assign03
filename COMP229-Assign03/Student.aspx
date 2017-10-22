<%@ Page Title="Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="COMP229_Assign03.Student" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--4.	Your Student Page will:
a.	collect and display personal data about the selected student as covered by the SQL database’s Student table. 
b.	list the selected student’s courses.
i.	Clicking on a course will load that course's Course Page.
c.	include an Update link to the Update Page. 
d.	include parameterized SQL queries for all actions. 
e.	include a delete button to remove the selected student (and redirect to the home page). -->
    <table>
        <tr>
            <td>Full Name:</td>
            <td>
                <asp:Label ID="stName" runat="server" /></td>
        </tr>
        <tr>
            <td>Student ID:</td>
            <td>
                <asp:Label ID="stID" runat="server" /></td>
        </tr>
        <tr>
            <td>Enrollment Date:</td>
            <td>
                <asp:Label ID="stDate" runat="server" /></td>
        </tr>
        <tr>
            <td>Grade:</td>
            <td>
                <asp:Label ID="stGrade" runat="server" /></td>
        </tr>
        <tr>
            <td>Course(s):</td>
            <td>
                <asp:Label ID="stCourse" runat="server" /></td>
        </tr>
    </table>
    <asp:LinkButton ID="updateInfo" OnClick="Change" CommandName="Update" Text="Update Info" runat="server"/> <br />
    <asp:LinkButton ID="deleteSt" OnClick="Change" CommandName="Delete" Text="Delete this Student" runat="server"/>
</asp:Content>
