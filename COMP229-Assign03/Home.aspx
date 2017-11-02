<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="COMP229_Assign03._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--3.	Your Landing Page will: 
a.	identify the brand.
b.	provide a list of all students’ names from the database.
c.	allow for the addition of new students to the database.
d.	allow a user to click on a student, loading the Student Page.
-->
    <div class="jumbotron" id="hero">
    </div>
    <asp:Label ID="employeesLabel" runat="server" />
    <ul>
        <asp:DataList ID="listSt" runat="server" OnItemCommand="listSt_ItemCommand">
            <ItemTemplate>
                <li>
                    <asp:LinkButton ID="stName" runat="server"
                        Text='<%#Eval("FirstMidName") + " " + Eval("LastName")%>'
                        CommandName="MoreDetail"
                        CommandArgument='<%#Eval("StudentID")%>' />
                </li>
            </ItemTemplate>
        </asp:DataList>
    </ul>
    
    <asp:Label for="insertCourseID"              runat="server" Text="Course ID: "        AutoPostBack="True"     /><asp:TextBox ID="insertCourseID"              runat="server" CssClass="form-group"></asp:TextBox><br>
    <asp:Label FOR="insertStudentID"             runat="server" Text="Student ID: "       AutoPostBack="True"     /><asp:TextBox ID="insertStudentID"             runat="server" CssClass="form-group"></asp:TextBox><br>
    <asp:Label for="insertStudentFirstMidName"   runat="server" Text="First Name: "       AutoPostBack="True"      /><asp:TextBox ID="insertStudentFirstMidName"   runat="server" CssClass="form-group"></asp:TextBox><br>
    <asp:Label for="insertStudentLastName"       runat="server" Text="Last Name: "        AutoPostBack="True"       /><asp:TextBox ID="insertStudentLastName"       runat="server" CssClass="form-group"></asp:TextBox><br>
    <asp:Label for="insertStudentEnrollmentDate" runat="server" Text="Enrollment Date: "  AutoPostBack="True"      />
    <asp:TextBox ID="insertStudentEnrollmentDate" runat="server" placeholder="please choose in the below calendar"  AutoPostBack="True" ReadOnly="true"></asp:TextBox>
    <asp:Calendar ID="CalendarDate" runat="server" CssClass="form-group" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" OnSelectionChanged="timePicker_changed" TitleFormat="Month" Width="400px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
        <DayStyle Width="14%" />
        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
        <TodayDayStyle BackColor="#CCCC99" />
    </asp:Calendar><br>
    <asp:Button ID="addStudent" runat="server" Text="Add" CssClass="btn btn-primary" CommandName="addStudent" OnClick="addStudent_Click"/> <br />
    <asp:Label ID="errorMsg" runat="server" />
</asp:Content>
