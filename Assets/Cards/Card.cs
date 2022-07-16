using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetUniqueId();
}


/**
 * A card that represents a pawn, a spawnable character.
 */
public class PawnCard : Card {

    // Using a static index to give cards a unique id
    public static int index = -1;
    public int id;
    
    public static int COMBAT_TYPE_WARRIOR = 0;
    public static int COMBAT_TYPE_RANGER = 1;
    public static int COMBAT_TYPE_WIZARD = 2;

    protected PawnCard(int combatType, int attackDamage, int attackSpeed, int maxHealthPoints, int movementSpeed) {
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
    
    public int GetUniqueId() {
        return id;
    }
}

public abstract class SpellCard: Card {

    // Using a static index to give cards a unique id
    public static int index = 1;
    public int id;

    public SpellCard() {
        this.id = index++;
    }

    public abstract void ApplyEffect(Pawn pawn);

    public int GetUniqueId() {
        return id;
    }
}