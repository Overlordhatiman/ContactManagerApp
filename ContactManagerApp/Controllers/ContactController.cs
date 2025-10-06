using AutoMapper;
using ContactManager.Core.DTOs;
using ContactManager.Core.Interfaces;
using ContactManagerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ContactManagerApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IFileParserService _fileParser;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IFileParserService fileParser, IMapper mapper)
        {
            _contactService = contactService;
            _fileParser = fileParser;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllAsync();
            var model = _mapper.Map<IEnumerable<ContactViewModel>>(contacts);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction(nameof(Index));

            var tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            try
            {
                using (var writeStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(writeStream);
                }

                var records = await _fileParser.ParseCsvAsync(tempFilePath);
                foreach (var record in records)
                    await _contactService.AddAsync(record);

                return Redirect("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Import failed: {ex.Message}");
            }
            finally
            {
                if (System.IO.File.Exists(tempFilePath))
                    System.IO.File.Delete(tempFilePath);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ContactViewModel model)
        {
            if (model == null)
            {
                return BadRequest(new { error = "Model is null" });
            }

            try
            {
                var dto = _mapper.Map<ContactDto>(model);
                await _contactService.UpdateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return Redirect("Index");
        }
    }
}
