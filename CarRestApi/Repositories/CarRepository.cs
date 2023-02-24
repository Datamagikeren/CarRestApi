namespace CarRestApi.Repositories    
{
    using CarClassLibrary;
    
    public class CarRepository
    {
        private int _nextID;
        private List<Car> _cars;
        public CarRepository()
        {
            _nextID = 1;
            _cars = new List<Car>()
            {
                new Car(){ID = _nextID++, LicensePlate = "BA55368", Model = "Huyndai", Price = 120000},
                new Car(){ID = _nextID++, LicensePlate = "OK34546", Model = "Mercedes", Price = 1000000},
                new Car(){ID = _nextID++, LicensePlate = "AB44671", Model = "Skoda", Price = 50000},
                new Car(){ID = _nextID++, LicensePlate = "GG34543", Model = "Audi", Price = 300000},
            };
        }
        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car? GetById(int id)
        {
            return _cars.Find(car => car.ID == id);
        }

        public Car Add(Car newCar)
        {
            newCar.ID = _nextID++;
            newCar.ValidateLicensePlate();
            newCar.ValidateModel();
            newCar.ValidatePrice();
            _cars.Add(newCar);
            return newCar;
        }

        public Car? Delete(int id)
        {
            Car? car = GetById(id);
            if (car == null) return null;
            _cars.Remove(car);
            return car;
        }

        public Car? Update(int id, Car updates)
        {
            Car? car = GetById(id);
            if (car == null) { return null; }
            car.LicensePlate = updates.LicensePlate;
            car.ValidateLicensePlate();
            car.Model = updates.Model;
            car.ValidateModel();
            car.Price = updates.Price;
            car.ValidatePrice();
            return car;

        }
        
    }
}
