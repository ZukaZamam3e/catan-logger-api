namespace CatanLoggerModels;
public class GameModel
{
    public int GameId { get; set; }

    public DateTime Date { get; set; }

    public IEnumerable<PlayerModel> Players { get; set; }

    public int TotalDiceRolls { get; set; }

    public IEnumerable<DiceRollModel> DiceRolls { get; set; }
}
