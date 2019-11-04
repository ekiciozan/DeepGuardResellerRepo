<%@ Page Language="C#"  MasterPageFile="~/Reseller/Site.Master" %>

    
<asp:Content ID="UserList" ContentPlaceHolderID="MainContent" runat="server">
    <div class="right_col" role="main" style="min-height:100%;">
    <div class="row" style="display:block">
    <div class="col-md-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Kullanıcı Listesi</h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up" style="padding-left:18px"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                         <%System.Data.DataTable dt = new Function().DataTable("Select  id as 'ID',UserName As 'Username', UserPassword as 'Password', FirstNameLastName as 'Name-Surname', Email, PersonalPhone as 'Pers.Phone', CompanyName as 'Comp.Phone', WebSite as 'Web Site', Address, TaxOffice as 'Tax Office', TaxNumber as 'Tax Number', IsConfirm as 'Is Confirm' from users where Authorization='user'");%>
                    <table class="table" id="zero_config">
                      <thead>
                        <tr>
                          <%for (int i = 1; i < dt.Columns.Count; i++)
                               {if (i == 11) continue;%>

                                   <th><%=dt.Columns[i] %></th>
                              <% } %>
                            <th>Confirm</th>
                        </tr>
                      </thead>
                      <tbody>
                           <%for (int i = 1; i < dt.Rows.Count; i++)
                                 {%>
                                    <tr>
                                        <%for (int j = 1; j < dt.Columns.Count; j++)
                                            {
                                                if (j == 11) continue;
                                                %>
                                             <td><%=dt.Rows[i][j] %></td>
                                        
                                        <%}%>
                                        <%if (dt.Rows[i][11].ToString() == "0")
                                            {%>
                                        <td id="Confirm_<%=dt.Rows[i][0] %>" > <button onclick="$('#Confirm_<%=dt.Rows[i][0] %>').load('process.aspx?p=confirm&id=<%=dt.Rows[i][0] %>');" class=" btn btn-success"><i class="fa fa-check"></i></button></td>
                                        <%} %>
                                       <%else if (dt.Rows[i][11].ToString() == "1")
                                            {%>
                                        <td id="Confirm_<%=dt.Rows[i][0] %>" > <button onclick="$('#Confirm_<%=dt.Rows[i][0] %>').load('process.aspx?p=notconfirm&id=<%=dt.Rows[i][0] %>');" class=" btn btn-danger"><i class="fa fa-remove"></i></button></td>
                                        <%} %>
                                   </tr>
                                <%}%>
                      </tbody>
                    </table>

                  </div>
                </div>
              </div>
        </div>

     </div>

    </asp:Content>
