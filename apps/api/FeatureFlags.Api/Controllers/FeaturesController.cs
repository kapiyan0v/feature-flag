using FeatureFlags.Api.Data;
using FeatureFlags.Api.DTOs;
using FeatureFlags.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlags.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeaturesController : ControllerBase
{
    private readonly AppDbContext _context;

    public FeaturesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeatureResponseDto>>> GetAll()
    {
        var features = await _context.Features
            .Select(f => new FeatureResponseDto(
                f.Id, f.Name, f.Key, f.Description, f.IsEnabled, f.CreatedAt
            ))
            .ToListAsync();

        return Ok(features);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FeatureResponseDto>> GetById(Guid id)
    {
        var feature = await _context.Features.FindAsync(id);
        if (feature is null) return NotFound();

        return Ok(new FeatureResponseDto(
            feature.Id, feature.Name, feature.Key, 
            feature.Description, feature.IsEnabled, feature.CreatedAt
        ));
    }

    [HttpPost]
    public async Task<ActionResult<FeatureResponseDto>> Create(CreateFeatureDto dto)
    {
        var feature = new Feature
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Key = dto.Key,
            Description = dto.Description,
            IsEnabled = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Features.Add(feature);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = feature.Id },
            new FeatureResponseDto(
                feature.Id, feature.Name, feature.Key,
                feature.Description, feature.IsEnabled, feature.CreatedAt
            ));
    }

    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> Toggle(Guid id)
    {
        var feature = await _context.Features.FindAsync(id);
        if (feature is null) return NotFound();

        feature.IsEnabled = !feature.IsEnabled;
        feature.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var feature = await _context.Features.FindAsync(id);
        if (feature is null) return NotFound();

        _context.Features.Remove(feature);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}