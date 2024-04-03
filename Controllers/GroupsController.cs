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
        private readonly IGroupsRepository _repository;
        private readonly IExpenseRepository _expenseRepository;

        public GroupsController(IMapper mapper, IGroupsRepository repository, IExpenseRepository expenseRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _expenseRepository = expenseRepository;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var groups = await _repository.GetAll();
            return Ok(_mapper.Map<List<GroupsResponseDTO>>(groups));
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroups(int id)
        {
            var getGroups = await _repository.GetById(id);
            if (getGroups == null)
            {
                return BadRequest("Grupo não encontrado");
            }
            return Ok(_mapper.Map<GroupsResponseDTO>(getGroups)); ;
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroups(int id, PaymentRequestDTO groupsRequestDTO)
        {
            var getGroups = await _repository.GetById(id);
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
    }
}