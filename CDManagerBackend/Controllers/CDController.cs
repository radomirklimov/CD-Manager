using CDManagerBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CDManagerBackend.Controllers
{
    [ApiController]
    [Route("cds")]
    public class CDController: ControllerBase
    {
        private readonly CDListService _service;

        public CDController(CDListService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CD cd)
        {
            _service.AddCD(cd);
            return CreatedAtAction(nameof(GetCurrent), cd);
        }

        [HttpDelete("current")]
        public IActionResult RemoveCurrent()
        {
            _service.RemoveCurrent();
            return NoContent();
        }

        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            var cd = _service.GetCurrent();
            if (cd == null) return NotFound();
            return Ok(cd);
        }

        [HttpGet("first")]
        public IActionResult GetFirst()
        {
            var firstCd = _service.GetFirst();
            if (firstCd == null) return NotFound();
            return Ok(firstCd);
        }

        [HttpGet("next")]
        public IActionResult GetNext()
        {
            var nextCd = _service.GetNext();
            if (nextCd == null) return NotFound();
            return Ok(nextCd);
        }

        [HttpGet("performer/{performer}")]
        public IActionResult FindByPerformer(string performer)
        {
            return Ok(_service.FindByPerformer(performer));
        }

        [HttpGet("title/{title}")]
        public IActionResult FindByTitle(string title)
        {
            return Ok(_service.FindByTitle(title));
        }

        [HttpPost("sort")]
        public IActionResult Sort()
        {
            _service.Sort();
            return Ok();
        }

        [HttpGet("export/json")]
        public IActionResult ExportJson()
        {
            string tempPath = Path.GetTempFileName();
            _service.ExportJson(tempPath);

            var bytes = System.IO.File.ReadAllBytes(tempPath);
            return File(bytes, "application/json", "cds.json");
        }

        [HttpGet("export/csv")]
        public IActionResult ExportCsv()
        {
            string tempPath = Path.GetTempFileName();
            _service.ExportCsv(tempPath);

            var bytes = System.IO.File.ReadAllBytes(tempPath);
            return File(bytes, "text/csv", "cds.csv");
        }

        [HttpGet("export/yaml")]
        public IActionResult ExportYaml()
        {
            string tempPath = Path.GetTempFileName();
            _service.ExportYaml(tempPath);

            var bytes = System.IO.File.ReadAllBytes(tempPath);
            return File(bytes, "application/x-yaml", "cds.yaml");
        }

        [HttpPost("import/json")]
        public async Task<IActionResult> ImportJson(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            string tempPath = Path.GetTempFileName();

            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _service.ImportJson(tempPath);

            return Ok("JSON imported successfully.");
        }

        [HttpPost("import/csv")]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            string tempPath = Path.GetTempFileName();

            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _service.ImportCsv(tempPath);

            return Ok("CSV imported successfully.");
        }

        [HttpPost("import/yaml")]
        public async Task<IActionResult> ImportYaml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            string tempPath = Path.GetTempFileName();

            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _service.ImportYaml(tempPath);

            return Ok("YAML imported successfully.");
        }
    }
}
