using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
public class DeleteCategoryInput : IRequest<Unit>
{
    public Guid Id { get; set; }
    public DeleteCategoryInput(Guid id)
        => Id = id;
}
