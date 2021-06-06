using System;

namespace TerminalsService.Models.ResponseModel
{
    public class TerminalsDto
    {
        public string TerminalId { get; set; }
        public string NotificationIdentifier { get; set; }
        public string DeviceId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
