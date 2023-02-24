using Microsoft.AspNetCore.Mvc;
using CarClassLibrary;
using CarRestApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private CarRepository _repository;
        public CarsController(CarRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<CarsController>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAll()
        {
           List<Car> result = _repository.GetAll();
            if (result.Count < 1) return NoContent();
            return _repository.GetAll();
        }

        // GET api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            if (_repository.GetById(id) == null) return NotFound("No car with that id was found");
            return _repository.GetById(id);
        }

        // POST api/<CarsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Car> Post([FromBody] Car newCar)
        {
            try
            {
                Car createdCar = _repository.Add(newCar);
                return Created($"api/pokemons/{createdCar.ID}", newCar);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Put(int id, [FromBody] Car value)
        {
            try
            {
                _repository.Update(id, value);
                if (_repository.GetById(id) == null)
                    return NotFound("You can't update a car that does not exist");
                Car car = _repository.GetById(id);
                return Created($"api/pokemons/{car.ID}", car);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            if (_repository.GetById(id) == null)
                return NotFound("That id does not exist");
            string carModel = _repository.GetById(id).Model;
            _repository.Delete(id);
            return Ok($"{carModel} was deleted");
        }
    }
}
