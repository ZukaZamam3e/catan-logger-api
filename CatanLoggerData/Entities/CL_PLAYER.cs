using System.ComponentModel.DataAnnotations.Schema;

namespace CatanLoggerData.Entities;

public class CL_PLAYER
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PLAYER_ID { get; set; }

    public int GAME_ID { get; set; }

    public string PLAYER_NAME { get; set; }

    public string PLAYER_COLOR { get; set; }

    public int TURN_ORDER { get; set; }

    public bool WINNER { get; set; }

    public CL_GAME GAME { get; set; }
}
