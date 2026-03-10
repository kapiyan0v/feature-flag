namespace FeatureFlags.Api.DTOs;

public record CreateRuleDto(
    Guid FeatureId,
    Guid EnvironmentId,
    string Type,
    string Conditions,
    bool Value,
    int Priority
);

public record RuleResponseDto(
    Guid Id,
    Guid FeatureId,
    Guid EnvironmentId,
    string Type,
    string Conditions,
    bool Value,
    int Priority
);