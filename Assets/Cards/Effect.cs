using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Effect
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

public class StrengthEffect: Effect {

    public int amount;

    public StrengthEffect(int amount) {
        this.amount = amount;
    }

}
