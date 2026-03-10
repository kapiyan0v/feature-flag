using FeatureFlags.Api.Data;
using FeatureFlags.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlags.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EnvironmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EnvironmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnvironmentResponseDto>>> GetAll()
    {
        var envs = await _context.Environments
            .Select(e => new EnvironmentResponseDto(
                e.Id, e.Name, e.Slug, e.CreatedAt
            ))
            .ToListAsync();

        return Ok(envs);
    }

    [HttpPost]
    public async Task<ActionResult<EnvironmentResponseDto>> Create(CreateEnvironmentDto dto)
    {
        var env = new Entities.Environment
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Slug = dto.Slug,
            CreatedAt = DateTime.UtcNow
        };

        _context.Environments.Add(env);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = env.Id },
            new EnvironmentResponseDto(env.Id, env.Name, env.Slug, env.CreatedAt));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var env = await _context.Environments.FindAsync(id);
        if (env is null) return NotFound();

        _context.Environments.Remove(env);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}