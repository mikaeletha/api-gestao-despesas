using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Implementation;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendRepository _repository;
        private readonly IUserRepository _userRepository;

        public FriendsController(IMapper mapper, IFriendRepository repository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
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
        public async Task<IActionResult> PutFriend(int id, FriendRequestDTO friendRequestDTO)
        {
            var findFriend = await _repository.GetById(id);
            if (findFriend == null)
            {
                return NotFound("Amigo não encontrado");
            }
            var updateFriend = _mapper.Map<Friend>(friendRequestDTO);
            var updatedFriend = await _repository.Update(id, updateFriend);
            if(updatedFriend == null)
            {
                return BadRequest("Ocorreu um erro ao atualizar o amigo");
            }

            return Ok(_mapper.Map<FriendResponseDTO>(updatedFriend));
        }

        // POST: api/Friends
        [HttpPost]
        public async Task<ActionResult<Friend>> PostFriend(FriendRequestDTO friendRequestDTO)
        {
            var user = await _userRepository.GetById(friendRequestDTO.UserId);
            if(user  == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var createFriend = _mapper.Map<Friend>(friendRequestDTO);
            createFriend.userId = friendRequestDTO.UserId;
            createFriend.User = user;
            if (createFriend == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o amigo");
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
                return BadRequest("Amigo não encontrado");
            }
            var deleteFriend = await _repository.Delete(Id);
            return NoContent();
        }
    }
}
