using System.ComponentModel.DataAnnotations;
using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Models;

public class UniverseCreationModel
{
    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Universe name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Universe name must be at least 3 characters")]
    public string UniverseName { get; set; }
    
    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Your commander name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Your commander name must be at least 3 characters")]
    public string CommanderName { get; set; }
    
    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Your planet name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Your planet name must be at least 3 characters")]
    public string StartingPlanetName { get; set; }

    [Range(1, 10)]
    public float UniverseSpeed { get; set; }

    public bool IsPeacefulMode { get; set; }
    
    public bool IncludeBots { get; set; }
    
    public UniverseSize UniverseSize { get; set; }

    public UniverseCreationModel()
    {
        UniverseName = string.Empty;
        CommanderName = string.Empty;
        UniverseSpeed = 1;
        UniverseSize = UniverseSize.VeryLarge;
    }
}