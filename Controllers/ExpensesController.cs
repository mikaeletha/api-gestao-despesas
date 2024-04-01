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
    public class ExpensesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _repository;

        public ExpensesController(IMapper mapper, IExpenseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
           var expenses = await _repository.GetAll();
           return Ok(_mapper.Map<List<ExpenseResponseDTO>>(expenses));
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense([FromRoute] int id)
        {
            var getExpense = await _repository.GetById(id); // Procura uma despesa por ID
            if (getExpense == null)
            {
                return BadRequest("Despesa não encontrada");
            }
            return Ok(_mapper.Map<ExpenseResponseDTO>(getExpense)); ;
        }

        //// PUT: api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense([FromRoute]int id, [FromBody] ExpenseRequestDTO expenseRequestDTO)
        {
            var findExpense = await _repository.GetById(id);
            if (findExpense == null)
            {
                return BadRequest("Despesa não encontrada");
            }
            var expense = _mapper.Map<Expense>(expenseRequestDTO);
            var updatedExpense = await _repository.Update(id, expense);

            if (updatedExpense == null)
            {
                return BadRequest("Ocorreu um erro ao atualizar a despesa");
            }
            return Ok(_mapper.Map<ExpenseResponseDTO>(updatedExpense));
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense([FromBody] ExpenseRequestDTO expenseRequestDTO)
        {
            var createExpense = _mapper.Map<Expense>(expenseRequestDTO);
            if (createExpense == null)
            {
                return BadRequest("Ocorreu um erro ao incluir a despesa ");
            }
            var savedExpense = await _repository.Create(createExpense);
            return Ok(_mapper.Map<ExpenseResponseDTO>(savedExpense));
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            var getExpense = await _repository.GetById(id);
            if (getExpense == null)
            {
                return BadRequest("Despesa não encontrada");
            }
            var deleteExpense = await _repository.Delete(id);
            return NoContent();
        }
    }
}