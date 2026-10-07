using PlayerSystem.Inventory;
using UnityEngine;
// All game events are listed here


/// <summary>
/// Published by the dying enemy, it contains the enemy object
/// and enemy data
/// </summary>
//public class EnemyDieEventData : EventData
//{
//    public Enemy enemy;
//    public GameObject enemyObject;
//    public EnemyDieEventData(Enemy pEnemy, GameObject pEnemyObject)
//    {
//        name = "EnemyDieEvent";
//        enemy = pEnemy;
//        enemyObject = pEnemyObject;
//    }

//    //Overriding ToString method to display event information for debugging
//    public override string ToString()
//    {
//        if (enemyObject == null)
//            return "Enemy object already destroyed";
//        else
//        {
//            return "Event name: " + name + "\n" +
//                   "Enemy died at position: " + enemyObject.transform.position + "\n"
//                    + "Enemy Droped Money: " + enemy.Money
//                    + "\n" + "Enemy gave XP: " + enemy.XP;
//        }
//    }
//}

/// <summary>
/// Published by the weapon controller, it contains the position of the user
/// and the weapon data
/// </summary>

public class AttackEventData : EventData
{
    public Item weaponData;
    public GameObject weaponUser;
    public AttackEventData(Item pWeaponData,GameObject pWeaponUser)
    {
        name = "AttackEvent";
        weaponData = pWeaponData;
        weaponUser = pWeaponUser;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        return "Event name: " + name + "\n" +
            $"Weapon: {weaponData.ItemName} used by: " + weaponUser.name;
    }

}

public class DefendEventData : EventData
{
    public Item shieldData;
    public GameObject shieldUser;
    public DefendEventData(Item pShieldData, GameObject pShieldUser)
    {
        name = "DefendEvent";
        shieldData = pShieldData;
        shieldUser = pShieldUser;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        return "Event name: " + name + "\n" +
            $"Shield: {shieldData.ItemName} used by: " + shieldUser.name;
    }

}

public class ItemUseEventData : EventData
{
    public Item itemData;
    public GameObject itemUser;
    public ItemUseEventData(Item pItemData, GameObject pItemUser)
    {
        name = "UseItemEvent";
        itemData = pItemData;
        itemUser = pItemUser;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        return "Event name: " + name + "\n" +
            $"Item: {itemData.ItemName} used by: " + itemUser.name;
    }

}