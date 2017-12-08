<%@ Page Title="Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course Enrollment.aspx.cs" Inherits="COMP229_Assign03.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="StudentInfo" runat="server" AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B" ShowFooter="true" OnRowDeleting="StudentInfo_RowDeleting"
        DataKeyNames="StudentID">
        <Columns>
            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <asp:Label ID="lblFirstName" runat="server"
                        Text='<%# Eval("FirstMidName")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFirstMidName" runat="server" Text='<%# Eval("FirstMidName")%>'></asp:TextBox>
                </EditItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:Label ID="lblLastName" runat="server"
                        Text='<%# Eval("LastName")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLastName" runat="server" Text='<%# Eval("LastName")%>'></asp:TextBox>
                </EditItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Enrollment Date">
                <ItemTemplate>
                    <asp:Label ID="lblEnrDate" runat="server"
                        Text='<%# Eval("EnrollmentDate")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEnrDate" runat="server"
                        Text='<%# Eval("EnrollmentDate")%>'></asp:TextBox>
                </EditItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="Grade">
                <ItemTemplate>
                    <asp:Label ID="lblGrade" runat="server"
                        Text='<%# Eval("Grade")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtGrade" runat="server"
                        Text='<%# Eval("Grade")%>'></asp:TextBox>
                </EditItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkRemove" runat="server"
                        OnClientClick="return confirm('Do you want to delete?')"
                        Text="Delete" CommandName="Delete"></asp:LinkButton>
                </ItemTemplate>

            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />
    </asp:GridView>
   
    <div class="addForm">
        <asp:Label ID="lblFullName" Text="Full name: " runat="server" AutoPostBack="true" />
        <asp:DropDownList ID="studentList" runat="server" OnSelectedIndexChanged="Name_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        <br />
        <asp:Label ID="lblEnrDate" Text="Enrollment Date: " runat="server" />
        <asp:TextBox ID="txtStudentEnrollmentDate" runat="server" placeholder="please choose in the below calendar" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
        <asp:Calendar ID="CalendarDate" runat="server" CssClass="form-group" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" OnSelectionChanged="timePicker_changed" TitleFormat="Month" Width="400px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
            <DayStyle Width="14%" />
            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
            <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
            <TodayDayStyle BackColor="#CCCC99" />
        </asp:Calendar>
        <br>
        <asp:Label ID="lblGrade" Text="Grade: " runat="server" />
        <asp:TextBox ID="txtGrade" runat="server" Text="0"></asp:TextBox><br />
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            CommandName="Add" OnClick="btnAdd_Click" />
        <br />
        <asp:Label ID="dbErrorMessage" ForeColor="Red" runat="server" />
    </div>
</asp:Content>
