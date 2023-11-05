using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class PatternRepository : IRepository<Pattern>, IDisposable
    {

        ApplicationContext _applicationContext = null!;

        public PatternRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public int Add(Pattern element)
        {
            _applicationContext.Patterns.Add(element);
            return _applicationContext.SaveChanges();

        }

        public int Delete(int id)
        {
            _applicationContext.Patterns.Remove(GetById(id));
            return _applicationContext.SaveChanges();
        }

        public IQueryable<Pattern> GetAll()
        {
            return _applicationContext.Patterns;
        }

        public Pattern GetById(int id)
        {
            return _applicationContext.Patterns.FirstOrDefault(x => x.Id == id) ?? new Pattern();
        }

        public int Update(Pattern element)
        {
            _applicationContext.Patterns.Update(element);
            return _applicationContext.SaveChanges();
        }

        public void Dispose()
        {
          _applicationContext?.Dispose();
        }
    }
}
