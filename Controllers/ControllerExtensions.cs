using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace knowledgeBase.Controllers
{
    public static class ControllerExtensions
    {
        public static string GetUsername(this Controller controller)
        {
            string username = controller.User.Identity.Name;
            if (username.IndexOf('\\') > 0)
            {
                username = username.Substring(username.IndexOf('\\') + 1);
            }
            return username?.ToLower();
        }
    }
}
