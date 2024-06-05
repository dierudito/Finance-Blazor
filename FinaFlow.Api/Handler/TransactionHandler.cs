using FinaFlow.Api.Data;
using FinaFlow.Core.Common;
using FinaFlow.Core.Enums;
using FinaFlow.Core.Handler;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinaFlow.Api.Handler;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
            request.Amount *= -1;

        var transaction = new Transaction
        {
            Amount = request.Amount,
            Type = request.Type,
            CategoryId = request.CategoryId,
            PaidOrReceivedAt = request.PaidOrReceivedAt,
            Title = request.Title,
            UserId = request.UserId
        };

        try
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch (DbUpdateException)
        {
            return new Response<Transaction?>(null, 500, "Erro ao atualizar na base de dados");
        }
        catch (Exception)
        {
            return new Response<Transaction?>(null, 500, "Não foi possível criar uma categoria");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

        if (transaction is null) return new Response<Transaction?>(null, 400, "Transação não encontrada");

        try
        {
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch (DbUpdateException)
        {
            return new Response<Transaction?>(null, 500, "Erro ao atualizar na base de dados");
        }
        catch (Exception)
        {
            return new Response<Transaction?>(null, 500, "Não foi possível deletar a transação");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

            return transaction is null
                ? new(null, 400, "Transação não encontrada")
                : new(transaction);
        }
        catch (Exception)
        {
            return new(null, 500, "Não foi possível obter a transação");
        }
    }

    public async Task<PagedResponse<List<Transaction?>>> GetByPeriodAsync(GetByPeriodTransactionRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch
        {
            return new(null, 401, "Não foi possível determinar a data do período");
        }


        try
        {
            var query = context
                .Transactions
                .AsNoTracking()
                .Where(t => t.UserId == request.UserId &&
                            t.PaidOrReceivedAt >= request.StartDate &&
                            t.PaidOrReceivedAt <= request.EndDate)
                .OrderByDescending(t => t.PaidOrReceivedAt);


            var count = await query.CountAsync();
            var transactions = await query.Skip(request.Skip).Take(request.PageSize).ToListAsync();

            return transactions is null
                ? new(null, 400, "Transação não encontrada")
                : new(transactions, count, request.PageNumber, request.PageSize);
        }
        catch (Exception)
        {
            return new(null, 500, "Não foi possível obter as transações");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

        if (transaction is null) return new Response<Transaction?>(null, 400, "Transação não encontrada");


        if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
            request.Amount *= -1;


        transaction.Amount = request.Amount;
        transaction.Type = request.Type;
        transaction.CategoryId = request.CategoryId;
        transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
        transaction.Title = request.Title;

        try
        {
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch (DbUpdateException)
        {
            return new Response<Transaction?>(null, 500, "Erro ao atualizar na base de dados");
        }
        catch (Exception)
        {
            return new Response<Transaction?>(null, 500, "Não foi possível atualizar a transação");
        }
    }
}
