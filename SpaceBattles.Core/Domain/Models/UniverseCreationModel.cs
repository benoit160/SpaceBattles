using System.ComponentModel.DataAnnotations;

namespace SpaceBattles.Core.Domain.Models;

public class UniverseCreationModel
{
    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Universe name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Universe name must be at least 3 characters")]
    public string UniverseName { get; set; }
    
    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Universe name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Universe name must be at least 3 characters")]
    public string CommanderName { get; set; }

    [Range(1, 10)]
    public float UniverseSpeed { get; set; }

    public bool IsPeacefulMode { get; set; }
    
    public bool IncludeBots { get; set; }

    [Range(1, 10)]
    public int NumberOfPlanets { get; set; }

    public UniverseCreationModel()
    {
        UniverseName = string.Empty;
        CommanderName = string.Empty;
        UniverseSpeed = 1;
        NumberOfPlanets = 1;
    }
}