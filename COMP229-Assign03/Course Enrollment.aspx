<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course Enrollment.aspx.cs" Inherits="COMP229_Assign03.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="StudentInfo" runat="server" AutoGenerateColumns="false"
                AlternatingRowStyle-BackColor="#C2D69B" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="Full Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFullName" runat="server"
                                Text='<%# Eval("FirstMidName") + " " + Eval("LastName")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFirstMidName" runat="server"
                                Text='<%# Eval("FirstMidName")+ " " + Eval("LastName")%>'></asp:TextBox>
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
                                CommandArgument='<%# Eval("StudentID")%>'
                                OnClientClick="return confirm('Do you want to delete?')"
                                Text="Delete" CommandName="Delete"></asp:LinkButton>
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="#C2D69B" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="StudentInfo" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:DropDownList ID="studentList" runat="server"></asp:DropDownList>
    <asp:TextBox ID="txtEnrDate" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtGrade" runat="server" Text="0"></asp:TextBox>
    <asp:Button ID="btnAdd" runat="server" Text="Add"
        CommandName="Add" />
    <br />
    <asp:Label ID="dbErrorMessage" ForeColor="Red" runat="server" />

</asp:Content>
