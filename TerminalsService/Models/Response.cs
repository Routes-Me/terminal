using Newtonsoft.Json.Linq;
using TerminalsService.Models.ResponseModel;
using System.Collections.Generic;

namespace TerminalsService.Models
{
    public class ErrorResponse
    {
        public string error { get; set; }
    }
    public class SuccessResponse
    {
        public string message { get; set; }
    }
    public class GetResponse
    {
        public Pagination pagination { get; set; }
        public List<TerminalsDto> data { get; set; }
    }
}
