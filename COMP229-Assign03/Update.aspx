<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="COMP229_Assign03.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--6.	The Update Page will:
a.	load already containing the selected student's data.
b.	allow for changing any data fields.
c.	update changed fields in the database.-->
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <asp:GridView ID="StudentInfo" runat="server" AutoGenerateColumns="false" Font-Names="Arial"
                Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                HeaderStyle-BackColor="Black" HeaderStyle-FontColor="White" AllowPaging="true" 
                ShowFooter="true" OnRowEditing="EditCustomer"
                OnRowUpdating="UpdateCustomer" OnRowCancelingEdit="CancelEdit"
                PageSize="10">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="StudentID">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentID" runat="server"
                                Text='<%# Eval("StudentID")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtStudentID" Width="40px"
                                MaxLength="5" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="100px" HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFirstMidName" runat="server"
                                Text='<%# Eval("FirstMidName")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFirstMidName" runat="server"
                                Text='<%# Eval("FirstMidName")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFirstMidName" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="150px" HeaderText="Company">
                        <ItemTemplate>
                            <asp:Label ID="lblLastName" runat="server"
                                Text='<%# Eval("LastName")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLastName" runat="server"
                                Text='<%# Eval("LastName")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="150px" HeaderText="Company">
                        <ItemTemplate>
                            <asp:Label ID="lblEnrDate" runat="server"
                                Text='<%# Eval("EnrDate")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEnrDate" runat="server"
                                Text='<%# Eval("EnrDate")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEnrDate" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                OnClick="AddNewCustomer" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
                <AlternatingRowStyle BackColor="#C2D69B" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="StudentInfo" />
        </Triggers>
    </asp:UpdatePanel>
  <%--  <table>
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

    </table>
    <asp:DataList runat="server" OnItemCommand="studentInfo_ItemCommand" ID="studentUpdate">
        <ItemTemplate>
            <asp:Button runat="server" Text="Edit" CommandName="editCommand" CommandArgument='<%# Eval("StudentID") %>' />
            | 
    <asp:Button runat="server" Text="Cancel Edit" CommandName="cancelCommand" CommandArgument='<%# Eval("StudentID") %>' />
        </ItemTemplate>
    </asp:DataList>--%>


</asp:Content>
