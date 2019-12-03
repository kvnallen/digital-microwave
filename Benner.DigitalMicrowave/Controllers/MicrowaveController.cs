using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core.Commands;
using Benner.DigitalMicrowave.Core.Notifications;
using Benner.DigitalMicrowave.Core.Services;
using Benner.DigitalMicrowave.Models;
using Microsoft.AspNetCore.Mvc;

namespace Benner.DigitalMicrowave.Controllers
{


    public class MicrowaveController : Controller
    {
        private readonly MicrowaveService _service;
        private readonly ProgramService _programService;

        public MicrowaveController(MicrowaveService service, ProgramService programService)
        {
            _service = service;
            _programService = programService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new MicrowaveViewModel(_programService.GetAll());
            return View(viewModel);
        }

        [HttpPost("microwave/start")]
        public IActionResult Start([FromBody] MicrowaveViewModel viewModel)
        {
            try
            {
                var command = GetCommand(viewModel);
                _service.Warm(command);
            }
            catch (Exception e)
            {
                return BadRequest(new List<Notification> { new Notification(e.Message, e.Message) });
            }

            return NoContent();
        }

        [HttpPost("microwave/fast-start")]
        public IActionResult FastStart([FromBody] MicrowaveViewModel viewModel)
        {
            try
            {
                var command = GetCommand(viewModel);
                _service.FastStart(command);
            }
            catch (Exception e)
            {
                return BadRequest(new List<Notification> { new Notification(e.Message, e.Message) });
            }

            return NoContent();
        }

        private WarmCommand GetCommand(MicrowaveViewModel viewModel)
        {
            return new WarmCommand
            {
                Text = viewModel.Text,
                Power = viewModel.Power,
                Time = viewModel.Time,
                ProgramName = viewModel.ProgramName,
                CurrentTime = viewModel.CurrentTime
            };
        }

        [HttpPost("microwave/cancel")]
        public async Task<IActionResult> Cancel()
        {
            try
            {
                _service.Cancel();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new List<Notification> { new Notification(e.Message, e.Message) });
            }
        }


        [HttpPost("microwave/pause")]
        public IActionResult Stop()
        {
            _service.Pause();
            return NoContent();
        }
    }
}