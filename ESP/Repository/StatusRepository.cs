using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class StatusRepository : IRepository<Status>,IDisposable
    {
        ApplicationContext applicationContext = null!;

        public StatusRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }


        public int Add(Status element)
        {
            applicationContext.Statuses.Add(element);
            return applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            applicationContext.Statuses.Remove(GetById(id));
            return applicationContext.SaveChanges();
        }

        public IQueryable<Status> GetAll()
        {
            return applicationContext.Statuses;
        }

        public Status GetById(int id)
        {
            return applicationContext.Statuses.Find(id) ?? new Status();
        }

        public int Update(Status element)
        {
            applicationContext.Statuses.Update(element);
            return applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext?.Dispose();
        }
    }
}
