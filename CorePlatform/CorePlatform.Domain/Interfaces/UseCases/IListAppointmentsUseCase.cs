using CorePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface IListAppointmentsUseCase
    {
        Task<IEnumerable<Appointment>> ExecuteAsync(DateTime? start, DateTime? end, Guid? patientId, bool? isActive);
    }
}