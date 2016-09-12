using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatService.Infrastructure.Interfaces
{
    interface ICurrentUserInformationServiceSet
    {
        bool SetUserId(string userId);
    }
}
