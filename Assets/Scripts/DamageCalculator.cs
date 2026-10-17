using System;
using UnityEngine;

namespace JJN.Calculation
{
    public static class Calculator
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

        public static string RollRarity()
        {
            int legendaryChance = 2; int epicChance = 5; int rareChance = 10; int uncommonChance = 25; 

            var randomNum = UnityEngine.Random.Range(0, 100);

            if (randomNum < legendaryChance)
            {
                return "Legendary";
            }
            else
            {
                randomNum = UnityEngine.Random.Range(0, 100);
                if (randomNum < epicChance) 
                {
                    return "Epic";
                }
                else
                {
                    randomNum = UnityEngine.Random.Range(0, 100);
                    if (randomNum < rareChance)
                    {
                        return "Rare";
                    }
                    else
                    {
                        randomNum = UnityEngine.Random.Range(0, 100);
                        if (randomNum < uncommonChance)
                        {
                            return "Uncommon";
                        }
                        else
                        {
                            return "Common";
                        }
                    }
                }
            }
        }
    }
}
