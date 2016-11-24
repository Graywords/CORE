
using CatService.BL.Models;

namespace CatService.BL.Infrastructure.CatExtensionsTools.Interfaces
{
	public interface ICatSupportToolsService
	{
		string GetCouchUuid();

		CatDocument GetHtml(string url);

	}
}
