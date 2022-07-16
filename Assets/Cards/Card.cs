using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Card
{
    int GetUniqueId();
}

/**
 * A card that represents a pawn, a spawnable character.
 */
public class PawnCard : Card
{
    // Using a static index to give cards a unique id
    public static int index = -1;

    public int id;

    public static int COMBAT_TYPE_WARRIOR = 0;

    public static int COMBAT_TYPE_RANGER = 1;

    public static int COMBAT_TYPE_WIZARD = 2;

    protected PawnCard(
        int combatType,
        int attackDamage,
        int attackSpeed,
        int maxHealthPoints,
        int movementSpeed
    )
    {
        // negative numbers for pawn cards
        this.id = index--;
        this.combatType = combatType;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackDamage;
        this.maxHealthPoints = maxHealthPoints;
        this.movementSpeed = movementSpeed;
    }

    public int combatType;

    public int attackDamage;

    public int attackSpeed;

    public int maxHealthPoints;

    public int movementSpeed;

    public int GetUniqueId()
    {
        return id;
    }

    public string GetDescription()
    {
        if (combatType == COMBAT_TYPE_WARRIOR)
        {
            return "Warrior";
        }
        if (combatType == COMBAT_TYPE_RANGER)
        {
            return "Ranger";
        }
        if (combatType == COMBAT_TYPE_WIZARD)
        {
            return "Wizard";
        }
        return "";
    }
}

public abstract class SpellCard : Card
{
    // Using a static index to give cards a unique id
    public static int index = 1;

    public int id;

    public List<Effect> effects = new List<Effect>();

    public SpellCard()
    {
        this.id = index++;
    }

    public void ApplyEffect(Pawn pawn)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            pawn.ApplyEffect(effects[i]);
        }
    }

    public int GetUniqueId()
    {
        return id;
    }

    public string GetDescription()
    {
        string result = "";
        for (int i = 0; i < effects.Count; i++)
        {
            result += effects[i].GetDescription();
            if (effects.Count >= 3 && i == effects.Count - 2)
            {
                result += ", and ";
            }
            else if (i != effects.Count - 1)
            {
                result += ", ";
            }
            else
            {
                result += ".";
            }
        }

        return result;
    }
}
