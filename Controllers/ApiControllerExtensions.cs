using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace knowledgeBase.Controllers
{
    public static class ApiControllerExtensions
    {
        public static string GetUsername(this ApiController controller)
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
