using System;
using Benner.DigitalMicrowave.Core.Commands;
using Benner.DigitalMicrowave.Core.Services;
using Benner.DigitalMicrowave.Extensions;
using Benner.DigitalMicrowave.Models;
using Microsoft.AspNetCore.Mvc;

namespace Benner.DigitalMicrowave.Controllers
{
    [Controller]
    public class ProgramController : Controller
    {
        private readonly ProgramService _service;

        public ProgramController(ProgramService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var programs = _service.GetAll().ToViewModel();
            return View(programs);
        }

        [HttpGet("program/{name}")]
        public IActionResult GetByName(string name)
        {
            var program = _service.GetByName(name).ToViewModel();
            return Ok(program);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProgramViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var command = new CreateProgramCommand {
                    Name = viewModel.Name,
                    HeatingCharacter = viewModel.HeatingCharacter,
                    Instructions = viewModel.Instructions,
                    Power = viewModel.Power,
                    Time = viewModel.Time
                };

                _service.Add(command);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
