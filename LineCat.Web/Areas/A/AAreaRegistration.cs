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
            context.MapRoute(
                "A_default",
                "A/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}