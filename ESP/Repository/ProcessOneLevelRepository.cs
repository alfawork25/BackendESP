using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class ProcessOneLevelRepository : IRepository<ProcessOneLevel>
    {
        private readonly ApplicationContext _context;

        public ProcessOneLevelRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(ProcessOneLevel element)
        {
            _context.ProcessOneLevels.Add(element);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
             _context.ProcessOneLevels.Remove(GetById(id));
            return _context.SaveChanges();
        }

        public IQueryable<ProcessOneLevel> GetAll()
        {
            return _context.ProcessOneLevels;
        }

        public ProcessOneLevel GetById(int id)
        {
            return _context.ProcessOneLevels.Find(id) ?? new ProcessOneLevel();
        }

        public int Update(ProcessOneLevel element)
        {
            _context.ProcessOneLevels.Update(element);
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
