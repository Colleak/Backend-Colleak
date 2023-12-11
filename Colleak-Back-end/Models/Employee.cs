using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colleak_Back_end.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? EmployeeName { get; set; } = null!;

        public bool? AllowLocationTracking { get; set; }

        public string? ConnectedRouterName { get; set; }

        public string? ConnectedDeviceMacAddress { get; set; }

        public bool? ConnectedToDevice { get; set; }
    }
}
