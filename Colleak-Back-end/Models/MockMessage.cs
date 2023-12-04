namespace Colleak_Back_end.Models
{
    public class MockMessage
    {
        public string? sender_name { get; set; }
        public string? receiver_name { get; set; }
        public string sender_id { get; set; }
        public string receiver_id { get; set; }
        public string? message { get; set; }
        public int? request_time { get; set; }
        public string? disturb { get; set; }
        public string? is_on_location { get; set; }

        public MockMessage(string? sender_Name, string? receiver_Name, string sender_Id, string receiver_Id, string? Message, int? request_Time, string? Disturb, string? is_on_Location)
        {
            sender_name = sender_Name;
            receiver_name = receiver_Name;
            sender_id = sender_Id;
            receiver_id = receiver_Id;
            message = Message;
            request_time = request_Time;
            disturb = Disturb;
            this.is_on_location = is_on_Location;
        }
    }
}
