using CatanLoggerModels;
using CatanLoggerStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CatanLoggerApi.Controllers;

//[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameRepository _gameRepo;

    public GameController(ILogger<GameController> logger,
        IGameRepository gameRepo)
    {
        _logger = logger;
        _gameRepo = gameRepo;
    }

    [EnableCors("_myAllowSpecificOrigins")]
    [HttpGet("Get")]
    public IEnumerable<GameModel> Get()
    {
        return _gameRepo.GetGames(1000).ToArray();
    }

    [EnableCors("_myAllowSpecificOrigins")]
    [HttpPost("Save")]
    public GameModel SaveGame(GameModel model)
    {
        model = _gameRepo.SaveGame(1000, model);

        return model;
    }
}
