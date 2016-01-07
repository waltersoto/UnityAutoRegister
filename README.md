# UnityAutoRegister

This library will auto-register types using the Unity Container. There are not many configuration options since the main reason I am using
this is to inject dependencies in ASP.NET MVC controllers (similar usage to the example below). 

###Usage

Define an interface:
```cs
 public interface IUserService
 {
        bool Authorize(string username, string password);
        IList<string> UserList();
 }
```

Implement a class with the interface decorated with the **Implement** attribute:
```cs
    [Implement(typeof(IUserService))]
    public class UserService : IUserService
    {
        public bool Authorize(string username, string password)
        {
            return username.Equals("test", StringComparison.InvariantCultureIgnoreCase) &&
                   password.Equals("test", StringComparison.InvariantCultureIgnoreCase);
        }

        public IList<string> UserList()
        {
            return new List<string>() { "User 1", "User 2", "User 3" };
        }
    }
```

Using it in ASP.NET MVC execute the following method in **Global.asax**:

```cs
    protected void Application_Start()
    {
        AutoRegister.AllInMvc(new UnityContainer()); 
    }
```

Now you can just let Unity inject the dependencies in your controllers:
```cs
    public class HomeController : Controller
    {
        private readonly IUserService service;

        public HomeController(IUserService userService)
        {
            service = userService;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Result = service.Authorize("test", "test") ? "OK" : "FAILED";
            return View();
        }
    }
```



