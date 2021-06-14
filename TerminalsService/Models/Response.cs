using TerminalsService.Models.ResponseModel;
using System.Collections.Generic;

namespace TerminalsService.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; }
    }
    public class SuccessResponse
    {
        public string Message { get; set; }
    }
    public class GetResponse
    {
        public Pagination Pagination { get; set; }
        public List<TerminalsDto> Data { get; set; }
    }
    public class PostTerminalResponse
    {
        public string TerminalId { get; set; }
    }
}
