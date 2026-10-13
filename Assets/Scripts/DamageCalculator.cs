using System;

namespace JJN.Calculation
{
    public static class DamageCalculator
    {
        public static int CalculateDamage(int attack, int defender, bool defendState)
        {
            if (defendState)
            {
                return Math.Max(0, attack - defender);
            }

            return attack;
        }
    }
}
