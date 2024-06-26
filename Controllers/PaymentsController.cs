﻿using Microsoft.AspNetCore.Mvc;
using api_gestao_despesas.Models;
using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Repository.Interface;
using AutoMapper;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;

namespace api_gestao_despesas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _repository;
        private readonly IExpenseRepository _expenseRepository;

        public PaymentsController(IMapper mapper, IPaymentRepository repository, IExpenseRepository expenseRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _expenseRepository = expenseRepository;
        }

        // GET: api/Payments
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var payments = await _repository.GetAll();
            return Ok(_mapper.Map<List<PaymentResponseDTO>>(payments));
        }

        // GET: api/Payments/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment([FromRoute] int id)
        {
            var getPayment = await _repository.GetById(id);
            if (getPayment == null)
            {
                return BadRequest("Pagamento não encontrado");
            }

            return Ok(_mapper.Map<PaymentResponseDTO>(getPayment)); ;
        }

        // PUT: api/Payments/5/expenseId
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment([FromRoute]int id,[FromBody] PaymentRequestDTO paymentRequestDTO)
        {
            var findPayment = await _repository.GetById(id);
            if (findPayment == null)
            {
                return BadRequest("Pagamento não encontrado");
            }

            var updatePayment = _mapper.Map<Payment>(paymentRequestDTO);
            var updatedPayment = await _repository.Update(id, updatePayment);

            if (updatedPayment == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o pagamento");
            }

            return Ok(_mapper.Map<PaymentResponseDTO>(updatedPayment));
        }

        // POST: api/Payments
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment([FromBody] PaymentRequestDTO paymentRequestDTO)
        {
            var expense = await _expenseRepository.GetById(paymentRequestDTO.expenseId);
            if (expense == null)
            {
                return BadRequest("Despesa não encontrada");
            }

            var createPayment = _mapper.Map<Payment>(paymentRequestDTO);
            createPayment.ExpenseId = paymentRequestDTO.expenseId;
            createPayment.Expense = expense;    

            var savedPayment = await _repository.Create(createPayment);
            return Ok(_mapper.Map<PaymentResponseDTO>(savedPayment));
        }

        // DELETE: api/Payments/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment([FromRoute]int id)
        {
            var getPayment = await _repository.GetById(id);
            if (getPayment == null)
            {
                return BadRequest("Pagamento não encontrado");
            }
            var deletePayment = await _repository.Delete(id);
            return NoContent();
        }
    }
}
