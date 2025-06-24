using Microsoft.AspNetCore.Mvc;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Domain.Shared;
using System;
using System.Threading.Tasks;

namespace CorePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            return Ok(result.Value);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            var result = await _createPatient.ExecuteAsync(patient);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        // PUT: api/patients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Patient patient)
        {
            if (id != patient.Id) return BadRequest();
            var result = await _updatePatient.ExecuteAsync(patient);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // PATCH: api/patients/{id}/deactivate
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _deactivatePatient.ExecuteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
