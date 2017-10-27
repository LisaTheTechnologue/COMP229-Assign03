<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="COMP229_Assign03.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--6.	The Update Page will:
a.	load already containing the selected student's data.
b.	allow for changing any data fields.
c.	update changed fields in the database.-->
    <asp:DetailsView ID="studentData" runat="server" AutoGenerateColumns="false" OnItemUpdating="studentData_ItemUpdating">
        <Fields>
            <asp:TemplateField HeaderText="Full Name">
                <ItemTemplate>
                    <asp:Label ID="lblFirstMidName" runat="server"
                        Text='<%# Eval("FirstMidName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFirstMidName" runat="server"
                        Text='<%# Eval("FirstMidName")%>'></asp:TextBox>
                </EditItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="Full Name">
                <ItemTemplate>
                    <asp:Label ID="lblFullName" runat="server"
                        Text='<%# Eval("LastName")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFirstMidName" runat="server"
                        Text='<%# Eval("LastName")%>'></asp:TextBox>
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

            <asp:CommandField ShowEditButton="True" />

        </Fields>
    </asp:DetailsView>
    <br />

</asp:Content>
