using System;

namespace JJN.Calculation
{
    public static class DamageCalculator
    {
        public static int CalculateDamage(int attack, float critChance, float critMult, int defender, bool defendState)
        {
            Random random = new Random();
            if (!(random.Next(0, 100) < critChance))
            {
                critMult = 1;
            }

            if (defendState)
            {
                return Math.Max(0, (int)(attack * critMult - defender));
            }

            return (int)(attack * critMult);
        }
    }
}
