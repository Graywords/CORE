using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatService.Infrastructure
{
    public class UserInformationService
    {
        private static UserInformationService _instance;

        private UserInformationService()
        {
        }

        public static UserInformationService GetInstance()
        {
            if (_instance == null) _instance = new UserInformationService();
            return _instance;
        }
    }
}