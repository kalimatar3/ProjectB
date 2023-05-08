using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBaseScript : MyMonoBehaviour
{
        [Header("base")]
        protected Vector3 lucsetno;
        protected LayerMask PlayerLayer ;
        protected LayerMask StepLayer;
        protected float CreateThunderTime;
         protected int h;
        protected Animator StepAnim;
        protected float ThunderAnimation;
        protected Collider2D Stepcollider;
        [SerializeField] bool CanbeDestroy = true;
   protected override void Start()
    {
            StepLayer = LayerMask.GetMask("Step");
            PlayerLayer = LayerMask.GetMask("Player");

    }
    protected void Stepbase()
    {
        this.Start();
    }
    protected void DestroyStep()
    {
        if(CanbeDestroy)
        {
        float DestroyField = 0f;
        Collider2D[] objects = Physics2D.OverlapAreaAll(transform.position +  new Vector3(-(DestroyField),DestroyField), transform.position+ new Vector3(DestroyField ,-DestroyField), StepLayer);
        if (objects.Length > 1)
        {
            Destroy(gameObject);
        }
        }
    }
}
