class Cripple : SpellCard
{
    public Cripple(int amount)
    {
        effects.Add(new SpeedEffect(amount));
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
    public PlagueCarrier(int amount)
    {
        effects.Add(new BurnEffect(amount, 3));
    }
}

class Feeble : SpellCard
{
    public Feeble(int amount)
    {
        effects.Add(new StrengthEffect(amount));
    }
}

class Fragile : SpellCard
{
    public Fragile(int amount)
    {
        effects.Add(new HealthEffect(amount));
    }
}

class Blind : SpellCard
{
    public Blind()
    {
        effects.Add(new BlindEffect());
    }
}
