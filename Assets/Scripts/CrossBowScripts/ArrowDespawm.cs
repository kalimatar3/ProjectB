using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDespawm : DeSpawnByDistance
{
    protected void OnEnable()
    {
        this.Base = this.transform.parent.position;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loaddata();
    }
    protected virtual void Loaddata()
    {
        base.Distance = 100;
    }
}
