using StudentInformationSystem.Models;

namespace StudentInformationSystem.Repository
{
    public interface IRoleRepository
    {
        ICollection<ApplicationRole> GetRoles();
    }
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<ApplicationRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
