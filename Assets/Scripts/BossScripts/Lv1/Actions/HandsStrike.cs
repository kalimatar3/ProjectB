using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsStrike : MyMonoBehaviour
{
    [SerializeField] protected bool Upcoming,Strike;
    [SerializeField] protected float UpcomingTime,StrikeTime;
    [SerializeField] public Transform Base,Target;
    [SerializeField] protected GameObject Hand;
    [SerializeField] protected float CurrenttargetPos;
    [SerializeField] protected float CurrentbasePos;
    [SerializeField] protected List<Transform> ListPos;
     protected LineRenderer thisRen;
     protected float CanTakePos;
     protected void Targercontroller()
     {
        if(CanTakePos == 1 )
        {
        CanTakePos = 0;
        Base.transform.position = ListPos[(int)CurrentbasePos].transform.position;
        Target.transform.position = ListPos[(int)CurrenttargetPos].transform.position;
        }
        Base.transform.up = Target.transform.position - Base.transform.position ;
     }
     public void RandomTarGet()
     {
        if(Lv1Boss.Instance.FoundPlayer) 
        {
           Target.position = this.Base.transform.position + (PlayerCtrl.Instance.transform.position - this.Base.transform.position).normalized * 60;
            return;
        }
        List<float> thislist = new List<float>() {100/6f,100/6,100/6,100/6,100/6,100/6};
        List<float> alist = Rand.Main(thislist); 
        CurrenttargetPos = alist[0];
        if(CurrenttargetPos >= 3 && CurrenttargetPos <= 5)
        {
        thislist = new List<float>() {100/3,100/3,100/3,0,0,0}; 
        }
        else
        {
        thislist = new List<float>() {0,0,0,100/3,100/3,100 /3};
        }
        alist = Rand.Main(thislist);
        CurrentbasePos = alist[0];
     }
    IEnumerator RunTime()
    {
        Upcoming = true;
        yield return new WaitForSeconds(UpcomingTime);
        Upcoming = false;
        Strike = true;
        yield return new WaitForSeconds(StrikeTime);
        Strike = false;
        yield return new WaitForFixedUpdate();
        this.gameObject.SetActive(false);
    }
    protected void ThisAction()
    {
        if(Upcoming && !Strike)
        {
            this.RandomTarGet();
            this.Targercontroller();
            this.StrikeUpcoming();
        }
        else if(!Upcoming && Strike )
        {
            this.Striking();
        }
        else
        {
            this.Striked();
        }
    }
    protected void OnEnable()
    {
        this.CanTakePos = 1;
        StartCoroutine(RunTime());
    }
    protected void StrikeUpcoming()
    {
        thisRen = GetComponent<LineRenderer>();
        thisRen.positionCount = 2;
        thisRen.SetPosition(0,Base.transform.position);
        thisRen.SetPosition(1,Target.transform.position);
        Hand.SetActive(true);
        Hand.transform.position = Base.transform.position;
        Hand.transform.rotation = Base.transform.rotation;
    }
    protected void Striking()
    {
        thisRen = GetComponent<LineRenderer>();
        thisRen.positionCount = 0;
        Vector3 direct = Target.transform.position - Hand.transform.position;
        Hand.transform.Translate(direct.magnitude * Vector3.up * 5f * Time.deltaTime);
    }
    protected void Striked()
    {
        Hand.SetActive(false);
    }
    protected void FixedUpdate()
    {
        this.ThisAction();
    } 
}
