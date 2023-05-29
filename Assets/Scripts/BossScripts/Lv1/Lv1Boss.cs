using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1Boss : Boss
{
    [SerializeField] public bool FoundPlayer;
    private static Lv1Boss instance;
    public static Lv1Boss Instance {get => instance;}
     [SerializeField] protected List<float> ActPercent ;
     [SerializeField] protected List<GameObject> AllActs;
    [SerializeField] protected List<float> CurrentActList = new List<float>();
     protected float CurrentTime;
     [SerializeField] protected float NextActTime;
    protected override void Awake()
    {
        if (instance != null && instance != this)    Destroy(this);
        else   instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetAct();
        this.LoadData();
    }
    protected void LoadData()
    {
        this.NextActTime = 2f;
        this.ActPercent.Add(100f);
        for(int i = 1 ; i < AllActs.Count;i++)  this.ActPercent.Add(0);
    }
    protected void GetAct()
    {
        GameObject Boss = transform.Find("Actions").gameObject;
        foreach (Transform Act in Boss.transform)
        {
            if(Act.gameObject == null) return;
            AllActs.Add(Act.gameObject);
        }
    }
    protected void SetActiveAct()
    {
        if(this.FoundPlayer)
        {
            GameObject Boss = transform.Find("Actions").gameObject;
            foreach (Transform Act in Boss.transform)
            {
                if(Act.GetComponent<HandsStrike>())
                {
                    Act.GetComponent<HandsStrike>().enabled = true;
                }
            }
        return;
        }
        CurrentTime += Time.deltaTime * 1f;
        if(CurrentTime > NextActTime)
        {
            CurrentTime = 0;
            CurrentActList =  Rand.Main(ActPercent);
            if(CurrentActList != null)
            {
                for(int  i = 0 ; i < CurrentActList.Count; i++)
                {
                    if(!AllActs[(int)CurrentActList[i]].activeSelf)
                    AllActs[(int)CurrentActList[i]].SetActive(true);
                }
            }
        }
    }
    protected void FixedUpdate()
    {
        this.SetActiveAct();
    }
}