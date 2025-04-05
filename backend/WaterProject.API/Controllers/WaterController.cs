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


        [HttpGet("Recommendations")]
        public IActionResult GetRecomendation(string articleId)
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
        

        [HttpGet("GetProjectTypes")]
        public IActionResult GetProjectTypes ()
        {
            var projectTypes = _waterContext.Projects
                .Select(p => p.ProjectType)
                .Distinct()
                .ToList();

            return Ok(projectTypes);
        }

        [HttpPost("AddProject")]
        public IActionResult AddProject([FromBody] Project newProject)
        {
            _waterContext.Projects.Add(newProject);
            _waterContext.SaveChanges();
            return Ok(newProject);
        }

        [HttpPut("UpdateProject/{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] Project updatedProject)
        {
            var existingProject = _waterContext.Projects.Find(projectId);

            existingProject.ProjectName = updatedProject.ProjectName;
            existingProject.ProjectType = updatedProject.ProjectType;
            existingProject.ProjectRegionalProgram = updatedProject.ProjectRegionalProgram;
            existingProject.ProjectImpact = updatedProject.ProjectImpact;
            existingProject.ProjectPhase = updatedProject.ProjectPhase;
            existingProject.ProjectFunctionalityStatus = updatedProject.ProjectFunctionalityStatus;

            _waterContext.Projects.Update(existingProject);
            _waterContext.SaveChanges();

            return Ok(existingProject);
        }

        [HttpDelete("DeleteProject/{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            var project = _waterContext.Projects.Find(projectId);

            if (project == null)
            {
                return NotFound(new {message = "Project not found"});
            }

            _waterContext.Projects.Remove(project);
            _waterContext.SaveChanges();

            return NoContent();
        }

    }
}
