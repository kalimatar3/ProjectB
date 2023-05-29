using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DameReciver : MyMonoBehaviour
{
    [SerializeField] protected float MaxHp;
    [SerializeField] protected float CurrentHp;
    public virtual void ReducedHp(float Dame)
    {
        CurrentHp -= Dame;
        if(CurrentHp <= 0 ) CurrentHp = 0;
    }
    public virtual void IncreacsedHp(float Dame)
    {
        if(CurrentHp > MaxHp ) CurrentHp = MaxHp;
        CurrentHp += Dame;
    }
    public virtual void Reborn()
    {
        CurrentHp = MaxHp;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Reborn();
    }
}
