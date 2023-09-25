using AutoFixture;
using Colleak_Back_end.Controllers;
using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.Controller
{
    public class EmployeesControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEmployeesService> _serviceMock;
        private readonly EmployeesController _sut;

        public EmployeesControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IEmployeesService>>();
            _sut = new EmployeesController(_serviceMock.Object);//creates the implementation in memory
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange
            var employeesMock = _fixture.Create<List<Employee>>();
            _serviceMock.Setup(x => x.GetEmployeeAsync()).Returns(async () => employeesMock);

            //Act
            var result = await _sut.Get().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<List<Employee>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(employeesMock.GetType());
            _serviceMock.Verify(x => x.GetEmployeeAsync(), Times.Once());
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            List<Employee> response = null;
            _serviceMock.Setup(x => x.GetEmployeeAsync()).Returns(async () => response);

            //Act
            var result = await _sut.Get().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.GetEmployeeAsync(), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnOkResponse_WhenValidInput()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.GetEmployeeAsync(employeesMock.Id)).Returns(async () => employeesMock);

            //Act
            var result = await _sut.Get(employeesMock.Id).ConfigureAwait(false);

            //Assert
            Assert.NotNull(employeesMock);
            result.Should().NotBeNull();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(employeesMock.GetType());
            _serviceMock.Verify(x => x.GetEmployeeAsync(employeesMock.Id), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arrange
            Employee response = null;
            var employeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.GetEmployeeAsync(employeesMock.Id)).Returns(async () => response);

            //Act
            var result = await _sut.Get(employeesMock.Id).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.GetEmployeeAsync(employeesMock.Id), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFound_WhenNoValidDataGiven()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.GetEmployeeAsync(employeesMock.Id)).Returns(async () => employeesMock);
            employeesMock.Id = null;

            //Act
            var result = await _sut.Get(employeesMock.Id).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task PostEmployee_ShouldReturnOk_WhenSuccesfullCreated()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.CreateEmployeeAsync(employeesMock)).Returns(async () => employeesMock);

            //Act
            var result = await _sut.Post(employeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async Task PostEmployee_ShouldReturnBadRequest_WhenInvalidData()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            employeesMock.EmployeeName = null;
            _serviceMock.Setup(x => x.CreateEmployeeAsync(employeesMock)).Returns(async () => employeesMock);

            //Act
            var result = await _sut.Post(employeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnOk_WhenSuccesfullUpdated()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            var updatedEmployeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.UpdateEmployeeAsync(employeesMock.Id, updatedEmployeesMock)).Returns(async () => updatedEmployeesMock);

            //Act
            var result = await _sut.Update(employeesMock.Id, updatedEmployeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkResult>();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnBadRequest_WhenOldEmployeeIsNull()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            var updatedEmployeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.UpdateEmployeeAsync(employeesMock.Id, updatedEmployeesMock)).Returns(async () => updatedEmployeesMock);
            employeesMock.Id = null;

            //Act
            var result = await _sut.Update(employeesMock.Id, updatedEmployeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(updatedEmployeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnBadRequest_WhenUpdatedEmployeeIsNull()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            var updatedEmployeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.UpdateEmployeeAsync(employeesMock.Id, updatedEmployeesMock)).Returns(async () => updatedEmployeesMock);
            updatedEmployeesMock = null;

            //Act
            var result = await _sut.Update(employeesMock.Id, updatedEmployeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnBadRequest_WhenUpdatedEmployeeEmployeeNameIsNull()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            var updatedEmployeesMock = _fixture.Create<Employee>();
            updatedEmployeesMock.EmployeeName = null;
            _serviceMock.Setup(x => x.UpdateEmployeeAsync(employeesMock.Id, updatedEmployeesMock)).Returns(async () => updatedEmployeesMock);

            //Act
            var result = await _sut.Update(employeesMock.Id, updatedEmployeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnOk_WhenSuccesfullyDeleted()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.DeleteEmployeeAsync(employeesMock.Id)).Returns(async () => employeesMock);

            //Act
            var result = await _sut.Delete(employeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkResult>();
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnBadRequest_WhenDataIsNull()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            var _employeesMock = employeesMock;
            _serviceMock.Setup(x => x.DeleteEmployeeAsync(employeesMock.Id)).Returns(async () => employeesMock);
            employeesMock = null;

            //Act
            var result = await _sut.Delete(employeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(_employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnBadRequest_WhenDataIdIsNull()
        {
            //Arrange
            var employeesMock = _fixture.Create<Employee>();
            var _employeesMock = employeesMock;
            _serviceMock.Setup(x => x.DeleteEmployeeAsync(employeesMock.Id)).Returns(async () => employeesMock);
            employeesMock.Id = null;

            //Act
            var result = await _sut.Delete(employeesMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(_employeesMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }
    }
}
