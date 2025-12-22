namespace SpaceBattles.Core.Domain.Models;

using System.ComponentModel.DataAnnotations;
using SpaceBattles.Core.Domain.Enums;

public class UniverseCreationModel
{
    public UniverseCreationModel()
    {
        CommanderName = "Player";
        StartingPlanetName = "Earth";
        UniverseSpeed = 1f;
        UniverseSize = UniverseSize.Medium;
        IncludeBots = true;
    }

    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Your commander name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Your commander name must be at least 3 characters")]
    public string CommanderName { get; set; }

    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Your planet name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Your planet name must be at least 3 characters")]
    public string StartingPlanetName { get; set; }

    public float UniverseSpeed { get; set; }

    public bool IsPeacefulMode { get; set; }

    public bool IncludeBots { get; set; }

    public UniverseSize UniverseSize { get; set; }

    public int NumberOfBots
    {
        get
        {
            return !IncludeBots
                ? 0
                : UniverseSize switch
                {
                    UniverseSize.VerySmall => 1,
                    UniverseSize.Small => 3,
                    UniverseSize.Medium => 6,
                    UniverseSize.Large => 15,
                    UniverseSize.VeryLarge => 30,
                    _ => 1,
                };
        }
    }
}