using TerminalsService.Models;
using TerminalsService.Models.ResponseModel;
using TerminalsService.Models.DBModels;
using System.Threading.Tasks;

namespace TerminalsService.Abstraction
{
    public interface ITerminalsRepository
    {
        Terminals PostTerminals(TerminalsDto terminalsDto);
        Terminals DeleteTerminals(string terminalId);
        dynamic GetTerminals(string terminalId, Pagination pageInfo);
        Terminals UpdateTerminals(string terminalId, TerminalsDto terminalsDto);
    }
}
