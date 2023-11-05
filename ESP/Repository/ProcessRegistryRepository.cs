using ESP.Context;
using ESP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESP.Repository
{
    public class ProcessRegistryRepository : IRepository<ReferenceProcess>
    {
        private readonly ApplicationContext _context;

        public ProcessRegistryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(ReferenceProcess element)
        {
            _context.ReferenceProcesses.Add(element);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            _context.ReferenceProcesses.Remove(GetById(id));
            return _context.SaveChanges();
        }

        public IQueryable<ReferenceProcess> GetAll()
        {
            return _context.ReferenceProcesses;
        }

        public ReferenceProcess GetById(int id)
        {
            return _context.ReferenceProcesses.Include(p => p.SystemBlock)
                                              .Include(p => p.ProcessOneLevel)
                                              .Include(p => p.ProcessTwoLevel)
                                              .Include(p => p.Process)
                                              .FirstOrDefault(z => z.Id == id) ?? new ReferenceProcess();
        }

        public int Update(ReferenceProcess element)
        {
            _context.ReferenceProcesses.Update(element);
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
