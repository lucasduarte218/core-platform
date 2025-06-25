using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CorePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IListAppointmentsUseCase _listAppointments;
        private readonly ICreateAppointmentUseCase _createAppointment;
        private readonly IUpdateAppointmentUseCase _updateAppointment;
        private readonly IDeactivateAppointmentUseCase _deactivateAppointment;

        public AppointmentsController(
            IListAppointmentsUseCase listAppointments,
            ICreateAppointmentUseCase createAppointment,
            IUpdateAppointmentUseCase updateAppointment,
            IDeactivateAppointmentUseCase deactivateAppointment)
        {
            _listAppointments = listAppointments;
            _createAppointment = createAppointment;
            _updateAppointment = updateAppointment;
            _deactivateAppointment = deactivateAppointment;
        }

        // GET: api/appointments?start=...&end=...&patientId=...&isActive=...
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? start, [FromQuery] DateTime? end, [FromQuery] Guid? patientId, [FromQuery] bool? isActive)
        {
            var result = await _listAppointments.ExecuteAsync(start, end, patientId, isActive);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Appointment appointment)
        {
            var result = await _createAppointment.ExecuteAsync(appointment);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id) return BadRequest();
            var result = await _updateAppointment.ExecuteAsync(appointment);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // PATCH: api/appointments/{id}/deactivate
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _deactivateAppointment.ExecuteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
