using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace MoviesMafia.Models.GenericRepo
{
    public class GenericRecordsDB<T> : IGenericRecordsDB<T> where T : class
    {
        private readonly RecordsDBContext _context;


        public GenericRecordsDB(RecordsDBContext context)
        {
            _context = context;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public List<Records> GetRecordsByUserId(string userId)
        {
            List<Records> listRecords= _context.Set<Records>().Where(x => x.UserId == userId).Select(e => new Records{Id=e.Id, Name= e.Name, Year=e.Year,Type = e.Type }).ToList();
            return listRecords;
        }
    }
}
