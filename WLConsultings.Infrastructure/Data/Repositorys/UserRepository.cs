using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLConsultings.Domain.Core.Interfaces.Repositorys;
using WLConsultings.Domain.Entities;

namespace WLConsultings.Infrastructure.Data.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<ApplicationUser> GetByIdAsync(Guid id) =>
            await _context.Users.FindAsync(id);

        public async Task AddAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
