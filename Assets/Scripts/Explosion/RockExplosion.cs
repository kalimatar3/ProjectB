using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExplosion : ExplodeDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Dame = 1;
        this.Force = 10;
        this.Stuntime = 0.25f;
    }
}
