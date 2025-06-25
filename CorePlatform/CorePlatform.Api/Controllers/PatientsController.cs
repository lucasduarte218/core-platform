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

        public PatientsController(
            IListPatientsUseCase listPatients,
            ICreatePatientUseCase createPatient,
            IUpdatePatientUseCase updatePatient,
            IDeactivatePatientUseCase deactivatePatient)
        {
            _listPatients = listPatients;
            _createPatient = createPatient;
            _updatePatient = updatePatient;
            _deactivatePatient = deactivatePatient;
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
    }
}
