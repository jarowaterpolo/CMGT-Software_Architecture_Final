using JJN.Enemy;
using JJN.PlayerSystem.Inventory;
using UnityEngine;
// All game events are listed here


/// <summary>
/// Published by the dying enemy, it contains the enemy object
/// and enemy data
/// </summary>
public class EnemyDieEventData : EventData
{
    public Enemy enemy;
    public GameObject enemyObject;
    public EnemyDieEventData(Enemy pEnemy, GameObject pEnemyObject)
    {
        name = "EnemyDieEvent";
        enemy = pEnemy;
        enemyObject = pEnemyObject;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        if (enemyObject == null)
            return "Enemy object already destroyed";
        else
        {
            return "Event name: " + name + "\n" +
                   "Enemy died at position: " + enemyObject.transform.position + "\n"
                    + "Enemy Droped Money: " + enemy.Money
                    + "\n" + "Enemy gave XP: " + enemy.XP;
        }
    }
}

/// <summary>
/// Published by the player when trying to open a item container, it contains the player transform
/// </summary>
public class OpenItemContainerEventData : EventData
{
    public Transform playerTransform;
    public OpenItemContainerEventData(Transform pPlayerTransform)
    {
        name = "OpenItemContainerEvent";
        playerTransform = pPlayerTransform;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        if (playerTransform == null)
            return "player not found";
        else
        {
            return "Event name: " + name + "\n" +
                   "Cast at player position: " + playerTransform.position + "\n";
        }
    }
}

/// <summary>
/// Published by the player when trying to equip a new item
/// </summary>
public class EquipItemEventData : EventData
{
    public Item itemToEquip;
    public EquipItemEventData(Item pItem)
    {
        name = "OpenItemContainerEvent";
        itemToEquip = pItem;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        if (itemToEquip == null)
            return "no item or null";
        else
        {
            return "Event name: " + name + "\n" +
                   "equip event run with: " + itemToEquip.ItemName + "\n";
        }
    }
}

/// <summary>
/// Published by the weapon controller, it contains the position of the user
/// and the weapon data
/// </summary>

public class AttackEventData : EventData
{
    public Weapon weaponData;
    public GameObject weaponUser;
    public AttackEventData(Weapon pWeaponData,GameObject pWeaponUser)
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
    public Shield shieldData;
    public GameObject shieldUser;
    public bool isBlocking;
    public DefendEventData(Shield pShieldData, GameObject pShieldUser, bool pIsBlocking)
    {
        name = "DefendEvent";
        shieldData = pShieldData;
        shieldUser = pShieldUser;
        isBlocking = pIsBlocking;
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
    public SpecialItem itemData;
    public GameObject itemUser;
    public ItemUseEventData(SpecialItem pItemData, GameObject pItemUser)
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

public class QuestEventData : EventData
{
    public string currentQuestText;
    public bool questIsComplete;
    public QuestEventData(string pCurrentQuestText, bool pQuestIsComplete)
    {
        name = "QuestEvent";
        currentQuestText = pCurrentQuestText;
        questIsComplete = pQuestIsComplete;
    }

    //Overriding ToString method to display event information for debugging
    public override string ToString()
    {
        return "Event name: " + name + "\n" +
            $"current quest text = {currentQuestText} and completed = " + questIsComplete;
    }

}