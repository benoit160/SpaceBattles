namespace SpaceBattles.Core.Entities;

public sealed class Universe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public SmallKey Id { get; set; }

    required public string Name { get; set; }

    public DateTime CreationDate { get; init; }
         = DateTime.Now;

    public List<Planet> Planets { get; }
        = new List<Planet>();

    public static Universe CreateUniverse(UniverseCreationModel model)
    {
        Universe newUniverse = new Universe
        {
            Name = model.UniverseName,
        };

        newUniverse.Planets.AddRange(Enumerable
            .Range(0, model.NumberOfPlanets)
            .Select(index => new Planet
            {
                Universe = newUniverse,
            }));

        return newUniverse;
    }
}

public class UniverseCreationModel
{
    [Required(ErrorMessage = "Please enter a name")]
    [StringLength(20, ErrorMessage = "Universe name can't be more than 20 characters")]
    [MinLength(3, ErrorMessage = "Universe name must be at least 3 characters")]
    public string UniverseName { get; set; }

    [Range(1, 10)]
    public int UniverseSpeed { get; set; }

    public bool IsPeacefullMode { get; set; }

    [Range(1,100)]
    public int NumberOfPlanets { get; set; }

    public UniverseCreationModel()
    {
        UniverseName = string.Empty;
        UniverseSpeed = 1;
        NumberOfPlanets = 1;
    }
}