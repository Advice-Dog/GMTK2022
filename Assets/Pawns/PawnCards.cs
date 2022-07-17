class Tank : PawnCard
{
    public Tank() :
        base(PawnCard.COMBAT_TYPE_WARRIOR, 3, 100, 9, 75)
    {
    }
}

class Rogue : PawnCard
{
    public Rogue() :
        base(PawnCard.COMBAT_TYPE_WARRIOR, 4, 75, 10, 75)
    {
    }
}

class Hunter : PawnCard
{
    public Hunter() :
        base(PawnCard.COMBAT_TYPE_RANGER, 3, 100, 6, 100)
    {
    }
}

class Scout : PawnCard
{
    public Scout() :
        base(PawnCard.COMBAT_TYPE_RANGER, 2, 125, 7, 100)
    {
    }
}

class Necromancer : PawnCard
{
    public Necromancer() :
        base(PawnCard.COMBAT_TYPE_WIZARD, 2, 125, 6, 125)
    {
    }
}

class Mage : PawnCard
{
    public Mage() :
        base(PawnCard.COMBAT_TYPE_WIZARD, 4, 75, 6, 125)
    {
    }
}
