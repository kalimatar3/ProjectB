using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowScript : MyMonoBehaviour
{
   [SerializeField] protected float Speed = 100;
   protected void FixedUpdate()
   {
      this.transform.parent.Translate(Vector3.up * Speed * Time.deltaTime);
   }
}
