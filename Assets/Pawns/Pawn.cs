using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn
{
    public int combatType;

    // todo: get this value elsewhere
    public int maxHealthPoints = 10;

    public int currentHealthPoints = 10;

    public int damageAmount = 1;

    public int attackSpeed = 100;

    public int movementSpeed = 100;

    // todo: possibly use a list of effects instead.
    public bool isOnFire = false;

    public Pawn()
    {
    }

    public void ApplyEffect(Effect effect)
    {
        if (effect is StrengthEffect)
        {
            damageAmount += ((StrengthEffect) effect).amount;
        }
        else if (effect is HealthEffect)
        {
            maxHealthPoints += ((HealthEffect) effect).amount;
            currentHealthPoints += ((HealthEffect) effect).amount;
            maxHealthPoints = Math.Max(1, maxHealthPoints);
            currentHealthPoints = Math.Max(1, currentHealthPoints);
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
    }
}
