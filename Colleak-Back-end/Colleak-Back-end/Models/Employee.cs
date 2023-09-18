using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colleak_Back_end.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string EmployeeName { get; set; } = null!;
    }
}
