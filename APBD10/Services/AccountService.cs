﻿using APBD10.Contexts;
using APBD10.DTOs;
using APBD10.Exceptions;
using APBD10.Models;
using APBD10.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Services;

public interface IAccountService
{
    Task<GetAccountResponseModel> GetAccountById(int id);
}

public class AccountService(DatabaseContext context) : IAccountService
{
    public async Task<GetAccountResponseModel> GetAccountById(int id)
    {
        var response = await context.Accounts
            .Where(e => e.AccountId == id)
            .Select(e => new GetAccountResponseModel()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Role = e.Role.Name,
                ShoppingCarts = e.ShoppingCarts.Select(s => new SingleShoppingCartItem(
                    s.ProductId,
                    s.Product.Name,
                    s.Amount
                    )).ToList()
            }).FirstOrDefaultAsync();

        if (response is null)
        {
            throw new AccountNotFoundException($"Account with id {id} does not exist");
        }

        return response;
    }
}