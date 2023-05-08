using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnByDistance : DeSpawn
{
    [SerializeField] protected Transform Base;
    [SerializeField] protected float Distance;
    protected override bool CanDeSpawn()
    {
        if((this.transform.position - Base.position).sqrMagnitude >= Distance)
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
