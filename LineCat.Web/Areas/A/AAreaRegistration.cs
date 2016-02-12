using System.Web.Mvc;

namespace LineCat.Web.Areas.A
{
    public class AAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "A";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "A_product",
            //    "a/p/{id}",
            //    new { controller = "p", action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "A_default",
                "a/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional }
            );
        }
    }
}