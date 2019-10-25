using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeepGuardReseller
{
    public partial class Site : System.Web.UI.MasterPage
    {
        
        public HttpCookie kullaniciCookie;

        protected void Page_Load(object sender, EventArgs e)
        {
            kullaniciCookie = Request.Cookies["id"];
            if (kullaniciCookie == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}