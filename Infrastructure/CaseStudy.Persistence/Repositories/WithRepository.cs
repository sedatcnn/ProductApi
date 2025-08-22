using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using CaseStudy.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.Persistence.Repositories
{
    public class WithRepository : IWithRepository
    {
        private readonly AppDbContext _context;

        public WithRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetUserAsync()
        {
            var value = _context.Users.ToList();
            return value;
        }


        public List<Product> GetProductAsync()
        {
            return _context.Products.ToList();
        }

     
    }
}
