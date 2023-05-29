using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameDealtoPlayer : Damedealer
{
    protected override void SendDametoObj(Transform obj)
    {
        PlayerReciver arReciver = obj.GetComponent<PlayerReciver>();
        if(arReciver == null) return;
        arReciver.ReducedHp(this.Dame);
        arReciver.IsStuned(this.Stuntime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {       
        this.SendDametoObj(other.transform);
    }  

}
