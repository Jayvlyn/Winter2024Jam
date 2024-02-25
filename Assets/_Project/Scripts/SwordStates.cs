using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;

public class SwordStateBase
{
    
    public virtual void OnEnterState(Sword sword) {}
    public virtual void OnExitState(Sword sword) {}
    public virtual void UpdateState(Sword sword) {}
}

public class ReelingSword : SwordStateBase
{
    public override void OnEnterState(Sword sword)
    {
        //Debug.Log("Became Reel");
    }
    public override void OnExitState(Sword sword) {}

    public override void UpdateState(Sword sword)
    {
        sword.MoveTowards(sword.PlayerTransform, sword.IdleForce);
        if (Vector2.Distance(sword.transform.position, sword.PlayerTransform.position) < sword.distanceTolerance)
        {   
            sword.ChangeState(sword.hovering);
        }
    }
}
public class StationarySword : SwordStateBase
{
    public override void OnEnterState(Sword sword)
    {
        //Debug.Log("Became Stationary");
        //AudioManager.instance.PlayOneShotAtPitch(sword.wallStabSound, Random.Range(1.5f, 2f));

        sword.Solidity(true);
        sword.Rigidbody.bodyType = RigidbodyType2D.Static;
        sword.transform.position = sword.FollowTransform.position;
        if((!sword.playerFacingRight && !sword.isFacingRight) || (sword.playerFacingRight && sword.isFacingRight))
        {
            sword.Flip();
        }

        if (sword.isFacingRight)
        {
            sword.transform.position = new Vector3(sword.transform.position.x + sword.stabOffset, sword.transform.position.y, 0);
        }
        else
        {
            sword.transform.position = new Vector3(sword.transform.position.x - sword.stabOffset, sword.transform.position.y, 0);
        }

        sword.transform.rotation = Quaternion.identity;

    }

    public override void OnExitState(Sword sword)
    {
        sword.Solidity(false);
        sword.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    public override void UpdateState(Sword sword) {}
}
public class ThrowingSword : SwordStateBase
{
    public override void OnEnterState(Sword sword)
    {
        sword.ElapsedThrowTime = 0;
        //Debug.Log("Became Thrown");
        AudioManager.instance.PlayOneShotAtPitch(sword.throwSound, Random.Range(0.95f, 1.05f));
    }

    public override void OnExitState(Sword sword)
    {
        sword.ElapsedThrowTime = 0;
    }

    public override void UpdateState(Sword sword)
    {
        if (sword.objectStabbedInto != null)
        {
            sword.ChangeState(sword.stationary);
        }
        else
        {
            if (Vector2.Distance(sword.FollowTransform.position, sword.transform.position) < 1)
            {
                sword.ChangeState(sword.reel);
            }
        }
        sword.MoveTowards(sword.FollowTransform, sword.ThrowForce);
    }
}
public class SlashingSword : SwordStateBase
{
    public override void OnEnterState(Sword sword) 
    {
    }
    public override void OnExitState(Sword sword) 
    {
    }
    public override void UpdateState(Sword sword) 
    {
    }
}
public class HoveringSword : SwordStateBase
{
    public override void OnEnterState(Sword sword) 
    {
        //Debug.Log("became Hovering");

    }
    public override void OnExitState(Sword sword) {}

    public override void UpdateState(Sword sword)
    {
        sword.FollowForce = Vector2.Distance(sword.transform.position, sword.PlayerTransform.position) * sword.FollowForceMultiplier;
        sword.MoveTowards(sword.PlayerTransform, sword.FollowForce, false);
    }
}