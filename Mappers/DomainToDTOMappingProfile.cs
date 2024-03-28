using api_gestao_despesas.DTO.Request;
using api_gestao_despesas.DTO.Response;
using api_gestao_despesas.Models;
using AutoMapper;

namespace api_gestao_despesas.Mappers
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Payment, PaymentRequestDTO>().ReverseMap();
            CreateMap<Expense, ExpenseRequestDTO>().ReverseMap();
            CreateMap<Payment, PaymentResponseDTO>().ReverseMap();
            CreateMap<Expense, ExpenseResponseDTO>().ReverseMap();
        }
    }
}
