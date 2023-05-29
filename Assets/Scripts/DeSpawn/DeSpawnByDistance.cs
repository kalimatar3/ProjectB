using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnByDistance : DeSpawn
{
    [SerializeField] protected Vector3 Base;
    [SerializeField] protected float Distance;
    protected override bool CanDeSpawn()
    {
        if((this.transform.parent.position - Base).magnitude >= Distance)
        {
            return true;
        }
        else return false;
    }
    protected override void DeSpawnObjects()
    {
        BulletSpawner.Instance.DeSpawnToPool(this.transform.parent);
    }
}
