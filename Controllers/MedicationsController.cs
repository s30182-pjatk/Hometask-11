using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial11.DAL;
using Tutorial11.Models;
using Tutorial11.Service;

namespace Tutorial11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        
        private IHospitalService _hospitalService;

        public MedicationsController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet("getPatient/{idPatient}")]
        public async Task<IActionResult> GetPatient(int idPatient, CancellationToken cancellationToken)
        {

            var result = await _hospitalService.getPatientById(idPatient);

            return Ok(result);
        }

        [HttpPost("addPrescription")]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDTO prescription,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _hospitalService.addPrescription(prescription);
                return Ok(result);
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Medicaments count is greater than 10":
                        return BadRequest("Medicaments count is greater than 10");
                    case "Medicament not found":
                        return NotFound("Medicament not found");
                    case "Due date cannot be earlier than date of issue":
                        return BadRequest("Due date cannot be earlier than date of issue");
                }

                throw ex;
            }
        }
    }
}
