using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tutorial11.Controllers;
using Tutorial11.Models;
using Tutorial11.Service;
using Xunit;

namespace Tutorial11.Tests
{
    public class MedicationsControllerTests
    {
        private readonly Mock<IHospitalService> _mockHospitalService;
        private readonly MedicationsController _controller;

        public MedicationsControllerTests()
        {
            _mockHospitalService = new Mock<IHospitalService>();
            _controller = new MedicationsController(_mockHospitalService.Object);
        }

        [Fact]
        public async Task GetPatient_ReturnsOk_WithCorrectData()
        {
            // Arrange
            int testId = 1;
            var expectedPatient = new GetPatientDto
            {
                PatientId = testId,
                FirstName = "Test",
                LastName = "User",
                BirthDate = new DateTime(1990, 1, 1),
                Prescriptions = new List<PrescriptionDto>()
            };

            _mockHospitalService.Setup(service => service.getPatientById(testId))
                .ReturnsAsync(expectedPatient);

            // Act
            var result = await _controller.GetPatient(testId, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetPatientDto>(okResult.Value);
            Assert.Equal(expectedPatient.PatientId, returnValue.PatientId);
        }

        [Fact]
        public async Task AddPrescription_ReturnsOk_WithValidData()
        {
            // Arrange
            var dto = new PrescriptionDTO
            {
                Patient = new PatientDTO
                {
                    IdPatient = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(1990, 1, 1)
                },
                Doctor = new DoctorDTO
                {
                    IdDoctor = 1,
                    FirstName = "Greg",
                    LastName = "House",
                    Email = "house@example.com"
                },
                Date = "2023-01-01",
                DueDate = "2023-01-15",
                Medicaments = new List<MedicamentDTO>
                {
                    new MedicamentDTO
                    {
                        IdMedicament = 1,
                        Name = "Aspirin",
                        Type = "Tablet",
                        Dose = 2,
                        Details = "Take twice daily"
                    }
                }
            };

            _mockHospitalService.Setup(service => service.addPrescription(dto))
                .ReturnsAsync(123); // pretend new prescription ID is 123

            // Act
            var result = await _controller.AddPrescription(dto, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(123, okResult.Value);
        }
    }
}
