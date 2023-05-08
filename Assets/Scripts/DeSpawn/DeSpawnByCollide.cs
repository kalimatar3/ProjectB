 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnByCollide : DeSpawn
{
    protected bool Can;
    protected Transform ObjDes;
    [SerializeField] protected string Tagname;
    protected override bool CanDeSpawn()
    {
        return Can;
    }
    protected void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag(Tagname))
        {
           Can = true;
        }
    }
        protected void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(Tagname))
        {
           Can = false;
        }
    }

    protected override void DeSpawnObjects()
    {
        StepSpawner.Instance.DeSpawnToPool(transform.parent);
    }
}
