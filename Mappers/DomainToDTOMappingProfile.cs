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
            //Payments
            CreateMap<PaymentRequestDTO, Payment>().ReverseMap();
            CreateMap<Payment, PaymentResponseDTO>().ReverseMap();

            //Expenses
            CreateMap<ExpenseRequestDTO, Expense>().ReverseMap();
            CreateMap<Expense, ExpenseResponseDTO>().ReverseMap();

            //Groups
            CreateMap<GroupsRequestDTO, Group>().ReverseMap();
            CreateMap<Group, GroupsResponseDTO>().ReverseMap();
            
            //Users
            CreateMap<UserRequestDTO, User>().ReverseMap();
            CreateMap<User, UserResponseDTO>().ReverseMap();

            //Friends
            CreateMap<FriendRequestDTO, Friend>().ReverseMap();
            CreateMap<Friend, FriendResponseDTO>().ReverseMap();
        }
    }
}
