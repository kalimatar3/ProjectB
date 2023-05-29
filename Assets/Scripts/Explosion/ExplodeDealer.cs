using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ExplodeDealer : DameDealtoPlayer
{
    [SerializeField] protected float Force;
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected override void SendDametoObj(Transform obj)
    {
        base.SendDametoObj(obj);
        if(obj.transform.parent == null) return;
        Vector3 direction = Vector3.Normalize(obj.transform.parent.position - transform.parent.position);
        Rigidbody2D objbody = obj.transform.GetComponentInParent<Rigidbody2D>();
        if(objbody == null) return;
        objbody.AddForce(new Vector2(direction.x, direction.y )*Force, ForceMode2D.Impulse);
    }
}
