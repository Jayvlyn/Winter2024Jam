using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;

public class IdleSword : BaseState2D<Sword.SwordState>
{
    private Sword sword = null;
    private Sword getSword(StateMessenger2D<Sword.SwordState> messenger)
    {
        //if (sword == null) return sword;
        Sword sword = messenger as Sword;
        return sword;
    }
    public IdleSword(Sword.SwordState key) : base(key)
    {
    }

    public override void UpdateState(StateMessenger2D<Sword.SwordState> messenger)
    {
        base.UpdateState(messenger);
        
        
    }
}

public class ThrowingSword : BaseState2D<Sword.SwordState>
{
    public ThrowingSword(Sword.SwordState key) : base(key)
    {
    }
}

public class StationarySword : BaseState2D<Sword.SwordState>
{
    public StationarySword(Sword.SwordState key) : base(key)
    {
    }
}
