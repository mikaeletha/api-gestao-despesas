namespace api_gestao_despesas.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using api_gestao_despesas.Models; // Importe o namespace que contém a classe Group
using global::api_gestao_despesas.Repository.Interface;



public class OwnerAuthorizationHandler : AuthorizationHandler<OwnerRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGroupsRepository _repository; // Adicione o repositório de grupos aqui

    public OwnerAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IGroupsRepository repository)
    {
        _httpContextAccessor = httpContextAccessor;
        _repository = repository; // Injete o repositório de grupos
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var groupIdString = _httpContextAccessor.HttpContext?.Request.RouteValues["id"].ToString();

        if (userId != null && groupIdString != null)
        {
            var groupId = int.Parse(groupIdString);
            var group = await _repository.GetById(groupId); // Aguarde a conclusão da tarefa para obter o objeto Group
            var isOwner = IsUserGroupOwner(userId, group); // Passe o objeto Group para verificar se o usuário é o proprietário
            if (isOwner)
            {
                context.Succeed(requirement);
            }
        }
    }

    private bool IsUserGroupOwner(string userId, Group group)
    {
        // Verifique se o usuário é o proprietário do grupo
        return group != null && group.OwnerId.Equals(userId);
    }
}

