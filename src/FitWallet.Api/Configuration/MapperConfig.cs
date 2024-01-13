using AutoMapper;
using FitWallet.Api.Extensions;
using FitWallet.Core.Dtos;
using FitWallet.Core.Dtos.Users;
using FitWallet.Core.Models;

namespace FitWallet.Api.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Wallet, WalletDto>();
        CreateMap<WalletDto, Wallet>()
            .Ignore(e => e.Disabled);

        CreateMap<RegisterRequestDto, User>();
    }
}