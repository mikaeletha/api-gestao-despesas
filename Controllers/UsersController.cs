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

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UsersController(IMapper mapper, IUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _repository.GetAll();
            var usersResponse = new List<UserResponseDTO>();
            foreach ( var user in users)
            {
                usersResponse.Add(UserResponseDTO.Of(user));
            }
            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int id)
        {
            var getUser = await _repository.GetById(id); // Procura uma despesa por ID
            if (getUser == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            return Ok(UserResponseDTO.Of(getUser)); ;
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
