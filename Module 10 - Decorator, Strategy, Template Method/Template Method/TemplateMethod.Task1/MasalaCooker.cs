using System;

namespace TemplateMethod.Task1
{
    public class MasalaCooker
    {
        private ICooker cooker;

        public MasalaCooker(ICooker cooker)
        {
            this.cooker = cooker;
        }

        public void CookMasala(Country country)
        {
            if(country == Country.India)
            {
                cooker.FryRice(200, Level.Strong);
                cooker.PepperRice(Level.Strong);
                cooker.SaltRice(Level.Strong);

                cooker.FryChicken(100, Level.Strong);
                cooker.PepperChicken(Level.Strong);
                cooker.SaltChicken(Level.Strong);

                cooker.PrepareTea(15, TeaKind.Green, 12);
            }
            else
            {
                cooker.FryRice(500, Level.Strong);
                cooker.PepperRice(Level.Low);
                cooker.SaltRice(Level.Strong);

                cooker.FryChicken(300, Level.Medium);
                cooker.PepperChicken(Level.Low);
                cooker.SaltChicken(Level.Medium);

                cooker.PrepareTea(10, TeaKind.Black, 10);
            }
        }
    }
}
