namespace api_gestao_despesas.Properties;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        // Adiciona o cabeçalho "Authorization" ao Swagger UI
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Description = "Token JWT de autenticação",
            Required = false // Se necessário, altere para true se todas as operações exigirem autorização
        });
    }
}
