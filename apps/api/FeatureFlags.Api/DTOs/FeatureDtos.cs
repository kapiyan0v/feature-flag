namespace FeatureFlags.Api.DTOs;

public record CreateFeatureDto(
    string Name,
    string Key,
    string Description
);

public record UpdateFeatureDto(
    string Name,
    string Description
);

public record FeatureResponseDto(
    Guid Id,
    string Name,
    string Key,
    string Description,
    bool IsEnabled,
    DateTime CreatedAt
);