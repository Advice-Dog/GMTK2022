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
}


/**
 * A card that represents a pawn, a spawnable character.
 */
public class PawnCard : Card {
    
    public static int COMBAT_TYPE_WARRIOR = 0;
    public static int COMBAT_TYPE_RANGER = 1;
    public static int COMBAT_TYPE_WIZARD = 2;

    protected PawnCard(int combatType, int attackDamage, int attackSpeed, int maxHealthPoints, int movementSpeed) {
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
    
}

public abstract class SpellCard: Card {

    public abstract void ApplyEffect(Pawn pawn);

}