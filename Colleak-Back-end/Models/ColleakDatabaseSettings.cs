namespace Colleak_Back_end.Models
{
    public class ColleakDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EmployeeCollectionName { get; set; } = null!;
    }
}
