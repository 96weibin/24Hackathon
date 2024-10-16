using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace knowledgeBase.DAL
{
    public class DbContextFactory
    {        
        public static Model1Container GetDbContext()
        {
            //在HttpContext中有一个Items属性，它也可以用来保存key-value，一次请求正好对应着一个HttpContext，请求结束，它自动释放，EF上下文也就不存在了。
            Model1Container dbContext = HttpContext.Current.Items["dbContext"] as Model1Container;
            if (dbContext == null)
            {
                dbContext = new Model1Container();
                HttpContext.Current.Items["dbContext"] = dbContext;
            }
            return dbContext;
        }

        public static int SaveChanges()
        {
            return DbContextFactory.GetDbContext().SaveChanges();
        }
    }
}
