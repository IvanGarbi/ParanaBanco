using AutoMapper;
using ParanaBancoCase.Business.Models;
using ParanaBancoCase.Main.ViewModels;

namespace ParanaBancoCase.Main.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Cliente, ClienteViewModel>().ReverseMap();
    }
}