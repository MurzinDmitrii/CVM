using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;
using Курсач.Classes;
using Курсач.Model;

namespace CVM_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodVerifyHashedPassword()
        {
            string hash = "ADT0LyjuQNeOBZQlNPqI6f8oai0sPa2xb6cBy9Mqeiq3dcb9O6mEDXQ93kDtlNRe+g==";
            string password = "123";
            Assert.IsTrue(Cryptography.VerifyHashedPassword(hash, password));
        }
        [TestMethod]
        public void TestMethod1()
        {
            Passport passport = DB.entities.Passport.FirstOrDefault(c => c.ClientId == 3);
            byte[] hash = passport.PassportSeria;
            string password = "1234";
            byte[] s = Cryptography.EncryptData(Encoding.UTF8.GetBytes(password));
            Assert.AreEqual(System.Text.Encoding.UTF8.GetString(Cryptography.DecryptData(passport.PassportSeria)), password);
        }
    }
}
