using System.Runtime.CompilerServices;
using Fina.Api.Data;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Fina.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Services;

public class CategoryService(AppDbContext context) : ICategoryService
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description
        };

        try
        {    
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(data: category, code: 201, message: "Categoria criada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(data: null, code: 500, message: "Não foi possível criar a Categoria!");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories
                                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if(category is null)
                return new Response<Category?>(data: null, code: 404, message: "Categoria não encontrada!");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(data: category, message: "Categoria atualizada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(data: null, code: 500, message: "Não foi possível alterar a Categoria!");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
       try
       {
            var category = await context.Categories
                                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

            if(category is null)
                return new Response<Category?>(data: null, code: 404, message: "Categoria não encontrada!");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(data: category, message: "Categoria excluída com sucesso!");
       }
       catch (Exception ex)
       {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(data: null, code: 500, message: "Não foi possível excluir a Categoria!");
       }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return category is null 
                    ? new Response<Category?>(data: null, code: 404, message: "Categoria não encontrada!")
                    : new Response<Category?>(data: category);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Response<Category?>(data: null, code: 500, message: "Não foi possível recuperar a categoria");
        }
    }
   
    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context.Categories
            .AsNoTracking()
            .Where(c => c.UserId == request.UserId)
            .OrderBy(c => c.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(
                categories,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new PagedResponse<List<Category>?>(data: null, code: 500, message: "Não foi possível consultar as Categorias!");
        }
    }
}