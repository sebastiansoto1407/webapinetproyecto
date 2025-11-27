using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SistemaGestionDocumentos.API.Utils
{
    /// <summary>
    /// Configuracion para que Swagger maneje correctamente IFormFile
    /// </summary>
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operacion, OperationFilterContext contexto)
        {
            var parametroFormFile = contexto.ApiDescription.ActionDescriptor
                .Parameters.FirstOrDefault(p => p.ParameterType == typeof(IFormFile));

            if (parametroFormFile != null)
            {
                if (operacion.RequestBody == null)
                {
                    operacion.RequestBody = new OpenApiRequestBody();
                }

                operacion.RequestBody.Content.Clear();
                operacion.RequestBody.Content.Add("multipart/form-data", new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties = new Dictionary<string, OpenApiSchema>()
                        {
                            { "archivo", new OpenApiSchema() { Type = "string", Format = "binary" } }
                        }
                    }
                });
            }
        }
    }
}
