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
    [Produces("application/json")]
    public class PatientsController : ControllerBase
    {
        private readonly IListPatientsUseCase _listPatients;
        private readonly ICreatePatientUseCase _createPatient;
        private readonly IUpdatePatientUseCase _updatePatient;
        private readonly IDeactivatePatientUseCase _deactivatePatient;
        private readonly IGetPatientDashboardUseCase _getDashboard;

        public PatientsController(
            IListPatientsUseCase listPatients,
            ICreatePatientUseCase createPatient,
            IUpdatePatientUseCase updatePatient,
            IDeactivatePatientUseCase deactivatePatient,
            IGetPatientDashboardUseCase getDashboard)
        {
            _listPatients = listPatients;
            _createPatient = createPatient;
            _updatePatient = updatePatient;
            _deactivatePatient = deactivatePatient;
            _getDashboard = getDashboard; ;
        }

        // GET: api/patients?name=...&cpf=...&isActive=...
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? cpf, [FromQuery] bool? isActive)
        {
            var result = await _listPatients.ExecuteAsync(name, cpf, isActive);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var response = result.Value!.Adapt<IEnumerable<PatientResponse>>();

            return Ok(response);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePatientRequest request)
        {
            var dto = request.Adapt<CreatePatientDto>();

            var result = await _createPatient.ExecuteAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var response = result.Value!.Adapt<PatientResponse>();

            return Created(string.Empty, response);
        }

        // PUT: api/patients/{cpf}
        [HttpPut("{cpf}")]
        public async Task<IActionResult> Put(string cpf, [FromBody] UpdatePatientRequest request)
        {
            if (!string.Equals(cpf, request.CPF, StringComparison.OrdinalIgnoreCase))
                return BadRequest("O CPF da rota e do corpo não coincidem.");

            var dto = request.Adapt<UpdatePatientDto>();

            var result = await _updatePatient.ExecuteAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // PATCH: api/patients/{cpf}/deactivate
        [HttpPatch("{cpf}/deactivate")]
        public async Task<IActionResult> Deactivate(string cpf)
        {
            var result = await _deactivatePatient.ExecuteAsync(cpf);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // GET: api/patients/dashboard
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _getDashboard.ExecuteAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var resp = result.Value!.Adapt<PatientDashboardResponse>();

            return Ok(resp);
        }
    }
}
