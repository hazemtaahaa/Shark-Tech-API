using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL;

internal class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Product>> GetAllWithCategoryAndImagesAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .ToListAsync();
    }
    public async Task<Product> GetByIdWithCategoryAndImagesAsync(Guid id)
    {
        return await _context.Products
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .Include(p => p.ProductImages).FirstOrDefaultAsync();
            
    }
}

