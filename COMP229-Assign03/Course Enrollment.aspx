<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course Enrollment.aspx.cs" Inherits="COMP229_Assign03.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <!--
5.	The Course Page will: 
a.	display all students enrolled in the selected course. 
b.	allow for the removal and addition of a student to the selected course.

-->
    <asp:Label ID="courseName" runat="server" Text='<%#Eval("Title") %>' />
    <asp:Label ID="courseSt" runat="server" Text='<%#Eval("Title") %>' />
</asp:Content>
