<%@ Page Title="Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course Enrollment.aspx.cs" Inherits="COMP229_Assign03.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--List of Students-->
    <fieldset>
        <legend>List of Student</legend>
        <asp:GridView ID="StudentInfo" runat="server" AutoGenerateColumns="False"
            AlternatingRowStyle-BackColor="#C2D69B" ShowFooter="True" OnRowDeleting="StudentInfo_RowDeleting"
            DataKeyNames="StudentID" BackColor="White" BorderColor="#999999" BorderStyle="Solid" 
            BorderWidth="1px" CellPadding="5" ForeColor="Black" GridLines="Vertical" Height="60px" HorizontalAlign="Center" Width="600px">
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
            <AlternatingRowStyle BackColor="#CCCCCC" HorizontalAlign="Center" />
            <FooterStyle BackColor="#ffffff" HorizontalAlign="Center"/>
            <HeaderStyle BackColor="#d13636" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <RowStyle  HorizontalAlign="Center" />

            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center"/>
            <SortedAscendingHeaderStyle BackColor="#808080" HorizontalAlign="Center"/>
            <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center"/>
            <SortedDescendingHeaderStyle BackColor="#383838" HorizontalAlign="Center"/>
        </asp:GridView>
    </fieldset>

    <!--Adding new students-->
    <fieldset>
        <legend>Add New Student</legend>
        <div class="row">
            <div class="col-md-6 labelAdd">
                <asp:Label ID="lblFullName" Text="Full name: " runat="server" />
            </div>
            <div class="col-md-6 ">
                <asp:DropDownList ID="studentList" runat="server"
                    OnSelectedIndexChanged="Name_SelectedIndexChanged" CssClass="textboxAdd">
                </asp:DropDownList>
            </div>
            <div class="col-md-6 labelAdd">
                <asp:Label ID="lblEnrDate" Text="Enrollment Date: " runat="server" />
            </div>
            <div class="col-md-6 ">
                <asp:TextBox ID="txtStudentEnrollmentDate" runat="server" CssClass="textboxAdd" placeholder="Please choose in the below calendar" ReadOnly="true"></asp:TextBox>
                <asp:Calendar ID="CalendarDate" runat="server" CssClass="calendar" BackColor="White"
                    BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt"
                    ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" OnSelectionChanged="timePicker_changed" TitleFormat="Month" Width="400px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                    <DayStyle Width="14%" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                    <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                    <TodayDayStyle BackColor="#CCCC99" />
                </asp:Calendar>
            </div>
            <div class="col-md-6 labelAdd">
                <asp:Label ID="lblGrade" Text="Grade: " runat="server" />
            </div>
            <div class="col-md-6 ">
                <asp:TextBox ID="txtGrade" runat="server" Text="0" CssClass="textboxAdd"></asp:TextBox>
            </div>
        </div>
    </fieldset>
    <div class="confirm">
        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary"
            CommandName="Add" OnClick="btnAdd_Click" />
        <br />
        <asp:Label ID="dbErrorMessage" class="errorMsg" runat="server" />
    </div>
</asp:Content>
