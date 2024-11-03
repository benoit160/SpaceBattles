using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceBattles.Server.Infrastructure;

namespace SpaceBattles.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public sealed class TelemetryController : ControllerBase
{
    private readonly SpaceBattlesDbContext _dbContext;
    
    public TelemetryController(SpaceBattlesDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
    }

    [HttpPost]
    public async Task<ActionResult> PostTelemetry(CancellationToken cancellationToken)
    {
        List<DayStatistics> statisticsList = await _dbContext.Statistics
            .ToListAsync(cancellationToken);
        
        DayStatistics? stats = statisticsList.Find(s => s.Date == DateOnly.FromDateTime(DateTime.Now));

        if (stats is null)
        {
            stats = new DayStatistics();
            _dbContext.Statistics.Add(stats);
        }
        
        stats.Logins++;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return NoContent();
    }
}