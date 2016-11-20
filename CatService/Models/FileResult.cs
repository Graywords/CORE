using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CatService.Models
{
	public class FileResult : IHttpActionResult
	{
		private readonly byte[] content;
		private readonly string contentType;
		private readonly string name;
		private readonly bool forceDownload;

		public FileResult(byte[] content, string contentType, string name, bool forceDownload = true)
		{
			this.content = content;
			this.contentType = contentType;
			this.name = name;
			this.forceDownload = forceDownload;
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var stream = new MemoryStream(this.content);
			result.Content = new StreamContent(stream);
			result.Content.Headers.ContentType = new MediaTypeHeaderValue(this.contentType);
			result.Content.Headers.ContentDisposition = this.forceDownload
				? new ContentDispositionHeaderValue("attachment") { FileName = this.name }
				: new ContentDispositionHeaderValue("inline") { FileName = this.name };

			return Task.FromResult(result);
		}
	}
}