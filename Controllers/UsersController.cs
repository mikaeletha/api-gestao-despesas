using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Implementation;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol.Core.Types;
using System.Linq;

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IFriendRepository _friendRepository;

        public UsersController(IMapper mapper, IUserRepository repository, IFriendRepository friendRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _friendRepository = friendRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _repository.GetAll();
            var usersResponse = new List<UserResponseDTO>();
            foreach ( var user in users)
            {

                var listFriends = _friendRepository.GetAllByUser(user.Id).Result;
                var userIds = new List<int>();
                foreach (var friend in listFriends)
                {
                    userIds.Add(friend.FriendId);
                }
                var friends = _repository.GetAllByIds(userIds);
                usersResponse.Add(UserResponseDTO.Of(user, friends.Result));
            }
            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser([FromRoute] int id)
        {
            var getUser = await _repository.GetById(id); // Procura uma despesa por ID
            if (getUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            var listFriends = _friendRepository.GetAllByUser(id).Result;
            var userIds = new List<int>();
            foreach (var friend in listFriends)
            {
                userIds.Add(friend.FriendId);
            }
            var friends = _repository.GetAllByIds(userIds);
            return Ok(UserResponseDTO.Of(getUser, friends.Result));
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserRequestDTO userRequestDTO)
        {
            var createUser = _mapper.Map<User>(userRequestDTO);
            createUser.Name = userRequestDTO.Name;
            createUser.Email = userRequestDTO.Email;
            createUser.Password = BCrypt.Net.BCrypt.HashPassword(userRequestDTO.Password);
            createUser.PhoneNumber = userRequestDTO.PhoneNumber;

            var savedUser = await _repository.Create(createUser);
            return Ok(_mapper.Map<UserResponseDTO>(savedUser));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UserRequestDTO userRequestDTO)
        {
            var findUser = await _repository.GetById(id);
            if (findUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            var user = _mapper.Map<User>(userRequestDTO);
            var updatedUser = await _repository.Update(id, user);

            if (updatedUser == null)
            {
                return BadRequest("Ocorreu um erro ao atualizar o usuário");
            }
            return Ok(_mapper.Map<UserResponseDTO>(updatedUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var getUser = await _repository.GetById(id);
            if (getUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            var deleteUser = await _repository.Delete(id);
            return NoContent();
        }
    }
}
