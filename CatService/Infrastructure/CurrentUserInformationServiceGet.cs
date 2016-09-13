using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CatService.Infrastructure.Interfaces;

namespace CatService.Infrastructure
{
    public class CurrentUserInformationServiceGet : ICurrentUserInformationServiceGet
    {
        public string GetUserId()
        {
             return UserInformationService.GetInstance().GetUiid();
        }
    }
}