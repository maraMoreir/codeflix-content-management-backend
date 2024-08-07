using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

internal interface IGetCategory : 
    IRequestHandler<GetCategoryInput, CategoryModelOutut>
{ }
