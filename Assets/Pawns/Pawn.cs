using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public int combatType;

    public int maxHealthPoints;

    public int currentHealthPoints;

    //todo: should this be added?
    public int damageAmount;

    public int attackSpeed;

    public int movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyEffect(Effect effect)
    {
        if (effect is StrengthEffect)
        {
            damageAmount += ((StrengthEffect) effect).amount;
        }
    }
}
