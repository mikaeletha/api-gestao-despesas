using Microsoft.AspNetCore.Mvc;
using api_gestao_despesas.Models;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendRepository _repository;

        public FriendsController(IMapper mapper, IFriendRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }



        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var friends = await _repository.GetAll();
            return Ok(_mapper.Map<List<FriendResponseDTO>>(friends));
        }

        // GET: api/Friends/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<Friend>> GetFriend(int Id)
        {
            var getFriend = await _repository.GetById(Id); // Procura um amigo por ID
            if (getFriend == null)
            {
                return BadRequest("Amigo não encontrada");
            }

            return Ok(_mapper.Map<FriendResponseDTO>(getFriend)); ;
        }

        //// PUT: api/Friends/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutFriend(int Id, FriendRequestDTO friendRequestDTO)
        {
            var findExpense = await _repository.GetById(Id);
            if (findExpense == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o amigo");
            }
            var updateFriend = _mapper.Map<Friend>(friendRequestDTO);
            var updatedFriend = await _repository.Create(updateFriend);

            return Ok(_mapper.Map<FriendResponseDTO>(updatedFriend));
        }

        // POST: api/Friends
        [HttpPost]
        public async Task<ActionResult<Friend>> PostFriend(FriendRequestDTO friendRequestDTO)
        {
            var createFriend = _mapper.Map<Friend>(friendRequestDTO);
            if (createFriend == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o amigo");
            }
            var savedFriend = await _repository.Create(createFriend);
            return Ok(_mapper.Map<FriendResponseDTO>(savedFriend));
        }

        // DELETE: api/Friends/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFriend(int Id)
        {
            var getFriend = await _repository.GetById(Id);
            if (getFriend == null)
            {
                return BadRequest("Amigo não encontrada");
            }
            var deleteFriend = await _repository.Delete(Id);
            return NoContent();
        }
    }
}