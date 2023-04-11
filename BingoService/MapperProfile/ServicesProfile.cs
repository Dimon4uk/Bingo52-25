using AutoMapper;
using BingoDAL.EntityFramework.Entities;
using BingoCore.GameModels.Card;
using BingoCore.GameModels.Player;
using BingoModels.ViewModels;
using System.Collections.Concurrent;

namespace BingoServices.MapperProfile
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<BingoCard, Card>().ReverseMap();


            CreateMap<BingoPlayer, PlayerCard>()
                .ForMember(dst => dst.PlayerId,
                            src => src.MapFrom(src => src.Id))
                .ForMember(dst => dst.SelectedNumbers,
                                  src => src.MapFrom(src => src.SelectedNumbers.ToList()))
                .ForMember(dst => dst.NumberOfLines,
                            src => src.MapFrom(src => src.WinLineCount));
                                  

            CreateMap<PlayerCard, BingoPlayer>()
                .ForMember(dst => dst.Id, 
                            src => src.MapFrom(src => src.PlayerId))
                .ForMember(dst => dst.SelectedNumbers, 
                            src => src.MapFrom(src => new ConcurrentBag<int>(src.SelectedNumbers)))
                .ForMember(dst => dst.WinLineCount, 
                            src => src.MapFrom(src => src.NumberOfLines));
            

            CreateMap<Card, BingoGameViewModel>()
                .ForMember(dst => dst.SelectedLines,
                           src => src.MapFrom(src => src.PlayerCard.WinningLines))
                .ForMember(dst => dst.OrderedSelectedNumbers,
                           src => src.MapFrom(src => src.PlayerCard.SelectedNumbers.Order().ToList()))
                .ForMember(dst => dst.LinesCount,
                            src => src.MapFrom(src => src.PlayerCard.NumberOfLines))
                .ForMember(dst => dst.Card,
                           src => src.MapFrom((src, dst) =>
                           {
                               return new BingoCardViewModel
                               {
                                   SelectedNumbers = src.PlayerCard.SelectedNumbers,
                                   Size = src.Size,
                                   Id = src.Id,
                                   MaxValue = src.Size,
                                   CardGrid = src.CardGrid
                               };
                           }));
        }
    }
}
