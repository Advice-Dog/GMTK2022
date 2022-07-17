class Cripple : SpellCard
{
    public Cripple()
    {
        effects.Add(new SpeedEffect(-25));
    }
}

class WingsClipped : SpellCard
{
    public WingsClipped()
    {
        effects.Add(new NoJumpEffect());
    }
}

class PlagueCarrier : SpellCard
{
    public PlagueCarrier()
    {
        effects.Add(new BurnEffect(1, 3));
    }
}

class Feeble : SpellCard
{
    public Feeble()
    {
        effects.Add(new StrengthEffect(-25));
    }
}

class Fragile : SpellCard
{
    public Fragile()
    {
        effects.Add(new HealthEffect(-99));
    }
}

class Blind : SpellCard
{
    public Blind()
    {
        effects.Add(new BlindEffect());
    }
}
