namespace FeatureFlags.Api.DTOs;

public record CreateEnvironmentDto(
    string Name,
    string Slug
);

public record EnvironmentResponseDto(
    Guid Id,
    string Name,
    string Slug,
    DateTime CreatedAt
);