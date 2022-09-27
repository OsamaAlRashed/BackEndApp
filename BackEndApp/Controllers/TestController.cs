using BackEndApp.Models;
using BackEndApp.Services.Interfaces;
using BackEndApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRepository<Test> repository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TestController(IRepository<Test> repository, IWebHostEnvironment webHostEnvironment)
        {
            this.repository = repository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await repository.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await repository.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Test test)
        {
            var result = await repository.Add(test);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Test test)
        {
            var result = await repository.Update(test);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await repository.Delete(id);
            return Ok(result);
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            var path = FileExtensions.TryUploadImage(file, "File", webHostEnvironment.WebRootPath);
            return Ok(path);
        }

    }
}
