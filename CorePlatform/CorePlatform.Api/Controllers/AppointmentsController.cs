using CorePlatform.Api.Requests;
using CorePlatform.Api.Responses;
using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using Mapster;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/appointments?start=...&end=...&patientCpf=...&isActive=...
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? start, [FromQuery] DateTime? end, [FromQuery] string? patientCpf, [FromQuery] bool? isActive)
        {
            var result = await _listAppointments.ExecuteAsync(start, end, patientCpf, isActive);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            // Map IEnumerable<Appointment> to IEnumerable<AppointmentResponse>
            var response = result.Value!.Adapt<IEnumerable<AppointmentResponse>>();
            return Ok(response);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentRequest request)
        {
            var dto = request.Adapt<CreateAppointmentDto>();

            var result = await _createAppointment.ExecuteAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var response = result.Value!.Adapt<AppointmentResponse>();
            return Created(string.Empty, response);
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAppointmentRequest request)
        {
            if (id != request.Id) return BadRequest();

            var dto = request.Adapt<UpdateAppointmentDto>();

            var result = await _updateAppointment.ExecuteAsync(dto);

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
