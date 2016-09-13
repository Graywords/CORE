using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CatService.Infrastructure.Interfaces;

namespace CatService.Infrastructure
{
    public class CurrentUserInformationServiceSet : ICurrentUserInformationServiceSet
    {
        public void SetUserId(string userId)
        {
            UserInformationService.GetInstance().SetUiid(userId);
        }
    }
}