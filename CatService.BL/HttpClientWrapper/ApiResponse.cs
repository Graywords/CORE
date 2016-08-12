using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CatService.BL.Enums;
using Newtonsoft.Json;

namespace CatService.BL.HttpClientWrapper
{
	public sealed class ApiResponse
	{
		private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			TypeNameHandling = TypeNameHandling.All
		};

		public readonly Uri RequestUri;
		public readonly ApiRequestMethod RequestMethod;
		public readonly string RequestContent;
		public readonly HttpContent RequestHttpContent;

		public Exception WebError { get; internal set; }
		public HttpStatusCode Status { get; internal set; }

		public bool Success
		{
			get { return (int)Status >= 200 && (int)Status < 300; }
		}

		public HttpResponseHeaders Headers { get; internal set; }
		public HttpContentHeaders ContentHeaders { get; internal set; }
		public string Raw { get; internal set; }
		public byte[] RawBytes { get; private set; }

		public T Deserialize<T>()
		{
			if (string.IsNullOrWhiteSpace(Raw) || ContentHeaders == null)
				return default(T);
			var contentType = ContentHeaders.ContentType;

			if (contentType == null)
				return default(T);

			if (contentType.MediaType == "application/json")
				return JsonConvert.DeserializeObject<T>(Raw, this.serializerSettings);

			if (contentType.MediaType == "text/plain" && typeof(string) == typeof(T))
				return (T)Convert.ChangeType(Raw, typeof(T));

			return default(T);
		}

		public ApiErrorData ErrorData()
		{
			return Success ? null : Deserialize<ApiErrorData>();
		}

		internal ApiResponse(Uri url, ApiRequestMethod method, string content, HttpContent httpContent)
		{
			this.RequestUri = url;
			this.RequestMethod = method;
			this.RequestContent = content;
			this.RequestHttpContent = httpContent;
		}

		public static HttpContent CreateUploadFileContent(string name, string fileName, byte[] content)
		{
			var requestContent = new MultipartFormDataContent();
			var imageContent = new ByteArrayContent(content);
			imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

			requestContent.Add(imageContent, name, fileName);

			return requestContent;
		}

		internal HttpContent GetContent()
		{
			if (this.RequestHttpContent != null)
				return this.RequestHttpContent;

			return this.RequestContent == null
				? null
				: new StringContent(this.RequestContent, Encoding.UTF8, "application/json");
		}

		internal void SetResponse(HttpResponseMessage responseMessage)
		{
			Status = responseMessage.StatusCode;
			Headers = responseMessage.Headers;
			if (responseMessage.Content != null)
			{
				ContentHeaders = responseMessage.Content.Headers;
				Raw = responseMessage.Content.ReadAsStringAsync().Result;
			}
		}

		internal async Task SetResponseAsync(HttpResponseMessage responseMessage)
		{
			Status = responseMessage.StatusCode;
			Headers = responseMessage.Headers;
			if (responseMessage.Content != null)
			{
				ContentHeaders = responseMessage.Content.Headers;
				Raw = await responseMessage.Content.ReadAsStringAsync();
				RawBytes = await responseMessage.Content.ReadAsByteArrayAsync();
			}
		}
	}

	public class ApiErrorData
	{
		public string ErrorCode { get; set; }
		public string Message { get; set; }
	}
}
