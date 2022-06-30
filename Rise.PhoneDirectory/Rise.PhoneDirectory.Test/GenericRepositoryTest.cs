using Microsoft.EntityFrameworkCore;
using Rise.PhoneDirectory.Core.Repositories;
using Rise.PhoneDirectory.Repository;
using Rise.PhoneDirectory.Repository.Repositories;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Test
{
    public class GenericRepositoryTest
    {
        private readonly DbContextOptions<PhoneDirectoryDbContext> _contextOptions;
        private readonly IGenericRepository<Person> _repository;
        private readonly PhoneDirectoryDbContext _context;

        public GenericRepositoryTest()
        {
            _contextOptions = new DbContextOptionsBuilder<PhoneDirectoryDbContext>().UseInMemoryDatabase("phonedirectory").Options;
            _context = new PhoneDirectoryDbContext(_contextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _repository = new GenericRepository<Person>(_context);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        private async void GetByIdAsync_MethodExecute_ReturnPerson(int personId)
        {
            var result = await _repository.GetByIdAsync(personId);
            Assert.Equal(personId, result.PersonId);
        }

        [Fact]
        private async void GetByIdAsync_IdInValid_ReturnNull()
        {
            var result = await _repository.GetByIdAsync(500);
            Assert.True(result == null);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        private void GetById_MethodExecute_ReturnPerson(int personId)
        {
            var result = _repository.GetById(personId);
            Assert.Equal(personId, result.PersonId);
        }

        [Fact]
        private void GetById_IdInValid_ReturnNull()
        {
            var result = _repository.GetById(500);
            Assert.True(result == null);
        }


        [Fact]
        private void Where_DefinedCondition_ReturnPersons()
        {
            var realQuery = _context.Persons.Where(nq => nq.Name == "Carol" || nq.Surname == "Edmunds");
            var results = _repository.Where(nq => nq.Name == "Carol" || nq.Surname == "Edmunds");
            Assert.Equal(realQuery.Count(), results.Count());
        }


        [Fact]
        private async void AnyAsync_DefinedCondition_ReturnTrue()
        {
            var results = await _repository.AnyAsync(nq => nq.Name == "Carol" || nq.Surname == "Edmunds");
            Assert.True(results);
        }

        [Fact]
        private async void AnyAsync_DefinedCondition_ReturnFalse()
        {
            var results = await _repository.AnyAsync(nq => nq.Name == "Test");
            Assert.False(results);
        }



        [Fact]
        private void Any_DefinedCondition_ReturnTrue()
        {
            var results = _repository.Any(nq => nq.Name == "Carol" || nq.Surname == "Edmunds");
            Assert.True(results);
        }

        [Fact]
        private void Any_DefinedCondition_ReturnFalse()
        {
            var results = _repository.Any(nq => nq.Name == "Test");
            Assert.False(results);
        }


        [Fact]
        public async void AddAsync_ValidModel_Return()
        {
            var addItem = new Person() { Name = "Test", Surname = "User", CompanyName = "Test Company" };
            await _repository.AddAsync(addItem);
            Assert.True(addItem.PersonId > 0);
        }

        [Fact]
        public void Add_ValidModel_Return()
        {
            var addItem = new Person() { Name = "Test", Surname = "User", CompanyName = "Test Company" };
            _repository.Add(addItem);
            Assert.True(addItem.PersonId > 0);
        }


        [Fact]
        public async void AddRangeAsync_ValidModels_Return()
        {
            var addItems = new List<Person>
            {
                new Person() { Name = "Test 1", Surname = "User", CompanyName = "Test Company" },
                new Person() { Name = "Test 2", Surname = "User", CompanyName = "Test Company" }
            };
            await _repository.AddRangeAsync(addItems);
            Assert.True(!addItems.Any(sq => sq.PersonId == 0));
        }

        [Fact]
        public void AddRange_ValidModels_Return()
        {
            var addItems = new List<Person>
            {
                new Person() { Name = "Test 1", Surname = "User", CompanyName = "Test Company" },
                new Person() { Name = "Test 2", Surname = "User", CompanyName = "Test Company" }
            };
            _repository.AddRange(addItems);
            Assert.True(!addItems.Any(sq => sq.PersonId == 0));
        }


        [Fact]
        public void Update_ValidModel_Return()
        {
            var updateItem = _context.Persons.First(nq => nq.PersonId == 1);
            updateItem.Name = "Test";
            _repository.Update(updateItem);
            var checkItem = _context.Persons.First(nq => nq.PersonId == 1);
            Assert.Equal(updateItem.Name, checkItem.Name);
        }


        [Fact]
        public void Remove_ValidModel_Return()
        {
            var removeItem = _context.Persons.First(nq => nq.PersonId == 1);
            _repository.Remove(removeItem);
            _context.SaveChanges();
            var checkItem = _context.Persons.FirstOrDefault(nq => nq.PersonId == 1);
            Assert.True(checkItem == null);
        }


        [Fact]
        public void RemoveRange_ValidModels_Return()
        {
            var removeItem = _context.Persons.First(nq => nq.PersonId < 2);
            _repository.Remove(removeItem);
            _context.SaveChanges();
            var checkItem = _context.Persons.Where(nq => nq.PersonId < 2);
            Assert.True(checkItem.Count() == 0);
        }
    }
}