<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course Enrollment.aspx.cs" Inherits="COMP229_Assign03.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!--
5.	The Course Page will: 
a.	display all students enrolled in the selected course. 
b.	allow for the removal and addition of a student to the selected course.

-->
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="StudentInfo" runat="server" AutoGenerateColumns="false" Font-Names="Arial"
                Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                HeaderStyle-BackColor="Black" HeaderStyle-FontColor="White" AllowPaging="true" 
                ShowFooter="true"
                OnPageIndexChanging="OnPaging" OnRowEditing="EditCustomer"
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
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument='<%# Eval("StudentID")%>'
                                OnClientClick="return confirm('Do you want to delete?')"
                                Text="Delete" OnClick="DeleteCustomer"></asp:LinkButton>
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
            <asp:AsyncPostBackTrigger ControlID="GridView1" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Label ID="courseName" runat="server" Text='<%#Eval("Title") %>' />
    <ul>
        <asp:Repeater ID="listSt" runat="server" OnItemCommand="listSt_ItemCommand">
            <ItemTemplate>
                <li>
                    <asp:LinkButton ID="crName" runat="server"
                        Text='<%#Eval("FirstMidName") +" " + Eval("LastName")%>'
                        CommandName="MoreDetail"
                        CommandArgument='<%#Eval("StudentID")%>' />
                    | 
                    <asp:LinkButton ID="deleteSt" OnClick="Change" CommandName="Delete"
                        Text="Delete Student" runat="server" CommandArgument='<%#Eval("StudentID")%>' />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>

    <asp:DropDownList ID="studentList" runat="server" ></asp:DropDownList><br />
    <asp:Label ID="dbErrorMessage" ForeColor="Red" runat="server" />

    <asp:LinkButton ID="updateInfo" OnClick="Change" CommandName="Add" CommandArgument="Add" Text="Add Student" runat="server" />
    
</asp:Content>
