using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowTrap : MonoBehaviour
{
    [SerializeField] protected Transform Base,Target;
    [SerializeField] protected float ReloadTime, UpComingTime,StartTime;
    [SerializeField] protected bool Shotted,UpComing,TimeRun,starting;
    [SerializeField] GameObject Arrow;
    [SerializeField] protected float ArrowSpeed;
     protected Vector3 TarGetPosition;
     protected float NextShot;
    protected Vector3 GunTip;
    protected void FixedUpdate()
    {
        this.DeSpawnArrow();
        if(starting)
        ShotArrow();
    }
    protected void TimeController()
    {
       if(Time.time >= NextShot)
       {
        TarGetPosition =  Target.transform.position;
        NextShot = Time.time + ReloadTime + UpComingTime;
        StartCoroutine(TimeRunning());
       }
    }
    protected void ShotArrow()
    {
        if(TimeRun)
        {
            TimeController();
            if(!Shotted && UpComing)
            {
                ArrowisUpComing();
            }
            else if(Shotted && !UpComing)
            {
                ArrowisShotted();
            }
        }
    }
 protected void ArrowisUpComing()
 {
    LineRenderer LineRenderer = GetComponent<LineRenderer>();
    LineRenderer.positionCount = 2;
    LineRenderer.SetPosition(0,Base.transform.position);
    LineRenderer.SetPosition(1, Base.transform.position + (TarGetPosition - Base.transform.position).normalized * 100);
 }
 protected void ArrowisShotted()
 {
    GunTip = (TarGetPosition- Base.transform.position).normalized;
    LineRenderer LineRenderer = GetComponent<LineRenderer>();
    LineRenderer.positionCount = 0;
    Base.transform.up = TarGetPosition - Base.transform.position; 
    this.SpawnArrow();
 }
    IEnumerator TimeRunning()
    {       
        Shotted = false;
        UpComing = true;
        yield return new WaitForSeconds(UpComingTime);
        UpComing = false;
        Shotted = true;
        yield return new WaitForFixedUpdate();
        Shotted = false;
        TimeRun = false;
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stunning") )
        {
            TimeRun = true;
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stunning") )
        {
            StartCoroutine(DelayStart());
        }
    }
    IEnumerator DelayStart()
    {
        starting = false;
        yield  return new WaitForSeconds(StartTime);
        starting = true;
    }
    protected void DeSpawnArrow()
    {
        if((Arrow.transform.position - Base.transform.position).magnitude > 3000f )
        {
        Arrow.transform.position =  Base.transform.position;
        Arrow.SetActive(false);
        }
    }
    protected void SpawnArrow()
    {
    Arrow.transform.position =  Base.transform.position;
    Arrow.transform.rotation = Base.transform.rotation;
    Arrow.SetActive(true);
    Arrow.GetComponent<Rigidbody2D>().velocity =  GunTip*ArrowSpeed;
    }
}
