﻿using System.Net;
using System.Text.Json;

namespace Front.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error; 
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

		
            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado.";
            }
            if (statusCode == HttpStatusCode.BadRequest)
            {
                var validationErrorResponse = JsonSerializer.Deserialize<ValidationErrorResponse>(await HttpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                if (validationErrorResponse != null) {
					return string.Join("\n", validationErrorResponse.Errors.SelectMany(e => e.Value));
				}
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esta operación.";
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permisos para hacer esta operación.";
            }
            if (statusCode == HttpStatusCode.Conflict)
            {
                return "Cliente ya se encuentra registrado.";
            }
            



            return "Ha ocurrido un error inesperado.";
        }
    }
}