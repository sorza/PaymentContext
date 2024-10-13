using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students = [];

        public StudentQueriesTests()
        {
            for (int i = 0; i<= 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString() + "@teste.com" ))
                );
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var _std = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, _std);
        }       

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");
            var _std = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, _std);
        }   
    }
}