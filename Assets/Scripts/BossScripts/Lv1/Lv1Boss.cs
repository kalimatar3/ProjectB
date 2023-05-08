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
     protected List<float> CurrentActList = new List<float>();
     protected float CurrentTime;
     [SerializeField] protected float NextAct;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetAct();
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
        CurrentTime += Time.deltaTime * 1f;
        if(CurrentTime > NextAct)
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

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}