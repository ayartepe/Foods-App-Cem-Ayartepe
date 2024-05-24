using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MVC.Controllers.Bases
{
    public abstract class MvcControllerBase : Controller
    {
        protected MvcControllerBase()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US"); // new CultureInfo("tr-TR")
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
