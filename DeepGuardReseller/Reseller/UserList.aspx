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
                          <%for (int i = 0; i < dt.Columns.Count; i++)
                               {%>
                                   <th><%=dt.Columns[i] %></th>
                              <% } %>
                            <th>Confirm</th>
                        </tr>
                      </thead>
                      <tbody>
                           <%for (int i = 0; i < dt.Rows.Count; i++)
                                 {%>
                                    <tr>
                                        <%for (int j = 0; j < dt.Columns.Count; j++)
                                            {%>
                                             <td><%=dt.Rows[i][j] %></td>
                                        
                                        <%}%>
                                        <%if (dt.Rows[i][11].ToString() == "0")
                                            {%>
                                        <td> <button onclick="location.href = 'process.aspx?p=confirm&id=<%=dt.Rows[i][0] %>';" class=" btn btn-success"><i class="fa fa-check"></i></button></td>
                                        <%} %>
                                       <%else if (dt.Rows[i][11].ToString() == "1")
                                            {%>
                                        <td> <button onclick="location.href = 'process.aspx?p=notconfirm&id=<%=dt.Rows[i][0] %>';" class=" btn btn-danger"><i class="fa fa-remove"></i></button></td>
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


    
       
      


    <%-- <div class="right_col" role="main">
          <!-- top tiles -->
          <div class="row tile_count">
   <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Stripped table <small>Stripped table subtitle</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <a class="dropdown-item" href="#">Settings 1</a>
                            <a class="dropdown-item" href="#">Settings 2</a>
                          </div>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">

                    <table class="table table-striped">
                      <thead>
                        <tr>
                          <th>#</th>
                          <th>First Name</th>
                          <th>Last Name</th>
                          <th>Username</th>
                            <th>Username</th>
                            <th>Username</th>
                            <th>Username</th>

                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th scope="row">1</th>
                          <td>Mark</td>
                          <td>Otto</td>
                          <td>@mdo</td>
                          <td>Mark</td>
                          <td>Otto</td>
                          <td>@mdo</td>
                        </tr>
                        <tr>
                          <th scope="row">2</th>
                          <td>Jacob</td>
                          <td>Thornton</td>
                          <td>@fat</td>
                            <td>Mark</td>
                          <td>Otto</td>
                          <td>@mdo</td>
                        </tr>
                        <tr>
                          <th scope="row">3</th>
                          <td>Larry</td>
                          <td>the Bird</td>
                          <td>@twitter</td>
                            <td>Mark</td>
                          <td>Otto</td>
                          <td>@mdo</td>
                        </tr>
                      </tbody>
                    </table>

                  </div>
                </div>
              </div>
        </div>--%>
    </asp:Content>
