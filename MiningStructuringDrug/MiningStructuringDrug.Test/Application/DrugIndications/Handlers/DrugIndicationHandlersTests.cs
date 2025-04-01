using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiningStructuringDrug.Core.Application.DrugIndications.Commands;
using MiningStructuringDrug.Core.Application.DrugIndications.Handlers;
using MiningStructuringDrug.Core.Application.DrugIndications.Queries;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;
using Moq;

namespace MiningStructuringDrug.Test.Application.DrugIndications.Handlers
{
    public class DrugIndicationHandlersTests
    {
        [Fact]
        public async Task CreateDrugIndicationCommandHandler_ValidCommand_ReturnsDrugIndicationDto()
        {
            // Arrange
            var mockRepo = new Mock<IDrugIndicationRepository>();
            var handler = new CreateDrugIndicationCommandHandler(mockRepo.Object);
            var command = new CreateDrugIndicationCommand
            {
                DrugName = "TestDrug",
                Indications = new List<string> { "Indication1", "Indication2" },
                ICD10Codes = new List<string> { "ICD1", "ICD2" }
            };

            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<DrugIndication>()))
                .Callback<DrugIndication>(entity => entity.Id = 1); // Simulate setting the ID

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestDrug", result.DrugName);
            Assert.Equal(2, result.Indications.Count);
            Assert.Equal(2, result.IcD10Codes.Count);
            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<DrugIndication>()), Times.Once);
        }

        [Fact]
        public async Task GetDrugIndicationQueryHandler_ValidId_ReturnsDrugIndicationDto()
        {
            // Arrange
            var mockRepo = new Mock<IDrugIndicationRepository>();
            var handler = new GetDrugIndicationQueryHandler(mockRepo.Object);
            var query = new GetDrugIndicationQuery { Id = 1 };
            var drugIndication = new DrugIndication
            {
                Id = 1,
                DrugName = "TestDrug",
                Indications = new List<string> { "Indication1" },
                IcD10Codes = new List<string> { "ICD1" }
            };

            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(drugIndication);

            // Act
            var result = await handler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("TestDrug", result.DrugName);
            Assert.Single(result.Indications);
            Assert.Single(result.IcD10Codes);
        }

        [Fact]
        public async Task GetDrugIndicationQueryHandler_InvalidId_ReturnsNull()
        {
            // Arrange
            var mockRepo = new Mock<IDrugIndicationRepository>();
            var handler = new GetDrugIndicationQueryHandler(mockRepo.Object);
            var query = new GetDrugIndicationQuery { Id = 99 }; // Invalid ID

            mockRepo.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((DrugIndication)null);

            // Act
            var result = await handler.Handle(query);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ListDrugIndicationsQueryHandler_ReturnsListOfDrugIndicationDto()
        {
            // Arrange
            var mockRepo = new Mock<IDrugIndicationRepository>();
            var handler = new ListDrugIndicationsQueryHandler(mockRepo.Object);
            var query = new ListDrugIndicationsQuery();
            var drugIndications = new List<DrugIndication>
        {
            new DrugIndication { Id = 1, DrugName = "Drug1" },
            new DrugIndication { Id = 2, DrugName = "Drug2" }
        };

            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(drugIndications);

            // Act
            var result = await handler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Drug1", result[0].DrugName);
            Assert.Equal("Drug2", result[1].DrugName);
        }

        [Fact]
        public async Task UpdateDrugIndicationCommandHandler_ValidCommand_CallsRepositoryUpdate()
        {
            // Arrange
            var mockRepo = new Mock<IDrugIndicationRepository>();
            var handler = new UpdateDrugIndicationCommandHandler(mockRepo.Object);
            var command = new UpdateDrugIndicationCommand
            {
                Id = 1,
                DrugName = "UpdatedDrug",
                Indications = new List<string> { "UpdatedIndication" },
                IcD10Codes = new List<string> { "UpdatedICD" }
            };

            // Act
            await handler.Handle(command);

            // Assert
            mockRepo.Verify(repo => repo.UpdateAsync(It.Is<DrugIndication>(di => di.Id == 1)), Times.Once);
        }

        [Fact]
        public async Task DeleteDrugIndicationCommandHandler_ValidCommand_CallsRepositoryDelete()
        {
            // Arrange
            var mockRepo = new Mock<IDrugIndicationRepository>();
            var handler = new DeleteDrugIndicationCommandHandler(mockRepo.Object);
            var command = new DeleteDrugIndicationCommand { Id = 1 };

            // Act
            await handler.Handle(command);

            // Assert
            mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
