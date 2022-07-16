class Juice : SpellCard
{
    public Juice()
    {
        effects.Add(new StrengthEffect(1));
    }
}

class GigaJuice : SpellCard
{
    public GigaJuice()
    {
        effects.Add(new StrengthEffect(2));
        effects.Add(new SpeedEffect(-25));
    }
}

class Beef : SpellCard
{
    public Beef()
    {
        effects.Add(new HealthEffect(2));
    }
}

class GigaBeef : SpellCard
{
    public GigaBeef()
    {
        effects.Add(new HealthEffect(4));
        effects.Add(new StrengthEffect(-1));
    }
}

class Dejuice : SpellCard
{
    public Dejuice()
    {
        effects.Add(new StrengthEffect(-1));
    }
}

class GigaDejuice : SpellCard
{
    public GigaDejuice()
    {
        effects.Add(new StrengthEffect(-2));
        effects.Add(new SpeedEffect(25));
    }
}

class Swiggy : SpellCard
{
    public Swiggy()
    {
        effects.Add(new SpeedEffect(25));
    }
}

class Kachow : SpellCard
{
    public Kachow()
    {
        effects.Add(new HealthEffect(-2));
    }
}

class Bazinga : SpellCard
{
    public Bazinga()
    {
        effects.Add(new HealthEffect(-3));
        effects.Add(new StrengthEffect(1));
    }
}

class Zoom : SpellCard
{
    public Zoom()
    {
        effects.Add(new MovementSpeedEffect(25));
    }
}

// Tubular Exit
// Swerve
// Slowbro
// Gotta go fast
class Marcupial : SpellCard
{
    public Marcupial()
    {
        effects.Add(new StrengthEffect(-3));
        effects.Add(new SpeedEffect(25));
        effects.Add(new MovementSpeedEffect(25));
    }
}

class BurnBabyBurn : SpellCard
{
    public BurnBabyBurn()
    {
        effects.Add(new BurnEffect(1, 3));
    }
}

class IceIceBaby : SpellCard
{
    public IceIceBaby()
    {
        effects.Add(new MovementSpeedEffect(-25));
    }
}

// Ligma
// Sigma
// Bricked up
// Laser beams
// Pew pew
// Bananaphone
