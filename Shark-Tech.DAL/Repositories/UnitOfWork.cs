using Microsoft.AspNetCore.Identity;
using Shark_Tech.DAL.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; }

    public IProductRepository ProductRepository { get; }

    public IProductImageRepository ProductImageRepository { get; }

    public ICustomerCartRepository CustomerCartRepository { get; }

    public IAuth Auth { get; } 

    private readonly AppDbContext _context;
    private readonly IConnectionMultiplexer _redis;

    private readonly UserManager<AppUser> _userManager;

    public UnitOfWork(AppDbContext context , IConnectionMultiplexer redis,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _redis = redis;
        _userManager = userManager;
        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context);
        ProductImageRepository = new ProductImageRepository(_context);
        CustomerCartRepository = new CustomerCartRepository(_redis);
        Auth = new AuthRepository( _userManager);
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();


}
