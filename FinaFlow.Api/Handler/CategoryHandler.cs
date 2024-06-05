using FinaFlow.Api.Data;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Handler;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Description = request.Description,
            Title = request.Title,
            UserId = request.UserId
        };

        try
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category);
        }
        catch (DbUpdateException)
        {
            return new Response<Category?>(null, 500, "Erro ao atualizar na base de dados");
        }
        catch (Exception)
        {
            return new Response<Category?>(null, 500, "Não foi possível criar uma categoria");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if (category is null) return new Response<Category?>(null, 400, "Categoria não encontrada");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category);
        }
        catch (DbUpdateException)
        {
            return new Response<Category?>(null, 500, "Erro ao atualizar na base de dados");
        }
        catch (Exception)
        {
            return new Response<Category?>(null, 500, "Não foi possível excluir a categoria");
        }
    }

    public async Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoryRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .OrderBy(c => c.Title);


            var count = await query.CountAsync();
            var categories = await query.Skip(request.Skip).Take(request.PageSize).ToListAsync();

            return categories is null
                ? new(null, 400, "Categoria não encontrada")
                : new(categories, count, request.PageNumber, request.PageSize);
        }
        catch (Exception)
        {
            return new(null, 500, "Não foi possível obter as categorias");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            return category is null
                ? new(null, 400, "Categoria não encontrada")
                : new(category);
        }
        catch (Exception)
        {
            return new Response<Category?>(null, 500, "Não foi possível obter a categoria");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if (category is null) return new Response<Category?>(null, 400, "Categoria não encontrada");

            category.Description = request.Description;
            category.Title = request.Title;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category);
        }
        catch (DbUpdateException)
        {
            return new Response<Category?>(null, 500, "Erro ao atualizar na base de dados");
        }
        catch (Exception)
        {
            return new Response<Category?>(null, 500, "Não foi possível atualizar a categoria");
        }
    }
}
