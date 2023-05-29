using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RockDealer : MyMonoBehaviour
{
    [SerializeField] protected GameObject Explode;
    protected void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(Explode,this.transform.parent.position,this.transform.parent.rotation);
        Destroy(this.transform.parent.gameObject);
    }
}
