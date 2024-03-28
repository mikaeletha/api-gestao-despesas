//using api_gestao_despesas.DTO.Request;
//using api_gestao_despesas.DTO.Response;
//using api_gestao_despesas.Models;
//using api_gestao_despesas.Repository.Interface;
//using api_gestao_despesas;
//using AutoMapper;
//using api_gestao_despesas.Service.Interface;
//using NuGet.Protocol.Core.Types;


//namespace api_gestao_despesas.Service.Implementation
//{
//    public class ExpenseService : IExpenseService
//    {

//        private readonly IMapper _mapper;
//        private readonly IExpenseRepository _repository;

//        public ExpenseService(IMapper mapper, IExpenseService _expenseService)
//        {
//            _mapper = mapper;
//        }

//        public async Task<ExpenseResponseDTO> Create(ExpenseRequestDTO expenseRequestDTO)
//        {
//            var expense = _mapper.Map<Expense>(expenseRequestDTO);
//            var savedExpense = await _repository.Create(expense);
//            return _mapper.Map<ExpenseResponseDTO>(savedExpense);
//        }

//        public async Task<List<ExpenseResponseDTO>> GetAll()
//        {
//            var expenses = await _repository.GetAll();
//            return _mapper.Map<List<ExpenseResponseDTO>>(expenses);
//        }

//        public async Task<ExpenseResponseDTO> GetById(int id)
//        {
//            var expense = await _repository.GetById(id); // Procura uma despesa por ID
//            return _mapper.Map<ExpenseResponseDTO>(expense);
//        }

//        public async Task<ExpenseResponseDTO> Update(int id, ExpenseRequestDTO expenseRequestDTO)
//        {
//            var expense = _mapper.Map<Expense>(expenseRequestDTO);
//            var updateExpense = await _repository.Update(expense);
//            return _mapper.Map<ExpenseResponseDTO>(updateExpense);
//        }

//        public async Task<ExpenseResponseDTO> Delete(int id)
//        {
//            var deleteExpense = await _repository.Delete(id);
//            return _mapper.Map<ExpenseResponseDTO>(deleteExpense);
//        }
//    }
//}
