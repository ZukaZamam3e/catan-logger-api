namespace CatanLoggerModels;

public class PlayerModel
{
    public int PlayerId { get; set; }

    public string Color { get; set; }

    public string Name { get; set; }

    public int TurnOrder { get; set; }

    public bool Winner { get; set; }

    public bool DeletedRecord { get; set; }
}
