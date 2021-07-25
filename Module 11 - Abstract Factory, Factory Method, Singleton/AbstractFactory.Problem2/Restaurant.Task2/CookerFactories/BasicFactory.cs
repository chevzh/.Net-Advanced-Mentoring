using AbstartFactory;

namespace Restaurant.Task2.CookerFactories
{
    public class BasicFactory
    {
        public ICookerFactory CreateFactory(Country country)
        {
            switch (country)
            {
                case Country.India:
                    return new IndianCookerFactory();
                case Country.Ukraine:
                    return new UkranianCookerFactory();
                case Country.England:
                    return new EnglandCookerFactory();
                default:
                    return new IndianCookerFactory();
            }
        }
    }
}
