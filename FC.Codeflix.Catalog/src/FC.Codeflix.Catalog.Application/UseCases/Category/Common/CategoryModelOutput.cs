using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
namespace FC.Codeflix.Catalog.Application.UseCases.Category.Common;
public class CategoryModelOutut
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public CategoryModelOutut(
        Guid id,
        string name,
        string description,
        bool isActive,
        DateTime createdAt
    )
    {
        Id = id;
        Name = name;
        Description = description ?? "";
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public static CategoryModelOutut FromCategory(DomainEntity.Category category)
    => new(
          category.Id,
          category.Name,
          category.Description,
          category.IsActive,
          category.CreatedAt
    );
}
