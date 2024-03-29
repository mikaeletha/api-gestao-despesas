//using api_gestao_despesas.DTO;
//using api_gestao_despesas.DTO.Request;
//using api_gestao_despesas.DTO.Response;
//using api_gestao_despesas.Models;
//using api_gestao_despesas.Repository.Interface;
//using api_gestao_despesas.Service.Interface;
//using AutoMapper;

//namespace api_gestao_despesas.Service.Implementation
//{

//    public class PaymentService : IPaymentService
//    {
//        private readonly IPaymentRepository _repository;
//        private readonly IMapper _mapper;

//        public PaymentService(IPaymentRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<PaymentResponseDTO> Create(PaymentRequestDTO paymentRequestDTO)
//        {
//            var payment = _mapper.Map<Payment>(paymentRequestDTO);
//            var savedPayment = await _repository.Create(payment);
//            return _mapper.Map<PaymentResponseDTO>(savedPayment);
//        }

//        public async Task<List<PaymentResponseDTO>> GetAll()
//        {
//            var expenses = await _repository.GetAll();
//            return new List<PaymentResponseDTO>((IEnumerable<PaymentResponseDTO>)expenses);
//        }

//        public async Task<PaymentResponseDTO> GetById(int id)
//        {
//            var payment = await _repository.GetById(id);
//            return _mapper.Map<PaymentResponseDTO>(payment);
//        }

//        public async Task<PaymentResponseDTO> Update(int id, PaymentRequestDTO paymentRequestDTO)
//        {
//            var payment = _mapper.Map<Payment>(paymentRequestDTO);
//            var updatePayment = await _repository.Update(payment);
//            return _mapper.Map<PaymentResponseDTO>(updatePayment);
//        }

//        public async Task<PaymentResponseDTO> Delete(int id)
//        {
//            var deletePayment = await _repository.Delete(id);
//            return _mapper.Map<PaymentResponseDTO>(deletePayment);
//        }
//    }

//}
