﻿namespace FC.Codeflix.Catalog.Domain.Entity;
public class Genre
{
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    //Outsiders cannot change the internal state, the only way to add it is using AddCategory.
    //In other words, an internal change to the entity will only happen through its own method.
    public IReadOnlyList<Guid> Categories 
        => (IReadOnlyList<Guid>) _categories.AsReadOnly(); 

    private List<Guid> _categories;

    public Genre(string name, bool isActive = true)
    {
        Name = name;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        _categories = new List<Guid>();
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
    public void Update(string name)
    {
        Name = name;
        Validate();
    }
    public void AddCategory(Guid categoryId)
    {
        _categories.Add(categoryId);
        Validate();
    }
    public void RemoveCategory(Guid categoryId)
    {
        _categories.Remove(categoryId);
        Validate();
    }
    private void Validate()
        => DomainValidation.NotNullOrEmpty(Name, nameof(Name));

}
