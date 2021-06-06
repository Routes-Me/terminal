using System;

namespace TerminalsService.Models.DBModels
{
    public partial class Terminals
    {
        public int TerminalId { get; set; }
        public int DeviceId { get; set; }
        public string NotificationIdentifier { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
