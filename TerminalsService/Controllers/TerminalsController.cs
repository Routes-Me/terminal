using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TerminalsService.Abstraction;
using TerminalsService.Models;
using TerminalsService.Models.DBModels;
using TerminalsService.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerminalsService.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("v{version:apiVersion}/")]
    public class TerminalsController : ControllerBase
    {
        private readonly ITerminalsRepository _terminalsRepository;
        private readonly TerminalsServiceContext _context;
        public TerminalsController(ITerminalsRepository terminalsRepository, TerminalsServiceContext context)
        {
            _terminalsRepository = terminalsRepository;
            _context = context;
        }

        [HttpPost]
        [Route("terminals")]
        public async Task<IActionResult> PostTerminals(TerminalsDto terminalsDto)
        {
            try
            {
                Terminals terminal = _terminalsRepository.PostTerminals(terminalsDto);
                await _context.Terminals.AddAsync(terminal);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status201Created, new SuccessResponse{ message = CommonMessage.TerminalInserted });
        }

        [HttpDelete]
        [Route("terminals/{terminalId}")]
        public async Task<IActionResult> DeleteTerminals(string terminalId)
        {
            try
            {
                Terminals terminal = _terminalsRepository.DeleteTerminals(terminalId);
                _context.Terminals.Remove(terminal);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet]
        [Route("terminals/{terminalId?}")]
        public IActionResult GetTerminals(string terminalId, [FromQuery] Pagination pageInfo)
        {
            GetResponse response = new GetResponse();
            try
            {
                response = _terminalsRepository.GetTerminals(terminalId, pageInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut]
        [Route("terminals/{terminalId}")]
        public async Task<IActionResult> UpdateTerminals(string terminalId, TerminalsDto terminalsDto)
        {
            try
            {
                Terminals terminal = _terminalsRepository.UpdateTerminals(terminalId, terminalsDto);
                _context.Terminals.Update(terminal);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponse{ error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse{ error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse{ error = ex.Message });
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
