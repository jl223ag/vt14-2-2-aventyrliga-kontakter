<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" ViewStateMode="Disabled" Inherits="AventyrligaKontakter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>

    <form id="form1" runat="server">
    <h1>Äventyrliga kontakter</h1>
    <div id="wrapper">
        <%-- Rättmeddelande --%>
        <div ID="SaveMessage" runat="server" Visible="false">
            <p id="closeSaveMessage" onClick="remover()">X</p> <%-- javascript funktion --%>
            <p id="saveText">Kontakten har sparats!</p>
        </div>

        <%-- Felmeddelanden--%>
        <asp:PlaceHolder ID="ErrorMessages" runat="server">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="InsertGroup" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="EditGroup" />
        </asp:PlaceHolder>

        <%-- List View--%>
        <asp:ListView ID="AdventurousListView" runat="server"
            SelectMethod="AdventurousListView_GetData"
            ItemType="AventyrligaKontakter.Model.Contact"
            DataKeyNames="ContactID"
            InsertMethod="AdventurousListView_InsertItem"
            UpdateMethod="AdventurousListView_UpdateItem"
            DeleteMethod="AdventurousListView_DeleteItem"
            InsertItemPosition="FirstItem">

            <%-- Layout--%>

            <LayoutTemplate>
                <table>
                    <tr>
                        <th>Firstname</th>
                        <th>Lastname</th>
                        <th>Email</th>
                        <th></th>
                    </tr>
                    <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                </table>

                <%-- Datapager--%>

                <asp:DataPager ID="DataPager1" runat="server" PageSize="20" PagedControlID="AdventurousListView">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" << "
                            ShowNextPageButton="False" ShowPreviousPageButton="False" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" >> "
                            ShowNextPageButton="False" ShowPreviousPageButton="False" />
                    </Fields>
                </asp:DataPager>

            </LayoutTemplate>

            <%-- Item template--%>

            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="FnamnLabel" runat="server" Text="<%#: Item.FirstName %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="EnamnLabel" runat="server" Text="<%#: Item.LastName %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="EmailLabel" runat="server" Text="<%#: Item.EmailAddress %>"></asp:Label>
                    </td>
                    <td class="command">
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" OnClientClick='<%# String.Format("return confirm(\"Ta bort {0} {1}?\")", Item.FirstName, Item.LastName) %>' CausesValidation="false"></asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false"></asp:LinkButton>

                    </td>
                </tr>
            </ItemTemplate>

            <%-- Insert item--%>

            <InsertItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="Fnamn" runat="server" Text="<%#: BindItem.FirstName %>" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Du måste fylla i ett namn" ControlToValidate="Fnamn" ValidationGroup="InsertGroup" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="Enamn" runat="server" Text="<%#: BindItem.LastName %>" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Du måste fylla i ett efternamn" ControlToValidate="Enamn" ValidationGroup="InsertGroup" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="Email" runat="server" Text="<%#: BindItem.EmailAddress %>" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Du måste ange en emailadress" ControlToValidate="Email" ValidationGroup="InsertGroup" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Emailen har ett felaktigt format" ControlToValidate="Email" ValidationExpression="^(?!\.)(\w|-|\.)+(?!\.)@(?!\.)[-.a-zåäöA-ZÅÄÖ0-9]+[a-zåäöA-ZÅÄÖ]{2,4}$" ValidationGroup="InsertGroup" Display="None"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" ValidationGroup="InsertGroup"></asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="False" ValidationGroup="InsertGroup"></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>

            <%-- Edit item--%>

            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="Fnamner" runat="server" Text="<%#: BindItem.FirstName %>" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Du måste fylla i ett namn" ControlToValidate="Fnamner" ValidationGroup="EditGroup" Display="None"></asp:RequiredFieldValidator>

                    </td>
                    <td>
                        <asp:TextBox ID="Enamner" runat="server" Text="<%#: BindItem.LastName %>" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Du måste fylla i ett efternamn" ControlToValidate="Enamner" ValidationGroup="EditGroup" Display="None"></asp:RequiredFieldValidator>

                    </td>
                    <td>
                        <asp:TextBox ID="Emailer" runat="server" Text="<%#: BindItem.EmailAddress %>" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Du måste fylla i en emailadress" ControlToValidate="Emailer" ValidationGroup="EditGroup" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Emailen har ett felaktigt format" ControlToValidate="Emailer" ValidationExpression="^(?!\.)(\w|-|\.)+(?!\.)@(?!\.)[-.a-zåäöA-ZÅÄÖ]+[a-zåäöA-ZÅÄÖ]{2,4}$" ValidationGroup="EditGroup" Display="None"></asp:RegularExpressionValidator>


                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Update" Text="Spara" ValidationGroup="EditGroup"></asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="False" ValidationGroup="EditGroup"></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>

            <%-- Empty data--%>

            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td>
                            Kunduppgifter finns ej
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>

        </asp:ListView>

    </div>
    </form>
    <script>
        function remover() {
            var wrapper = document.getElementById("wrapper");
            var saveMessage = document.getElementById("SaveMessage");
            wrapper.removeChild(saveMessage);
        };
    </script>
</body>
</html>
