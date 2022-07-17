using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Effect
{
    public string GetDescription();
}

public class StrengthEffect: Effect {

    public int amount;

    public StrengthEffect(int amount) {
        this.amount = amount;
    }

    public string GetDescription() {
        string prefix = "increases";
        if(amount < 0) {
            prefix = "decreases";
        }
        return prefix + " attack power by " + Mathf.Abs(amount).ToString();
    }
}

public class HealthEffect: Effect {
    public int amount;

    public HealthEffect(int amount) {
        this.amount = amount;
    }

    public string GetDescription() {
        if(amount == -99) {
            return "you will die in 1 hit";
        }

        string prefix = "increases";
        if(amount < 0) {
            prefix = "decreases";
        }
        return prefix + " health by " + Mathf.Abs(amount).ToString();
    }
}

public class SpeedEffect: Effect {
    public int amount;

    public SpeedEffect(int amount) {
        this.amount = amount;
    }

    public string GetDescription() {
        string prefix = "increases";
        if(amount < 0) {
            prefix = "decreases";
        }
        return prefix + " attack speed by " + Mathf.Abs(amount).ToString() + "%";
    }
}

public class MovementSpeedEffect: Effect {
    public int amount;

    public MovementSpeedEffect(int amount) {
        this.amount = amount;
    }

    public string GetDescription() {
        string prefix = "increases";
        if(amount < 0) {
            prefix = "decreases";
        }
        return prefix + " movement speed by " + Mathf.Abs(amount).ToString() + "%";
    }
}

public class BurnEffect : Effect {
    public int amount;
    public int interval;

    public BurnEffect(int amount, int interval) {
        this.amount = amount;
        this.interval = interval;
    }

    public string GetDescription() {
        return "take " + amount.ToString() + " poison damage every " + interval + " seconds";
    }
}

public class NoJumpEffect : Effect {
    public NoJumpEffect() {

    }

    public string GetDescription() {
        return "you can no longer jump";
    }
}

public class BlindEffect : Effect {
    public BlindEffect() {

    }

    public string GetDescription() {
        return "it's getting darker";
    }
}