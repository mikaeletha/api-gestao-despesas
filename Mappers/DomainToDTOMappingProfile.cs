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
            CreateMap<PaymentRequestDTO, Payment>().ReverseMap();
            CreateMap<ExpenseRequestDTO, Expense>().ReverseMap();
            CreateMap<GroupsRequestDTO, Group>().ReverseMap();
            CreateMap<Payment, PaymentResponseDTO>().ReverseMap();
            CreateMap<Expense, ExpenseResponseDTO>().ReverseMap();
            CreateMap<Group, GroupsResponseDTO>().ReverseMap();
            CreateMap<UserRequestDTO, User>().ReverseMap();
            CreateMap<User, UserResponseDTO>().ReverseMap();

        }
    }
}
