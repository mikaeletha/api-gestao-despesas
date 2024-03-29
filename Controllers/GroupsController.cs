using Microsoft.AspNetCore.Mvc;
using api_gestao_despesas.Models;
using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _repository;

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
            return Ok(_mapper.Map<List<GroupsResponseDTO>>(groups));
        }

        // GET: api/Groups/5
        [HttpGet("{IdGroups}")]
        public async Task<ActionResult<Group>> GetGroups(int IdGroups)
        {
            var getGroups = await _repository.GetById(IdGroups);
            if (getGroups == null)
            {
                return BadRequest("Grupo não encontrado");
            }

            return Ok(_mapper.Map<GroupsResponseDTO>(getGroups)); ;
        }

        // PUT: api/Groups/5
        [HttpPut("{IdGroups}")]
        public async Task<IActionResult> PutGroups(int IdGroups, PaymentRequestDTO groupsRequestDTO)
        {
            var getGroups = await _repository.GetById(IdGroups);
            if (getGroups == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o grupo");
            }
            var updateGroups = _mapper.Map<Group>(groupsRequestDTO);
            var updatedGroups = await _repository.Create(updateGroups);

            return Ok(_mapper.Map<GroupsResponseDTO>(updatedGroups));
        }

        // POST: api/Groups
        [HttpPost]
        public async Task<ActionResult<Group>> PostPayment(GroupsRequestDTO groupsRequestDTO)
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
        [HttpDelete("{IdGroups}")]
        public async Task<IActionResult> DeleteGroups(int IdGroups)
        {
            var getGroups = await _repository.GetById(IdGroups);
            if (getGroups == null)
            {
                return BadRequest("Grupo não encontrado");
            }
            var deleteGroups = await _repository.Delete(IdGroups);
            return NoContent();
        }
    }
}
