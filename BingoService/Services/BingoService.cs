using AutoMapper;
using BingoDAL.EntityFramework;
using BingoDAL.EntityFramework.Entities;
using BingoCore.GameEngine.Interfaces;
using BingoCore.GameModels.Card;
using BingoCore.GameModels.Player;
using BingoCore.GameParameters;
using BingoCore.GameParameters.Enums;
using BingoCore.GameParameters.Interfaces;
using BingoModels.ViewModels;
using BingoServices.Exceptions;
using BingoServices.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using static BingoCore.GameParameters.Consts.GameConstants;

namespace BingoServices.Services
{
    public class BingoService : IBingoService
    {
        private readonly IGameEngine _gameEngine;
        private readonly ICardRepository _cardRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IGameParameters _gameParameters;
        public BingoService(
            IGameEngine gameEngine,
            ICardRepository cardRepository,
            IPlayerRepository playerRepository,
            IMemoryCache cache,
            IMapper mapper)
        {
            _gameEngine = gameEngine;
            _cardRepository = cardRepository;
            _playerRepository = playerRepository;
            _cache = cache;
            _mapper = mapper;
            _gameParameters = new BingoGameParameters
                    (
                        GameCodes.BINGO_52_25,
                        BaseBingo.Size,
                        BaseBingo.MinCardValue,
                        BaseBingo.MaxCardValue
                    );

          
        }

        public async Task<BingoGameViewModel> CreateBingoCard(PlayerViewModel playerInfo)
        {
            var entityPlayer = await CreateOrGetPlayer(playerInfo);

            var game = _gameEngine.CreatePlayerGame(_gameParameters);

            var card = _mapper.Map<Card>(game.card);
            card.PlayerCard = _mapper.Map<PlayerCard>(game.player);
            card.PlayerCard.PlayerId = entityPlayer.Id;

            var cardEntity = await _cardRepository.Add(card);
            await _cardRepository.SaveChangesAsync();
            var cardCache = _cache.Set(
                cardEntity.Id,
                cardEntity, TimeSpan.FromMinutes(5));
           
            return _mapper.Map<BingoGameViewModel>(cardCache);
            
        }

        public async Task<BingoGameViewModel> GetBingoCard(PlayerViewModel playerInfo, int cardId) 
        {
            var card = await GetCard(cardId);

            if (card.PlayerCard.PlayerId != playerInfo.Id) 
            {
                throw new EntityNotFoundException("Card was not found");
            }

            return _mapper.Map<BingoGameViewModel>(card);
        }
        public async Task<BingoGameViewModel> GetNext(int cardId)
        {
            var card = await GetCard(cardId);

            var player = _mapper.Map<BingoPlayer>(card.PlayerCard);
            var nextNumber = _gameEngine.GenerateNextNumber(player, _gameParameters);

            card.PlayerCard.SelectedNumbers.Add(nextNumber);

            await _cardRepository.Update(card);
            await _cardRepository.SaveChangesAsync();
            var cacheEntity = _cache.Set(card.Id, card);

            var bingoCard = _mapper.Map<BingoCard>(card);
            var bingoPlayer = _mapper.Map<BingoPlayer>(card.PlayerCard);
            if (card.PlayerCard.SelectedNumbers.Count >= card.Size)
            {
                var winResult = _gameEngine.CalculatePlayerWinLines(bingoCard, bingoPlayer);
                card.PlayerCard.WinningLines = winResult.winLineArray;
                card.PlayerCard.NumberOfLines = winResult.lines;
            }

            return _mapper.Map<BingoGameViewModel>(card);
        }

        private async Task<Card> GetCard(int cardId) 
        {
            if (!_cache.TryGetValue(cardId, out Card? card) || card == null)
            {
                card = await _cardRepository.Get(c => c.Id == cardId);
            }

            if (null == card)
            {
                throw new NullReferenceException("Card entity missed in cache or db. Check your memory usage.");
            }

            return card;
        }

        private async Task<Player> CreateOrGetPlayer(PlayerViewModel playerInfo) 
        {

            if(!_cache.TryGetValue(playerInfo.Email, out Player? entityPlayer))
                 entityPlayer = await _playerRepository.Get(p => p.Id == playerInfo.Id);

            if (null == entityPlayer)
            {
                entityPlayer = await _playerRepository.Add(new Player
                {
                    Id = playerInfo.Id,
                    Email = playerInfo.Email
                });
                await _playerRepository.SaveChangesAsync();
                _cache.Set(playerInfo.Email, entityPlayer, TimeSpan.FromMinutes(5));
            }

            return entityPlayer;
        }
    }
}