using FeatureFlags.Api.Data;
using FeatureFlags.Api.DTOs;
using FeatureFlags.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlags.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private readonly AppDbContext _context;

    public RulesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("feature/{featureId}")]
    public async Task<ActionResult<IEnumerable<RuleResponseDto>>> GetByFeature(Guid featureId)
    {
        var rules = await _context.Rules
            .Where(r => r.FeatureId == featureId)
            .OrderBy(r => r.Priority)
            .Select(r => new RuleResponseDto(
                r.Id, r.FeatureId, r.EnvironmentId,
                r.Type, r.Conditions, r.Value, r.Priority
            ))
            .ToListAsync();

        return Ok(rules);
    }

    [HttpPost]
    public async Task<ActionResult<RuleResponseDto>> Create(CreateRuleDto dto)
    {
        var rule = new Rule
        {
            Id = Guid.NewGuid(),
            FeatureId = dto.FeatureId,
            EnvironmentId = dto.EnvironmentId,
            Type = dto.Type,
            Conditions = dto.Conditions,
            Value = dto.Value,
            Priority = dto.Priority,
            CreatedAt = DateTime.UtcNow
        };

        _context.Rules.Add(rule);
        await _context.SaveChangesAsync();

        return Ok(new RuleResponseDto(
            rule.Id, rule.FeatureId, rule.EnvironmentId,
            rule.Type, rule.Conditions, rule.Value, rule.Priority
        ));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rule = await _context.Rules.FindAsync(id);
        if (rule is null) return NotFound();

        _context.Rules.Remove(rule);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}