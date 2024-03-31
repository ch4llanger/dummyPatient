using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.Domain;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAppointmentsController : ControllerBase
    {
        private readonly PatientServiceContext _context;

        public PatientAppointmentsController(PatientServiceContext context)
        {
            _context = context;

        }

        // GET: api/PatientAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientAppointment>>> GetPatientAppointment()
        {
            return await _context.PatientAppointment.ToListAsync();
        }

        // GET: api/PatientAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientAppointment>> GetPatientAppointment(Guid id)
        {
            var patientAppointment = await _context.PatientAppointment.FindAsync(id);

            if (patientAppointment == null)
            {
                return NotFound();
            }

            return patientAppointment;
        }

    }
}
