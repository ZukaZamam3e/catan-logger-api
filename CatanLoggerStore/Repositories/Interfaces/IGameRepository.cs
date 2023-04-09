using CatanLoggerModels;
using System.Linq.Expressions;

namespace CatanLoggerStore.Repositories.Interfaces;
public interface IGameRepository : IRepository
{
    IEnumerable<GameModel> GetGames(int userId);

    GameModel SaveGame(int userId, GameModel model);
}
