class Juice : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new StrengthEffect(1));
    }
}

class GigaJuice : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new StrengthEffect(2));
        pawn.ApplyEffect(new SpeedEffect(-25));
    }
}

class Beef : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new HealthEffect(2));
    }
}

class GigaBeef : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new HealthEffect(4));
        pawn.ApplyEffect(new StrengthEffect(-1));
    }
}

class Dejuice : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new StrengthEffect(-1));
    }
}

class GigaDejuice : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new StrengthEffect(-2));
        pawn.ApplyEffect(new SpeedEffect(25));
    }
}

class Swiggy : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new SpeedEffect(25));
    }
}

class Kachow : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new HealthEffect(-2));
    }
}

class Bazinga : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new HealthEffect(-3));
        pawn.ApplyEffect(new StrengthEffect(1));
    }
}

class Zoom : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new MovementSpeedEffect(25));
    }
}

// Tubular Exit
// Swerve
// Slowbro
// Gotta go fast
class Marcupial : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new StrengthEffect(-3));
        pawn.ApplyEffect(new SpeedEffect(25));
        pawn.ApplyEffect(new MovementSpeedEffect(25));
    }
}

class BurnBabyBurn : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new BurnEffect(1, 3));
    }
}

class IceIceBaby : SpellCard
{
    public override void ApplyEffect(Pawn pawn)
    {
        pawn.ApplyEffect(new MovementSpeedEffect(-25));
    }
}

// Ligma
// Sigma
// Bricked up
// Laser beams
// Pew pew
// Bananaphone
