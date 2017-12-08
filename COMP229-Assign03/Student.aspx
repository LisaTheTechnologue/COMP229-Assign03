<%@ Page Title="Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="COMP229_Assign03.Student" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--4.	Your Student Page will:
-a.	collect and display personal data about the selected student as covered by the SQL database’s Student table. 
-b.	list the selected student’s courses.
-i.	Clicking on a course will load that course's Course Page.
-c.	include an Update link to the Update Page. 
-d.	include parameterized SQL queries for all actions. 
-e.	include a delete button to remove the selected student (and redirect to the home page). -->

    <table class="table table-striped">
        <thead>
            <tr>
                <th>Student ID     </th>
                <th>Full Name      </th>
                <th>Enrollment Date</th>
                <th>Course(s)      </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="stID" runat="server" Text='<%#Eval("StudentID")%>' /></td>
                <td>
                    <asp:Label ID="stName" runat="server" Text='<%#Eval("FirstMidName") + " " + Eval("LastName") %>' /></td>
                <td>
                    <asp:Label ID="stDate" runat="server" Text='<%#Eval("EnrollmentDate")%>' /></td>
                <td>

                    <asp:DataList runat="server" ID="listCr" OnItemCommand="listCr_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="crName" runat="server"
                                Text='<%#Eval("Title") +" - Grade: " + Eval("Grade")%>'
                                CommandName="MoreDetail"
                                CommandArgument='<%#Eval("CourseID")%>' OnClick="Change" />
                        </ItemTemplate>
                    </asp:DataList>

                </td>
            </tr>
        </tbody>
    </table>
    <div class="confirm">
        <asp:LinkButton ID="updateInfo" OnClick="Change" CommandName="Update" CommandArgument="Update" Text="Update Info" runat="server" />
        <br />
        <asp:LinkButton ID="deleteSt" OnClick="Change" CommandName="Delete" Text="Delete Student" runat="server" CommandArgument='<%#Eval("StudentID")%>' />
        <br />
        <asp:Label ID="errorMsg" class="errorMsg" runat="server" />
    </div>
</asp:Content>
