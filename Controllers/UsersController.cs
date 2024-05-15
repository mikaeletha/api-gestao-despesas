using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using api_gestao_despesas.Repository.Implementation;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Authenticate(LoginDTO model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            var usuarioDb = await _repository.GetByEmail(model.Email);

            if (usuarioDb == null)
            {
                return Unauthorized("Usuário ou senha incorretos.");
            }

            try
            {
                var isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, usuarioDb.Password);
                if (!isPasswordValid)
                {
                    return Unauthorized("Usuário ou senha incorretos.");
                }

                var token = await _repository.Login(usuarioDb);
                return Ok(new { jwtToken = token });
            }
            catch (Exception ex)
            {
                // Captura exceções genéricas, pode ser ajustado para capturar tipos específicos de exceção
                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação.");
            }
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            // Não é necessário fazer nada aqui para invalidar o token JWT, pois o JWT é baseado em estado.
            // O token JWT é autônomo e expira após um período de tempo configurado.
            return Ok("Logout realizado com sucesso.");
        }

        [HttpPut("{id}/update-password")]
        public async Task<IActionResult> UpdatePassword(int id, UpdateUserPassword updatePasswordDTO)
        {
            var usuarioDb = await _repository.GetByEmail(updatePasswordDTO.Email);
            var getUser = await _repository.GetById(id);
            if (id != getUser.Id || usuarioDb.Email != updatePasswordDTO.Email)
            {
                return BadRequest("ID do usuário na URL não corresponde ao ID do usuário no corpo da solicitação.");
            }

            try
            {
                var user = _mapper.Map<User>(updatePasswordDTO);
                var updatedUser = await _repository.UpdatePassword(id, user);
                if (updatedUser == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok("Senha atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar a senha: {ex.Message}");
            }
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

        [HttpPost("register")]
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
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UpdateUserRequestDTO updateUserRequestDTO)
        {
            var findUser = await _repository.GetById(id);
            if (findUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            var user = _mapper.Map<User>(updateUserRequestDTO);
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
