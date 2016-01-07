
using System.Collections.Generic;


namespace UnityAutoRegisterSample
{
    public interface IUserService
    {
        bool Authorize(string username, string password);
        IList<string> UserList();
    }
}
