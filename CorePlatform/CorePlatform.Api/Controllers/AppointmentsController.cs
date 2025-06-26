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
        private readonly IGetAppointmentDashboardUseCase _getDashboard;

        public AppointmentsController(
            IListAppointmentsUseCase listAppointments,
            ICreateAppointmentUseCase createAppointment,
            IUpdateAppointmentUseCase updateAppointment,
            IDeactivateAppointmentUseCase deactivateAppointment,
            IGetAppointmentDashboardUseCase getDashboard)
        {
            _listAppointments = listAppointments;
            _createAppointment = createAppointment;
            _updateAppointment = updateAppointment;
            _deactivateAppointment = deactivateAppointment;
            _getDashboard = getDashboard;
        }

        // GET: api/appointments?start=...&end=...&patientCpf=...&isActive=...
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? start, [FromQuery] DateTime? end, [FromQuery] string? patientCpf, [FromQuery] bool? isActive)
        {
            var result = await _listAppointments.ExecuteAsync(start, end, patientCpf, isActive);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

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

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _getDashboard.ExecuteAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var response = result.Value!.Adapt<AppointmentDashboardResponse>();
            return Ok(response);
        }
    }
}
