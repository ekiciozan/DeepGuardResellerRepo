<%@ Page Language="C#"%>

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
            <form>
              <h1>Giriş Formu</h1>
              <div>
                <input type="text" class="form-control" id="Log_kullanici_adi" placeholder="Kullanıcı Adı" required="" />
              </div>
              <div>
                <input type="password" class="form-control" id="Log_sifre" placeholder="Şifre" required="" />
              </div>
              <div>
                <button type="button" class="btn btn-success" style="width:100px;height:40px;">Giriş</button>
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
            <form>
              <h1>Kayıt Formu</h1>
               <div>
                <input type="text" class="form-control" id="Kayit_kullanici_adi" placeholder="Kullanıcı Adı" required="required" />
              </div>
              <div>
                <input type="password" class="form-control" id="Kayit_sifre" placeholder="Şifre" required="required" />
              </div>
               <div>
                <input type="text" class="form-control" id="ad_soyad" placeholder="Ad Soyad" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" id="email" placeholder="E-Mail" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" id="kisisel_tel" placeholder="Kişisel Telefon" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" id="firma_tel" placeholder="Firma Telefonu" required="required" />
              </div>
              <div>
                <input type="text" class="form-control" id="firma_ad" placeholder="Firma Adı" required="required" />
              </div>
                <div>
                <input type="text" class="form-control" id="web_site" placeholder="Web Sitesi" required="required" />
              </div>
                <div>
                    <textarea class="form-control" rows="3" cols="40" id="adres" placeholder="Adres">Adres..
                    </textarea>
              </div>
              <div style="margin-top:5%">
                <input type="text" class="form-control" id="vergi_dairesi" placeholder="Vergi Dairesi" required="required" />
              </div>
                <div>
                <input type="text" class="form-control" id="vergi_numarasi" placeholder="Vergi Numarası" required="required" />
              </div>
              <div>
                <%--<a class="btn btn-default submit" href="index.html">Giriş</a>--%>
              <%--   <a class="reset_pass" href="#">Lost your password?</a>--%>
              </div>
              <div>
                <button type="button" class="btn btn-success" style="width:100px;height:40px;">Kayıt Ol</button>
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

