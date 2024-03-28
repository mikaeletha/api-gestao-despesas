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
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _repository;

        public PaymentsController(IMapper mapper, IPaymentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }



        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var payments = await _repository.GetAll();
            return Ok(_mapper.Map<List<PaymentResponseDTO>>(payments));
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var getPayment = await _repository.GetById(id);
            if (getPayment == null)
            {
                return BadRequest("Pagamento não encontrado");
            }

            return Ok(_mapper.Map<PaymentResponseDTO>(getPayment)); ;
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentRequestDTO paymentRequestDTO)
        {
            var getPayment = await _repository.GetById(id);
            if (getPayment == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o pagamenyto");
            }
            var updatePayment = _mapper.Map<Payment>(paymentRequestDTO);
            var updatedPayment = await _repository.Create(updatePayment);

            return Ok(_mapper.Map<PaymentResponseDTO>(updatedPayment));
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentRequestDTO paymentRequestDTO)
        {
            var createPayment = _mapper.Map<Payment>(paymentRequestDTO);
            if (createPayment == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o pagamento");
            }
            var savedPayment = await _repository.Create(createPayment);
            return Ok(_mapper.Map<PaymentResponseDTO>(savedPayment));
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
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
