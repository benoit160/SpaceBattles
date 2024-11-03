using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceBattles.Server.Infrastructure;

namespace SpaceBattles.Server.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public sealed class SaveController : ControllerBase
{
    private readonly SpaceBattlesDbContext _dbContext;
    
    public SaveController(SpaceBattlesDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
    }

    [HttpGet]
    public async Task<ActionResult<SaveDataResponse>> GetSaveData([FromQuery] Guid saveId, CancellationToken cancellationToken)
    {
        SaveData? data = await _dbContext.SaveData
            .Where(data => data.Id == saveId)
            .FirstOrDefaultAsync(cancellationToken);

        if (data is null) return NotFound();

        return Ok(new SaveDataResponse(data.LastSave, data.Data));
    }

    [HttpPost]
    public async Task<ActionResult> PostSaveData([FromBody] PostDataRequest request, CancellationToken cancellationToken)
    {
        if (request.SaveId is null)
        {
            SaveData saveData = new SaveData(request.SaveData);
            _dbContext.SaveData.Add(saveData);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Ok(new PostDataResponse(saveData.Id));
        }

        SaveData? data = await _dbContext.SaveData.FirstOrDefaultAsync(data => data.Id == request.SaveId, cancellationToken);
        
        if (data is null) return NotFound();
        
        data.Update(request.SaveData);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Ok(new PostDataResponse(data.Id));
    }

    public record PostDataRequest(Guid? SaveId, string SaveData);
    
    public record PostDataResponse(Guid SaveId);
    
    public record SaveDataResponse(DateTime LastUpdate, string Base64Binary);
}