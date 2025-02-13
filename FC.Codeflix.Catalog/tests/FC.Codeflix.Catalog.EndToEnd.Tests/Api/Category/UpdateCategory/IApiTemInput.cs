namespace FC.Codeflix.Catalog.EndToEnd.Tests.Api.Category.UpdateCategory;

public interface IApiTemInput
{
    string? Description { get; set; }
    bool? IsActive { get; set; }
    string Name { get; set; }
}