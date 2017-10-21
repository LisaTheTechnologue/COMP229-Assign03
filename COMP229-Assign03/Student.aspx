<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="COMP229_Assign03.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--4.	Your Student Page will:
a.	collect and display personal data about the selected student as covered by the SQL database’s Student table. 
b.	list the selected student’s courses.
i.	Clicking on a course will load that course's Course Page.
c.	include an Update link to the Update Page. 
d.	include parameterized SQL queries for all actions. 
e.	include a delete button to remove the selected student (and redirect to the home page). -->

    Student ID: <%#Eval("StudentID")%> <br />
    Name: <%#Eval("LastName") %> <%#Eval("FirstMidName") %> <br />
    Enrollment Date: <%#Eval("EnrollmentDate") %>
    Courses: <%#Eval("Title") %>
    <asp:LinkButton Text="Update" CommandName="updateCommand" CommandArgument='<%# Eval("Title")%>' runat="server"/>
    <asp:LinkButton Text="Delete" CommandName="deleteCommand" CommandArgument='<%# Eval("Title")%>' runat="server"/>
</asp:Content>
