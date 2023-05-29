using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step03 : StepBaseScript
{
   [SerializeField] protected float CurrentTime;
    [SerializeField] private bool TimeRun;
    
    [SerializeField] protected float AliveTime;
    [SerializeField] protected float RespawTime;
    protected override void Start()
    {
        TimeRun = false;
    }
    protected void FixedUpdate()
    {
        base.DestroyStep();
        this.RunTime();
        this.fadeStep();
        base.Stepbase();
    }
    protected void RunTime()
    {
        if(TimeRun)
        {
            CurrentTime += Time.deltaTime;
        }
        else
            CurrentTime = 0f ; 
    }
    protected void fadeStep()
    {
        if(CurrentTime > AliveTime && CurrentTime < AliveTime + RespawTime)
        {
        hideComponents(false);
        }
        if(CurrentTime > AliveTime + RespawTime)
        {
        hideComponents(true);
        CurrentTime = 0f ; 
        TimeRun = false;
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && PlayerMoving.Instance.Grounded)
        {
         TimeRun = true;
        }
    }
    protected void hideComponents(bool h)
    {
          this.GetComponentInChildren<BoxCollider2D>().enabled =h;
          for(int i=0; i < this.GetComponentsInChildren<BoxCollider2D>().Length;i++)
          {
          this.GetComponentsInChildren<BoxCollider2D>()[i].enabled = h;
          }
          
       
    }
}
