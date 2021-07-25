using AbstartFactory;

namespace Restaurant.Task2.CookerFactories
{
    public interface IFactory
    {
        public ICookerFactory CreateFactory(Country country);
    }
}