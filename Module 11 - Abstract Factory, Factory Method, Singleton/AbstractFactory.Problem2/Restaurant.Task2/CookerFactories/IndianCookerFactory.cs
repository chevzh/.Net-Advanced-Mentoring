using AbstartFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Task2.CookerFactories
{
    public class SummerIndianCookerFactory: ICookerFactory
    {
        public void CookRice(ICooker cooker)
        {
            cooker.FryRice(100, Level.Low);
            cooker.PepperRice(Level.Medium);
        }

        public void CookChicken(ICooker cooker)
        {
            cooker.FryChicken(100, Level.Low);
            cooker.PepperChicken(Level.Medium);
        }
    }
}
