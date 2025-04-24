namespace ServerApp;

public class CarMethods
{
    private readonly AppDbContext _context;

    public CarMethods()
    {
        _context = new AppDbContext();
    }

    public List<Car> GetAllCars()
    {
        return _context.Cars.ToList();
    }

    public string AddCar(Car car)
    {
        _context.Cars.Add(car);
        _context.SaveChanges();
        return "Car added successfully!";
    }

    public string DeleteCar(int id)
    {
        var car = _context.Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
            return "Car not found";

        _context.Cars.Remove(car);
        _context.SaveChanges();
        return "Car deleted successfully!";
    }

    public string UpdateCar(Car updatedCar)
    {
        var car = _context.Cars.FirstOrDefault(c => c.Id == updatedCar.Id);
        if (car == null)
            return "Car not found";

        car.Brand = updatedCar.Brand;
        car.Model = updatedCar.Model;
        car.Year = updatedCar.Year;

        _context.SaveChanges();
        return "Car updated successfully!";
    }
}
