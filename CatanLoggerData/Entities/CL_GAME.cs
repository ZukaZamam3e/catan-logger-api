using System.ComponentModel.DataAnnotations.Schema;

namespace CatanLoggerData.Entities;

public class CL_GAME
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GAME_ID { get; set; }

    public int USER_ID { get; set; }

    public DateTime DATE_PLAYED { get; set; }

    public ICollection<CL_PLAYER> PLAYERS { get; set; }
    public ICollection<CL_DICEROLL> DICE_ROLLS { get; set; }

}
