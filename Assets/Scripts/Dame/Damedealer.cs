using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damedealer : MyMonoBehaviour
{
    [SerializeField] protected int Dame;
    [SerializeField] protected float Stuntime;
    protected virtual void SendDametoObj(Transform obj)
    {
        DameReciver dameReciver = null;
        dameReciver = obj.GetComponentInChildren<DameReciver>();
        if(dameReciver == null) return;
        this.SendDame(dameReciver);
    }
    protected virtual void SendDame(DameReciver dameReciver)
    {
        dameReciver.ReducedHp(this.Dame);
        return;
    }
}
