using AbstartFactory;

namespace Restaurant.Task2.CookerFactories
{
    public interface ICookerFactory
    {
        public abstract void CookRice(ICooker cooker);
        public abstract void CookChicken(ICooker cooker);
    }
}