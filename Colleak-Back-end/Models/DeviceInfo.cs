namespace Colleak_Back_end.Models
{
    public class DeviceInfo
    {
        public string Id { get; set; }
        public string Mac { get; set; }
        public string Description { get; set; }
        public string Ip { get; set; }
        public string Ip6 { get; set; }
        public string Ip6Local { get; set; }
        public string User { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
        public string Manufacturer { get; set; }
        public string Os { get; set; }
        public string DeviceTypePrediction { get; set; }
        public string RecentDeviceSerial { get; set; }
        public string RecentDeviceName { get; set; }
        public string RecentDeviceMac { get; set; }
        public string RecentDeviceConnection { get; set; }
        public string Ssid { get; set; }
        public string Vlan { get; set; }
        public string Switchport { get; set; }
        public UsageInfo Usage { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string GroupPolicy8021x { get; set; }
        public string AdaptivePolicyGroup { get; set; }
        public bool SmInstalled { get; set; }
        public string NamedVlan { get; set; }
        public string PskGroup { get; set; }        
    }

    public class UsageInfo
    {
        public int Sent { get; set; }
        public int Recv { get; set; }
        public int Total { get; set; }
    }
}
