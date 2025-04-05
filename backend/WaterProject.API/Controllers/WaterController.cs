using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaterProject.API.Data;
using System.IO;
using System.Linq;

namespace WaterProject.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WaterController : ControllerBase
    {
        private WaterDbContext _waterContext;

        private readonly IWebHostEnvironment _env;


        public WaterController(WaterDbContext temp, IWebHostEnvironment env)
        {
            _waterContext = temp;
            _env = env;
        }


        [HttpGet("ContentRecommendations")]
        public IActionResult GetContentRecomendation(string articleId)
        {
            // Define the CSV file path. Adjust the path as needed.
            var csvPath = System.IO.Path.Combine(_env.ContentRootPath, "Data/content_recs.csv");
            Console.WriteLine("Looking for CSV file at: " + csvPath);
            if (!System.IO.File.Exists(csvPath))
            {
                return NotFound(new { message = "CSV file not found." });
            }

            var lines = System.IO.File.ReadAllLines(csvPath);
            bool isHeader = true;
            foreach (var line in lines)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var parts = line.Split(',');
                if (parts.Length > 0 && string.Equals(parts[0].Trim(), articleId.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    var recommendations = parts.Skip(0)
                                               .Select(r => r.Trim())
                                               .Where(r => !string.IsNullOrEmpty(r))
                                               .ToList();

                    return Ok(recommendations);
                }
            }

            return NotFound(new { message = $"No recommendations found for articleId: {articleId}" });
        }

        [HttpGet("ContextRecommendations")]
        public IActionResult GetContextRecomendation(string articleId)
        {
            // Define the CSV file path. Adjust the path as needed.
            var csvPath = System.IO.Path.Combine(_env.ContentRootPath, "Data/recommendations.csv");
            Console.WriteLine("Looking for CSV file at: " + csvPath);
            if (!System.IO.File.Exists(csvPath))
            {
                return NotFound(new { message = "CSV file not found." });
            }

            var lines = System.IO.File.ReadAllLines(csvPath);
            bool isHeader = true;
            foreach (var line in lines)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var parts = line.Split(',');
                if (parts.Length > 0 && string.Equals(parts[0].Trim(), articleId.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    var recommendations = parts.Skip(0)
                                            .Select(r => r.Trim())
                                            .Where(r => !string.IsNullOrEmpty(r))
                                            .ToList();

                    return Ok(recommendations);
                }
            }

            return NotFound(new { message = $"No recommendations found for articleId: {articleId}" });
        }
        

        [HttpGet("GetArticlesNames")]
        public IActionResult GetArticlesNames ()
        {
            // Define the CSV file path. Adjust the path as needed.
            var csvPath = System.IO.Path.Combine(_env.ContentRootPath, "Data/recommendations.csv");
            Console.WriteLine("Looking for CSV file at: " + csvPath);
            if (!System.IO.File.Exists(csvPath))
            {
                return NotFound(new { message = "CSV file not found." });
            }

            var lines = System.IO.File.ReadAllLines(csvPath);
            bool isHeader = true;
            var projectNames = new List<string>();
            
            foreach (var line in lines)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var parts = line.Split(',');
                if (parts.Length > 0 && !string.IsNullOrEmpty(parts[0].Trim()))
                {
                    projectNames.Add(parts[0].Trim());
                }
            }

            return Ok(projectNames);
        }
        

    }
}
