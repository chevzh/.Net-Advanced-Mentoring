using AbstartFactory;

namespace Restaurant.Task2.CookerFactories
{
    public class SummerFactory: IFactory
    {
        public ICookerFactory CreateFactory(Country country)
        {
            switch (country)
            {
                case Country.India:
                    return new SummerIndianCookerFactory();
                case Country.Ukraine:
                    return new SummerUkranianCookerFactory();
                case Country.England:
                    return new SummerEnglandCookerFactory();
                default:
                    return new SummerIndianCookerFactory();
            }
        }
    }
}
