using LMSApi.Controllers;
using LMSApi.Model;
using LMSApi.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LMSApi.Tests
{
    public class MemberControllerTests
    {
        private readonly MemberController _controller;
        private readonly Mock<IMemberService> _mockService;

        public MemberControllerTests()
        {
            _mockService = new Mock<IMemberService>();
            _controller = new MemberController(_mockService.Object);
        }

        [Fact]
        public async Task GetMembers_ReturnsOkResult_WithListOfMembers()
        {
            var members = new List<Member>
            {
                new Member { Id = 1, FirstName = "John", LastName = "Doe" },
                new Member { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockService.Setup(s => s.GetMembersAsync()).ReturnsAsync(members);

            var result = await _controller.GetMembers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Member>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetMember_ReturnsOkResult_WithMember()
        {
            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe" };

            _mockService.Setup(s => s.GetMemberByIdAsync(1)).ReturnsAsync(member);

            var result = await _controller.GetMember(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Member>(okResult.Value);
            Assert.Equal(member, returnValue);
        }

        [Fact]
        public async Task GetMember_ReturnsNotFound_WhenMemberDoesNotExist()
        {
            _mockService.Setup(s => s.GetMemberByIdAsync(1)).ReturnsAsync((Member)null);

            var result = await _controller.GetMember(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateMember_ReturnsCreatedAtAction()
        {
            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe" };

            var result = await _controller.CreateMember(member);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Member>(createdAtActionResult.Value);
            Assert.Equal(member, returnValue);
        }

        [Fact]
        public async Task UpdateMember_ReturnsNoContent()
        {
            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe" };

            _mockService.Setup(s => s.UpdateMemberAsync(member)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateMember(1, member);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateMember_ReturnsBadRequest_WhenIdMismatch()
        {
            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe" };

            var result = await _controller.UpdateMember(2, member);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteMember_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteMemberAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteMember(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
