using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarClassLibrary;

namespace CarRestApi.Repositories.Tests
{
    [TestClass()]
    public class CarRepositoryTests
    {
        CarRepository repo = new CarRepository();
        Car testCar = new Car() { LicensePlate = "TU33457", Model = "Polestar", Price = 200000 };

        public static void AssertCarPropertiesEqual(Car expected, Car actual) // custom AreEqual metode
        {
            Assert.AreEqual(expected.Model, actual.Model);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.LicensePlate, actual.LicensePlate);
        }


        [TestMethod()]
        public void GetAllTest() 
        {
            Assert.AreEqual(4, repo.GetAll().Count);
        }

        [TestMethod()]
        public void GetByIdTest() 
        {
            Car car = repo.GetById(3);
            Assert.IsNotNull(car);
            Assert.AreEqual(car.Model, "Skoda");
        }

        [TestMethod()]
        public void AddTest() 
        {
            repo.Add(testCar);
            Assert.AreEqual(5, repo.GetAll().Count); ;
            Assert.AreEqual("Polestar", repo.GetById(5).Model);
        }
        [TestMethod()]
        //
        public void DeleteTest()
        {
            Assert.AreEqual("Mercedes", repo.Delete(2).Model);
            repo.Delete(2);
            Assert.IsNull(repo.GetById(2));
            Assert.AreEqual(3, repo.GetAll().Count);
        }
        [TestMethod()]
        public void UpdateTest()
        {
            repo.Update(2, testCar);
            Assert.AreNotEqual("Mercedes", repo.GetById(2).Model);
            AssertCarPropertiesEqual(testCar, repo.GetById(2));

        }
    }
}