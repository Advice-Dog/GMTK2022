using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
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
public class PawnCard: Card {
    
    public static int COMBAT_TYPE_MELEE = 0;
    public static int COMBAT_TYPE_RANGED = 1;

    protected PawnCard(int combatType) {
        this.combatType = combatType;
    }

    public int combatType;
    public int maxHealthPoints;
    
    //todo: should this be added?
    public int damageAmount;

    public int attackSpeed;
    public int movementSpeed;
    
}

public class WarriorCard: PawnCard {

    public WarriorCard(): base(PawnCard.COMBAT_TYPE_MELEE) {

    }
}

public class MageCard: PawnCard {

    public MageCard() : base(PawnCard.COMBAT_TYPE_RANGED) {

    }

}

public abstract class SpellCard: Card {

    public abstract void ApplyEffect(Pawn pawn);

}

public class StrengthCard: SpellCard {

    public override void ApplyEffect(Pawn pawn) {
        pawn.ApplyEffect(new StrengthEffect(2));
    }

}