using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Entity;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Category(string name, string description, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        Validate();
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        Validate();
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }
    
    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException("Name should not be empty or null");
        if (Name.Length < 3)
            throw new EntityValidationException("Name should be at least 3 characters long");
        if (Name.Length > 255)
            throw new EntityValidationException("Name should be or less or equal 255 characters long");
        if (Description == null)
            throw new EntityValidationException("Description should not be empty or null");
        if (Description.Length > 10_000)
            throw new EntityValidationException("Description should be less or equal 10_000 characters long");
    }
}