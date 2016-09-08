using System.Web;
using System.Web.Mvc;
using CatService.Filters;

namespace CatService
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
            filters.Add(new CatAuthorizationFilterAttribute());
		}
	}
}
