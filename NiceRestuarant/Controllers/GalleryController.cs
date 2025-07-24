using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NR.Core.DTOs;
using NR.Domain.Entities;
using NR.Domain.Interfaces;

namespace NiceRestuarant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryImageService _galleryService;

        public GalleryController(IGalleryImageService galleryService)
        {
            _galleryService = galleryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var images = await _galleryService.GetAllAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var image = await _galleryService.GetByIdAsync(id);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromForm] GalleryImageDto dto, IFormFile? file)
        {
            try
            {
                await _galleryService.AddAsync(dto, file);
                return CreatedAtAction(nameof(Get), new { id = 0 }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromForm] GalleryImageDto dto, IFormFile? file)
        {
            try
            {
                await _galleryService.UpdateAsync(id, dto, file);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _galleryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}