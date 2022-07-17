using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn
{
    public int combatType;

    public int maxHealthPoints;

    public int attackDamage;

    public int attackSpeed;

    public int movementSpeed;

    // todo: possibly use a list of effects instead.
    public bool isOnFire = false;

    public bool canJump = true;

    public bool isBlind = false;

    public Pawn(PawnCard card)
    {
        this.maxHealthPoints = card.maxHealthPoints;
        this.attackDamage = card.attackDamage;
        this.attackSpeed = card.attackSpeed;
        this.movementSpeed = card.movementSpeed;
    }

    public void ApplyEffect(Effect effect)
    {
        if (effect is StrengthEffect)
        {
            float percent = (1f - ((StrengthEffect) effect).amount);
            attackDamage = 1; //(int)((float) attackDamage * percent);
        }
        else if (effect is HealthEffect)
        {
            maxHealthPoints += ((HealthEffect) effect).amount;
            maxHealthPoints = Math.Max(1, maxHealthPoints);
        }
        else if (effect is SpeedEffect)
        {
            attackSpeed += ((SpeedEffect) effect).amount;
        }
        else if (effect is MovementSpeedEffect)
        {
            movementSpeed += ((MovementSpeedEffect) effect).amount;
        }
        else if (effect is BurnEffect)
        {
            isOnFire = true;
        }
        else if (effect is NoJumpEffect)
        {
            canJump = false;
        }
        else if (effect is BlindEffect)
        {
            isBlind = true;
        }
    }

    public override string ToString()
    {
        return "Pawn: hp: " +
        maxHealthPoints +
        ", attackDamage: " +
        attackDamage +
        ", attackSpeed: " +
        attackSpeed +
        ", movementSpeed: " +
        movementSpeed;
    }
}
