using CatanLoggerData.Context;
using CatanLoggerData.Entities;
using CatanLoggerModels;
using CatanLoggerStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatanLoggerStore.Repositories;

public class GameRepository : IGameRepository
{
    private readonly CatanLoggerDbContext _context;

    public GameRepository(CatanLoggerDbContext context)
    {
        _context = context;
    }

    public IEnumerable<GameModel> GetGames(int userId)
    {
        //IEnumerable<GameModel> query = (from g in _context.CL_GAME
        //                                join p in _context.CL_PLAYER on g.GAME_ID equals p.GAME_ID into ps
        //                                from p in ps.DefaultIfEmpty()
        //                                join d in _context.CL_DICEROLL on g.GAME_ID equals d.GAME_ID into ds
        //                                from d in ds.DefaultIfEmpty()
        //                                where g.USER_ID == userId
        //                                group new { p, d } by g into grp
        //                                select new GameModel
        //                                {
        //                                    GameId = grp.Key.GAME_ID,
        //                                    Date = grp.Key.DATE_PLAYED,
        //                                    Players = grp.Distinct().Select(m => m.p).Select(m => new PlayerModel
        //                                    {
        //                                        PlayerId = m.PLAYER_ID,
        //                                        Name = m.PLAYER_NAME,
        //                                        Color = m.PLAYER_COLOR,
        //                                        TurnOrder = m.TURN_ORDER,
        //                                        Winner = m.WINNER,
        //                                    }),
        //                                    DiceRolls = grp.Distinct().Select(m => m.d).Select(m => new DiceRollModel
        //                                    {
        //                                        DiceRollId = m.DICE_ROLL_ID,
        //                                        DiceNumber = m.DICE_NUMBER,
        //                                        DiceRolls = m.DICE_ROLLS
        //                                    }),
        //                                    TotalDiceRolls = grp.Select(m => m.d).Sum(m => m.DICE_ROLLS)
        //                                });

        IEnumerable<GameModel> query = _context.CL_GAME
            .Include(m => m.PLAYERS)
            .Include(m => m.DICE_ROLLS)
            .Select(m => new GameModel
            {
                GameId = m.GAME_ID,
                Date = m.DATE_PLAYED,
                Players = m.PLAYERS.Select(p => new PlayerModel
                {
                    PlayerId = p.PLAYER_ID,
                    Name = p.PLAYER_NAME,
                    Color = p.PLAYER_COLOR,
                    TurnOrder = p.TURN_ORDER,
                    Winner = p.WINNER,
                }),
                DiceRolls = m.DICE_ROLLS.Select(d => new DiceRollModel
                {
                    DiceRollId = d.DICE_ROLL_ID,
                    DiceNumber = d.DICE_NUMBER,
                    DiceRolls = d.DICE_ROLLS
                }),
                TotalDiceRolls = m.DICE_ROLLS.Sum(d => d.DICE_ROLLS)
            });

        return query;
    }

    public GameModel? SaveGame(int userId, GameModel model)
    {
        CL_GAME? gameEntity;
        if (model.GameId == -1)
        {
            gameEntity = new CL_GAME()
            {
                USER_ID = userId,
            };
            _context.CL_GAME.Add(gameEntity);
            _context.SaveChanges();
        }
        else
        {
            gameEntity = _context.CL_GAME.Where(m => m.GAME_ID == model.GameId).FirstOrDefault();
        }

        if(userId != gameEntity.USER_ID)
        {
            return null;
        }

        gameEntity.DATE_PLAYED = model.Date;

        int[] deletedPlayerIds = model.Players.Where(m => m.DeletedRecord).Select(m => m.PlayerId).ToArray();

        IEnumerable<CL_PLAYER> deletedPlayers = _context.CL_PLAYER.Where(m => deletedPlayerIds.Contains(m.PLAYER_ID));

        _context.CL_PLAYER.RemoveRange(deletedPlayers);

        IEnumerable<CL_PLAYER> newPlayers = model.Players.Where(m => m.PlayerId <= -1).Select(m => new CL_PLAYER
        {
            GAME_ID = gameEntity.GAME_ID,
            PLAYER_COLOR = m.Color,
            PLAYER_NAME = m.Name,
            TURN_ORDER = m.TurnOrder,
            WINNER = m.Winner
        });

        _context.CL_PLAYER.AddRange(newPlayers);

        foreach(PlayerModel player in model.Players.Where(m => m.PlayerId != -1))
        {
            CL_PLAYER? playerEnity = _context.CL_PLAYER.FirstOrDefault(m => m.PLAYER_ID == player.PlayerId);

            if(playerEnity != null)
            {
                playerEnity.PLAYER_COLOR = player.Color;
                playerEnity.PLAYER_NAME = player.Name;
                playerEnity.TURN_ORDER = player.TurnOrder;
                playerEnity.WINNER = player.Winner;
            }
        }

        int[] deletedDiceRollIds = model.DiceRolls.Where(m => m.DeletedRecord).Select(m => m.DiceRollId).ToArray();

        IEnumerable<CL_DICEROLL> deletedDiceRolls = _context.CL_DICEROLL.Where(m => deletedDiceRollIds.Contains(m.DICE_ROLL_ID));

        _context.CL_DICEROLL.RemoveRange(deletedDiceRolls);

        IEnumerable<CL_DICEROLL> newDiceRolls = model.DiceRolls.Where(m => m.DiceRollId <= -1).Select(m => new CL_DICEROLL
        {
            GAME_ID = gameEntity.GAME_ID,
            DICE_NUMBER = m.DiceNumber,
            DICE_ROLLS = m.DiceRolls,
        });

        _context.CL_DICEROLL.AddRange(newDiceRolls);

        foreach (DiceRollModel diceRoll in model.DiceRolls.Where(m => m.DiceRollId != -1))
        {
            CL_DICEROLL? diceRollEntity = _context.CL_DICEROLL.FirstOrDefault(m => m.DICE_ROLL_ID == diceRoll.DiceRollId);

            if (diceRollEntity != null)
            {
                diceRollEntity.DICE_NUMBER = diceRoll.DiceNumber;
                diceRollEntity.DICE_ROLLS = diceRoll.DiceRolls;
            }
        }

        _context.SaveChanges();

        return GetGames(userId).First(m => m.GameId == gameEntity.GAME_ID);
    }
}
