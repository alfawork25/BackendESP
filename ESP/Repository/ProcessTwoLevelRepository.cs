using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class ProcessTwoLevelRepository : IRepository<ProcessTwoLevel>
    {
        private readonly ApplicationContext _context;

        public ProcessTwoLevelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(ProcessTwoLevel element)
        {
            _context.ProcessTwoLevels.Add(element);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            _context.ProcessTwoLevels.Remove(GetById(id));
            return _context.SaveChanges();
        }

        public IQueryable<ProcessTwoLevel> GetAll()
        {
            return _context.ProcessTwoLevels;
        }

        public ProcessTwoLevel GetById(int id)
        {
            return _context.ProcessTwoLevels.Find(id) ?? new ProcessTwoLevel();
        }

        public int Update(ProcessTwoLevel element)
        {
            _context.ProcessTwoLevels.Update(element);
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
