

namespace Shared.Responses
{
	public class ActionResponse<T>
	{
		public bool WasSuccess { get; set; }
		public string? Message { get; set; }
		public int CodigoHTTP { get; set; }
		public T? Result { get; set; }

	}
}
