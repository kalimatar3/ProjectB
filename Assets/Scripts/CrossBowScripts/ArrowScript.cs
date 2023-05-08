using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowScript : MyMonoBehaviour
{
   protected Vector3 StartPosition;
   protected override void Awake()
   {
      base.Awake();
      StartPosition = transform.position;
   }
   protected void OnTriggerStay2D(Collider2D collision)
   {
    if(collision.gameObject.CompareTag("Player"))
    {
       PlayerController.Instance.DoStun(1f);
       collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * 1500);
    }
   }
}
