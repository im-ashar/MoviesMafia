using MoviesMafia.Models.GenericRepo;

namespace MoviesMafia.Models.Repo
{
    public interface IRecordsRepo : IGenericRepo<Records>
    {
        List<Records> GetRecordsByUserId(string userId);
    }
}
