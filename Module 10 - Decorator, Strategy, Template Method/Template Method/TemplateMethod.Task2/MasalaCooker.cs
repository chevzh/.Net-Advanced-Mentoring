using System;
using TemplateMethod.Task2.Cookers;

namespace TemplateMethod.Task2
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
                new IndiaCooker().CookMasala(cooker);
            }
            else
            {
                new UkraineCooker().CookMasala(cooker);
            }
        }
    }
}
