using AbstartFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Task2.CookerFactories
{
    public class SummerEnglandCookerFactory : ICookerFactory
    {
        public void CookRice(ICooker cooker)
        {
            cooker.FryRice(50, Level.Low);
        }

        public void CookChicken(ICooker cooker)
        {
            cooker.FryChicken(50, Level.Low);
        }
    }
}
