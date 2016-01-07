using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityContainerWeb.Models
{
    public interface IUserService
    {
        bool Authorize(string username, string password);
        IList<string> UserList();
    }
}
