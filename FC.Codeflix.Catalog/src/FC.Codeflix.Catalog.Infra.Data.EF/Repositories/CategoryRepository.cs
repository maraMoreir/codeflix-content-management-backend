﻿using FC.Codeflix.Catalog.Application.Excpetion;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FC.Codeflix.Catalog.Infra.Data.EF.Repositories;
public class CategoryRepository
    : ICategoryRepository
{
    private readonly CodeflixCatalogDbContext _context;
    private DbSet<Category> _categories 
        => _context.Set<Category>();

    public CategoryRepository(CodeflixCatalogDbContext context)
        => _context = context;

    public async Task Insert(
        Category aggregate, 
        CancellationToken cancellationToken)
            => await _categories.AddAsync(aggregate, cancellationToken);

    public async Task<Category> Get(
        Guid id, CancellationToken cancellationToken)
    {
        var catetory = await _categories.AsNoTracking().FirstOrDefaultAsync(
                    x => x.Id == id ,
                    cancellationToken
        );
        NotFoundException.ThrowIfNull(catetory, $"Categoty '{id}' not found.");
        return catetory!;
    }
    public Task Update(Category aggregate, CancellationToken cancellationToken)
        => Task.FromResult(_categories.Update(aggregate));

    public Task Delete(Category aggregate, CancellationToken cancellationToken)
        => Task.FromResult(_categories.Remove(aggregate));


    public async Task<SearchOutput<Category>> Search(
        SearchInput input, 
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page  - 1) * input.PerPage;
        var total = await _categories.CountAsync();
        var items = await _categories.AsNoTracking()
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }
}
