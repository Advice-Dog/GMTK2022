class HeroW1 : PawnCard
{
    public HeroW1() :
        base(PawnCard.COMBAT_TYPE_WARRIOR, 3, 100, 9, 75)
    {
    }
}

class HeroW2 : PawnCard
{
    public HeroW2() :
        base(PawnCard.COMBAT_TYPE_WARRIOR, 4, 75, 10, 75)
    {
    }
}

class HeroR1 : PawnCard
{
    public HeroR1() :
        base(PawnCard.COMBAT_TYPE_RANGER, 3, 100, 6, 100)
    {
    }
}

class HeroR2 : PawnCard
{
    public HeroR2() :
        base(PawnCard.COMBAT_TYPE_RANGER, 2, 125, 7, 100)
    {
    }
}

class HeroM1 : PawnCard
{
    public HeroM1() :
        base(PawnCard.COMBAT_TYPE_WIZARD, 2, 125, 6, 125)
    {
    }
}

class HeroM2 : PawnCard
{
    public HeroM2() :
        base(PawnCard.COMBAT_TYPE_WIZARD, 4, 75, 6, 125)
    {
    }
}
