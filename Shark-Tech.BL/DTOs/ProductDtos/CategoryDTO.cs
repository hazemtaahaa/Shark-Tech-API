namespace Shark_Tech.BL;

public record CategoryDTO
(
   string Name,
    string Description
);

public record UpdateCategoryDTO
(
    Guid Id,
   string Name,
    string Description
);
