using Microsoft.AspNetCore.Mvc;
using api_gestao_despesas.Models;
using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupsRepository _repository;

        public GroupsController(IMapper mapper, IGroupsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var groups = await _repository.GetAll();
            var groupsResponse = new List<GroupsResponseDTO>();
            foreach (var group in groups)
            {
                groupsResponse.Add(GroupsResponseDTO.Of(group));
            }
            return Ok(groupsResponse);
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupsResponseDTO>> GetGroups(int id)
        {
            var getGroups = await _repository.GetById(id);
            if (getGroups == null)
            {
                return BadRequest("Grupo não encontrado");
            }
            return Ok(GroupsResponseDTO.Of(getGroups));
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroups(int id, GroupsRequestDTO groupsRequestDTO)
        {
            var getGroups = await _repository.GetById(id);
            if (getGroups == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o grupo");
            }
            var updateGroups = _mapper.Map<Group>(groupsRequestDTO);
            var updatedGroups = await _repository.Update(id, updateGroups);

            return Ok(_mapper.Map<GroupsResponseDTO>(updatedGroups));
        }

        // POST: api/Groups
        [HttpPost]
        public async Task<ActionResult<GroupsRequestDTO>> PostGroup(GroupsRequestDTO groupsRequestDTO)
        {
            var createGroups = _mapper.Map<Group>(groupsRequestDTO);
            if (createGroups == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o grupo");
            }
            var savedGroups = await _repository.Create(createGroups);
            return Ok(_mapper.Map<GroupsResponseDTO>(savedGroups));
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroups(int id)
        {
            var getGroups = await _repository.GetById(id);
            if (getGroups == null)
            {
                return BadRequest("Grupo não encontrado");
            }
            var deleteGroups = await _repository.Delete(id);
            return NoContent();
        }

        [HttpPost("{id}/users/{userId}")]
        public async Task<ActionResult<GroupsRequestDTO>> AddUserGroup(int id, int userId)
        {
            var findGroup = await _repository.GetById(id);
            if(findGroup == null)
            {
                return NotFound("Grupo não encontrado");
            }
            var findUser = await _repository.GetById(userId);
            if(findUser == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var addedUserGroup = await _repository.AddGroupUsers(id, userId);

            return Ok(_mapper.Map<GroupsRequestDTO>(addedUserGroup));
        }

        //[HttpPost("{id}/users/{userId}/friend/{friendId}")]
        //public async Task<ActionResult<GroupsRequestDTO>> AddUserFriendGroup(int id, int userId, int friendId)
        //{
        //    var findGroup = await _repository.GetById(id);
        //    if (findGroup == null)
        //    {
        //        return NotFound("Grupo não encontrado");
        //    }
        //    var findUser = await _repository.GetById(userId);
        //    if (findUser == null)
        //    {
        //        return NotFound("Usuário não encontrado");
        //    }
        //    var findFriend = await _repository.GetById(friendId);
        //    if(findFriend == null)
        //    {
        //        return NotFound("Amigo não encontrado");
        //    }

        //    var addedUserGroup = await _repository.AddGroupFriendsUser(id, userId, friendId);

        //    return Ok(_mapper.Map<GroupsRequestDTO>(addedUserGroup));
        //}
        //[HttpDelete("{id}/usuarios/{usuarioId}")]
        //public async Task<ActionResult> DeleteUsuario(int id, int usuarioId)
        //{
        //    var model = await _context.VeiculosUsuarios
        //        .Where(c => c.VeiculoId == id && c.UsuarioId == usuarioId)
        //        .FirstOrDefaultAsync();

        //    if (model == null) return NotFound();

        //    _context.VeiculosUsuarios.Remove(model);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}