using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myBooks.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logService;
        public LogsController(LogsService logsService)
        {
            _logService = logsService;
        }

        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAllLogsFromDB()
        {
            try
            {
                var allLogs = _logService.GetAllLogsFromDb();
                return Ok(allLogs);
            }
            catch (Exception)
            {
                return BadRequest("Could not load Logs from database");
            }
        }
    }
}
