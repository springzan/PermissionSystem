using Permission.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.application.UserApp
{
    public interface IUserAppService
    {
        User CheckUser(string userName, string password);
    }
}
