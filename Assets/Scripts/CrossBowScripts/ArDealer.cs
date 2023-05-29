using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ArDealer : DameDealtoPlayer
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Dame = 2;
        this.Stuntime = 0.5f;
    }
    protected override void SendDametoObj(Transform obj)
    {
        base.SendDametoObj(obj);
        Rigidbody2D reciverbody = obj.transform.GetComponentInParent<Rigidbody2D>();
        if(reciverbody == null) return;
        reciverbody.AddForce((obj.transform.position - transform.position).normalized * 1000);
    }
}
