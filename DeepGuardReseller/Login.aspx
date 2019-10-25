<%@ Page Language="C#"%>

<%
    {
        HttpCookie aCookie;
        string cookieName;
        int limit = Request.Cookies.Count;
        for (int i = 0; i < limit; i++)
        {
            cookieName = Request.Cookies[i].Name;
            aCookie = new HttpCookie(cookieName);
            aCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie);
        }
    }
    string errorMessage = "";
    if (Request.QueryString["i"] == "login")
    {
        if (Function.IsLogin(Request.Form["Log_username"].Trim(), Request.Form["Log_password"].Trim()))
        {
            {
                HttpCookie aCookie;
                string cookieName;
                int limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
            }
            {
                HttpCookie kullaniciCookie;
                kullaniciCookie = Request.Cookies["id"];
                kullaniciCookie = new HttpCookie("id", Function.Sifrele(Sql.Table("select id from users where UserName=" + "?1" + " or Email=" + "?1" + "", Request.Form["Log_username"].Trim()).Rows[0][0].ToString()));
                kullaniciCookie.Expires = DateTime.Now.AddMonths(6);
                //kullaniciCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(kullaniciCookie);
            }
            Response.Redirect("Default.aspx");
        }
        else
        {
            errorMessage = "Hatalı Giriş Yaptınız!";
        }
    }
    if (Request.QueryString["i"] == "register")
    {
        if (Function.IsLogin(Request.Form["Reg_username"].Trim(), Request.Form["Reg_password"].Trim()) == false)
        {
            try
            {
                           Sql.ExSql($"INSERT INTO users (UserName , UserPassword, FirstNameLastName, Email, PersonalPhone, CompanyPhone, CompanyName, WebSite, Address, TaxOffice, TaxNumber) Values (?1,?2,?3,?4,?5,?6,?7,?8,?9,10,?11)", Request.Form["Reg_username"],Request.Form["Reg_password"],Request.Form["nameSurname"],Request.Form["email"],Request.Form["personalPhone"],Request.Form["companyPhone"],Request.Form["companyName"],Request.Form["website"],Request.Form["address"],Request.Form["taxOffice"],Request.Form["TaxNumber"] );
                           Response.Redirect("Default.aspx");
            }
            catch (Exception)
            {
                errorMessage = "Bir Hata oluştu!.";
            }
        }
        else
        {
            errorMessage = "Zaten Bizim Bayimizsiniz..";
        }
    }


%>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>DeepGuard </title>
    <link rel="icon" href="images/deepguardlogo-icon.png" type="image/x-icon">
    <!-- Bootstrap -->
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="../vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- Animate.css -->
    <link href="../vendors/animate.css/animate.min.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <link href="../build/css/custom.min.css" rel="stylesheet">
  </head>

  <body class="login">
    <div>
      <a class="hiddenanchor" id="signup"></a>
      <a class="hiddenanchor" id="signin"></a>

      <div class="login_wrapper">
        <div class="animate form login_form">
          <section class="login_content">
            <form action="?i=login" method="post">
              <h1>Giriş Formu</h1>
              <div>
                <input type="text" class="form-control" name="Log_username" placeholder="Kullanıcı Adı" required="" />
              </div>
              <div>
                <input type="password" class="form-control" name="Log_password" placeholder="Şifre" required="" />
              </div>
              <div>
                <button type="submit" class="btn btn-success" style="width:100px;height:40px;">Giriş</button>
               <%-- <a class="reset_pass" href="#">Lost your password?</a>--%>
              </div>

              <div class="clearfix"></div>

              <div class="separator">
                <p class="change_link">Bayimiz Olmak İster misiniz?
                  <a href="#signup" class="to_register"> Kayıt Ol </a>
                </p>

                <div class="clearfix"></div>
                <br />

                <div>
                  <h1>
                    <img src="images/deepguardlogo-icon.png" width="35" height="40" /> DeepGuard</h1>
                  <p>©2019 All Rights Reserved. DeepGuard. Privacy and Terms</p>
                </div>
              </div>
            </form>
          </section>
        </div>

        <div id="register" class="animate form registration_form">
          <section class="login_content">
            <form action="?i=register" method="post">
              <h1>Kayıt Formu</h1>
               <div>
                <input type="text" class="form-control" name="Reg_username" placeholder="Kullanıcı Adı" required="required" />
              </div>
              <div>
                <input type="password" class="form-control" name="Reg_password" placeholder="Şifre" required="required" />
              </div>
               <div>
                <input type="text" class="form-control" name="nameSurname" placeholder="Ad Soyad" required="required" />
              </div>
              <div>
                <input type="email" class="form-control" name="email" placeholder="E-Mail" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" name="personalPhone" placeholder="Kişisel Telefon" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" name="companyPhone" placeholder="Firma Telefonu" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" name="companyName" placeholder="Firma Adı" required="required" />
              </div>
                <div>
                <input type="text" class="form-control" name="webSite" placeholder="Web Sitesi" required="required" />
              </div>
                <div>
                    <textarea class="form-control" rows="3" cols="40" name="address" placeholder="Adres">Adres..
                    </textarea>
              </div>
              <div style="margin-top:5%">
                <input type="text" class="form-control" name="taxOffice" placeholder="Vergi Dairesi" required="required" />
              </div>
                <div>
                <input type="text" class="form-control" name="taxNumber" placeholder="Vergi Numarası" required="required" />
              </div>
              <div>
                <%--<a class="btn btn-default submit" href="index.html">Giriş</a>--%>
              <%--   <a class="reset_pass" href="#">Lost your password?</a>--%>
              </div>
              <div>
                <button type="submit" class="btn btn-success" style="width:100px;height:40px;">Kayıt Ol</button>
                <%--<a class="btn btn-default submit" href="index.html">Kayıt Ol</a>--%>
              </div>

              <div class="clearfix"></div>

              <div class="separator">
                <p class="change_link">Zaten bayimiz misinz? 
                    
                  <a href="#signin" class="to_register" onclick="scrollToTop(1000);"> Giriş Yap </a>
                </p>

                <div class="clearfix"></div>
                <br />
                <div>
                  <h1><img src="images/deepguardlogo-icon.png" width="35" height="40" /> DeepGuard</h1>
                  <p>©2019 All Rights Reserved DeepGuard. Privacy and Terms</p>
                </div>
              </div>
            </form>
          </section>
        </div>
      </div>
    </div>
  </body>
    <script>
        function scrollToTop(scrollDuration) {
    var scrollStep = -window.scrollY / (scrollDuration / 500),
        scrollInterval = setInterval(function(){
        if ( window.scrollY != 0 ) {
            window.scrollBy( 0, scrollStep );
        }
        else clearInterval(scrollInterval); 
    },15);
        }
    </script>
</html>

