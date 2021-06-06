using TerminalsService.Abstraction;
using TerminalsService.Models;
using TerminalsService.Models.DBModels;
using TerminalsService.Models.ResponseModel;
using TerminalsService.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using RoutesSecurity;

namespace TerminalsService.Repository
{
    public class TerminalsRepository : ITerminalsRepository
    {
        private readonly TerminalsServiceContext _context;
        private readonly AppSettings _appSettings;
        private readonly Dependencies _dependencies;

        public TerminalsRepository(IOptions<AppSettings> appSettings, IOptions<Dependencies> dependencies, TerminalsServiceContext context)
        {
            _appSettings = appSettings.Value;
            _dependencies = dependencies.Value;
            _context = context;
        }

        public Terminals DeleteTerminals(string terminalId)
        {
            if (string.IsNullOrEmpty(terminalId))
                throw new ArgumentNullException(CommonMessage.InvalidData);

            Terminals terminal = _context.Terminals.Where(r => r.TerminalId == Obfuscation.Decode(terminalId)).FirstOrDefault();
            if (terminal == null)
                throw new KeyNotFoundException(CommonMessage.TerminalNotFound);

            return terminal;
        }

        public dynamic GetTerminals(string terminalId, Pagination pageInfo)
        {
            List<Terminals> terminals = new List<Terminals>();
            int recordsCount = 1;

            if (!string.IsNullOrEmpty(terminalId))
                terminals = _context.Terminals.Where(i => i.TerminalId == Obfuscation.Decode(terminalId)).ToList();
            else
            {
                terminals = _context.Terminals.Skip((pageInfo.offset - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();
                recordsCount = _context.Terminals.Count();
            }

            List<TerminalsDto> terminalsDtoList = terminals.Select(t => new TerminalsDto
            {
                TerminalId = Obfuscation.Encode(t.TerminalId),
                DeviceId = Obfuscation.Encode(t.DeviceId),
                NotificationIdentifier = t.NotificationIdentifier,
                CreatedAt = t.CreatedAt
            }).ToList();

            return new GetResponse
            {
                data = terminalsDtoList,
                pagination = new Pagination
                {
                    offset = pageInfo.offset,
                    limit = pageInfo.limit,
                    total = recordsCount
                },
            };
        }

        public Terminals PostTerminals(TerminalsDto terminalsDto)
        {
            if (terminalsDto == null)
                throw new ArgumentNullException(CommonMessage.InvalidData);

            Terminals terminal = new Terminals()
            {
                NotificationIdentifier = terminalsDto.NotificationIdentifier,
                DeviceId = Obfuscation.Decode(terminalsDto.DeviceId),
                CreatedAt = DateTime.Now
            };
            return terminal;
        }

        public Terminals UpdateTerminals(string terminalId, TerminalsDto terminalsDto)
        {
            if (terminalsDto == null || string.IsNullOrEmpty(terminalId))
                throw new ArgumentNullException(CommonMessage.InvalidData);

            Terminals terminal = _context.Terminals.Where(t => t.TerminalId == Obfuscation.Decode(terminalId)).FirstOrDefault();
            if (terminal == null)
                throw new KeyNotFoundException(CommonMessage.TerminalNotFound);

            terminal.DeviceId = Obfuscation.Decode(terminalsDto.DeviceId);
            terminal.NotificationIdentifier = terminalsDto.NotificationIdentifier;
            return terminal;
        }
    }
}
