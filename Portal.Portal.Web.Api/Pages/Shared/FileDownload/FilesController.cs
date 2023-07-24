using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.Shared.FileDownload.Models;

namespace Portal.Portal.Web.Api.Pages.Shared.FileDownload
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class FilesController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Download([FromQuery] DownloadRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name is required.");
            }

            if (string.IsNullOrEmpty(request.Path))
            {
                return BadRequest("Path is required.");
            }

            if (!System.IO.File.Exists(request.Path))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(request.Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", request.Name);
        }
    }
}
