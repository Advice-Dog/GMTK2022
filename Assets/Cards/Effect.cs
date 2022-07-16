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

public class HealthEffect: Effect {
    public int amount;

    public HealthEffect(int amount) {
        this.amount = amount;
    }
}

public class SpeedEffect: Effect {
    public int amount;

    public SpeedEffect(int amount) {
        this.amount = amount;
    }
}

public class MovementSpeedEffect: Effect {
    public int amount;

    public MovementSpeedEffect(int amount) {
        this.amount = amount;
    }
}

public class BurnEffect : Effect {
    public int amount;
    public int interval;

    public BurnEffect(int amount, int interval) {
        this.amount = amount;
        this.interval = interval;
    }
}