using System;
using UnityEngine;

namespace JJN.Calculation
{
    public static class DamageCalculator
    {
        public static (int, bool) CalculateDamage(int attack, float critChance, float critMult, int defense, bool defendState)
        {
            var randomNum = UnityEngine.Random.Range(0, 100);
            bool doCrit = randomNum < critChance;
            Debug.Log($"rolled random num = {randomNum} critchance is {critChance} this means you ");
            if (!doCrit)
            {
                critMult = 1;
                Debug.Log("Dont crit");
            }
            else
            {
                Debug.Log("Do crit");
            }

            if (defendState)
            {
                return (Math.Max(0, (int)(attack * critMult - defense)), doCrit);
            }

            return ((int)(attack * critMult), doCrit);
        }
    }
}
