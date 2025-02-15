using MoviesMafia.Models.GenericRepo;

namespace MoviesMafia.Models.Repo
{
    public class RecordsRepo : GenericRepo<Records>, IRecordsRepo
    {
        private readonly AppDbContext _context;

        public RecordsRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Records> GetRecordsByUserId(string userId)
        {
            List<Records> listRecords = _context.Records.Where(x => x.UserId == userId).Select(e => new Records { Id = e.Id, Name = e.Name, Year = e.Year, Type = e.Type }).ToList();
            return listRecords;
        }
    }
}
