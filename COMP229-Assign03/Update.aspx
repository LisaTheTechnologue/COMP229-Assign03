<%@ Page Title="Update" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="COMP229_Assign03.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--6.	The Update Page will:
a.	load already containing the selected student's data.
b.	allow for changing any data fields.
c.	update changed fields in the database.-->
    <asp:DetailsView ID="studentData" runat="server" AutoGenerateColumns="false"
        OnModeChanging="studentData_ModeChanging" OnItemUpdating="studentData_ItemUpdating"
        DataKeyNames="StudentID" OnItemCommand="DetailsViewExample_ItemCommand"
        OnPageIndexChanging="studentData_PageIndexChanging" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="8" ForeColor="Black" GridLines="Vertical" AutoGenerateRows="False" CellSpacing="2" Height="60px" HorizontalAlign="Center" Width="220px">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <Fields>
            <asp:TemplateField HeaderText="StudentID" Visible="True" InsertVisible="True">
                <ItemTemplate>
                    <asp:Label ID="lblStudentID" Text='<%# Eval("StudentID") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtStudentID" runat="server" Text='<%# Bind("StudentID") %>' MaxLength="10" />
                </EditItemTemplate>
            </asp:TemplateField>
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
                        Text='<%# Eval("EnrollmentDate")%>' ReadOnly="true"></asp:TextBox>
                    <asp:Calendar ID="CalendarDate" runat="server"
                        BackColor="White" BorderColor="Black" DayNameFormat="Shortest"
                        Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px"
                        NextPrevFormat="FullMonth" OnSelectionChanged="CalendarDate_SelectionChanged"
                        TitleFormat="Month" Width="400px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                        <DayStyle Width="14%" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#CCCC99" />
                    </asp:Calendar>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grade">
                <ItemTemplate>
                    <asp:Label ID="lblGrade" runat="server"
                        Text='<%# Eval("Grade")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtGrade" runat="server" Text='<%# Eval("Grade")%>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField Visible="true" ShowCancelButton="true" ShowEditButton="true" />
        </Fields>

        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

    </asp:DetailsView>
    <br />
    <asp:Label ID="errorMsg" runat="server"></asp:Label>

</asp:Content>
