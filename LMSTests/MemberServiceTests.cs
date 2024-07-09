using LMSApi.Model;
using LMSApi.Models;
using LMSApi.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApi.Tests
{
    public class MemberServiceTests
    {
        private MemberService _service;
        private ApplicationDbContext _context;

        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetMembersAsync_ReturnsAllMembers()
        {
            // Arrange
            _context = CreateContext();
            _service = new MemberService(_context);

            _context.Members.AddRange(
                new Member { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" },
                new Member { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "0987654321" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetMembersAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetMemberByIdAsync_ReturnsMember()
        {
            // Arrange
            _context = CreateContext();
            _service = new MemberService(_context);

            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetMemberByIdAsync(1);

            // Assert
            Assert.Equal(member, result);
        }

        [Fact]
        public async Task CreateMemberAsync_AddsMember()
        {
            // Arrange
            _context = CreateContext();
            _service = new MemberService(_context);

            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" };

            // Act
            await _service.CreateMemberAsync(member);

            // Assert
            var result = await _context.Members.FindAsync(1);
            Assert.Equal(member, result);
        }

        [Fact]
        public async Task UpdateMemberAsync_UpdatesMember()
        {
            // Arrange
            _context = CreateContext();
            _service = new MemberService(_context);

            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            member.LastName = "Updated";

            // Act
            await _service.UpdateMemberAsync(member);

            // Assert
            var result = await _context.Members.FindAsync(1);
            Assert.Equal("Updated", result.LastName);
        }

        [Fact]
        public async Task DeleteMemberAsync_DeletesMember()
        {
            // Arrange
            _context = CreateContext();
            _service = new MemberService(_context);

            var member = new Member { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            // Act
            await _service.DeleteMemberAsync(1);

            // Assert
            var result = await _context.Members.FindAsync(1);
            Assert.Null(result);
        }
    }
}
