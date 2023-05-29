using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowTrap : MonoBehaviour
{
    [SerializeField] protected Transform Base,Target;
    [SerializeField] protected float ReloadTime, UpComingTime,StartTime;
    [SerializeField] protected bool Shotted,UpComing,TimeRun,starting;
    protected Transform Arrow;
    [SerializeField] protected float ArrowSpeed;
    protected Vector3 TarGetPosition;
    protected float NextShot;
    protected Vector3 GunTip;
    protected void FixedUpdate()
    {
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
    protected void SpawnArrow()
    {
        Arrow = BulletSpawner.Instance.Spawn(BulletSpawner.Arrow,Base.transform.position,Base.transform.rotation);
    }
}
