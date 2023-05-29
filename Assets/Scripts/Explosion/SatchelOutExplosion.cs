using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatchelOutExplosion : ExplodeDealer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Dame = 0 ;
        this.Stuntime = 0.2f;
        this.Force = 30;
    }
    protected override void SendDametoObj(Transform obj)
    {
        base.SendDametoObj(obj);
    }
}
