using Kjetil.Todo.Persistence;
using Kjetil.Todo.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string GetUsers()
    {
        using (var unitOfWork = new UnitOfWork(new TodoContext()))
        {
            var users = unitOfWork.Users.GetAll();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var json = jss.Serialize(users);

            return json;
        }
    }

    [WebMethod]
    public string GetTodoListItemsForUser(string str_id)
    {
        var json = "";
        int id = 0;
        if (Int32.TryParse(str_id, out id))
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                var users = unitOfWork.Users.GetTodoItemsForUser(id);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                json = jss.Serialize(users);
            }
        }
        return json;
    }

}
