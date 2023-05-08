using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatMap : MyMonoBehaviour
{
    protected BoxCollider2D box;
    [SerializeField] protected int Map_Gate;
    [SerializeField] protected List<string> StepArray;
    [SerializeField] protected List<float> StCrPercents ;
    [SerializeField] protected Transform start,end;
    protected Vector3 A,B;
    protected override void Start()
    {
        Map_Gate = 1 ;
        box =  GetComponent<BoxCollider2D>();
         A = start.position;
         B = end.position;
         this.CreateStepsArray();
    }
   private void OnTriggerStay2D(Collider2D collision)
    {
        if (Map_Gate == 1)
        {
            if(collision.CompareTag("Player") || collision.CompareTag("Stunning"))
            {
            StepSpawner.Instance.CreateMap(A,B,StepArray,StepSpawner.Instance.vertical);
            Map_Gate = 0;
            return;
            }
        }
    }
    protected void CreateStepsArray()
    {
        int j = 0,dem = 0;
        for(int i = 0 ; i < StepSpawner.Instance.StepName.Count; i++)
        {
           while(j < dem + (int)(MaxStepArray()*(StCrPercents[i]/100f)) )
           {
            StepArray.Add(StepSpawner.Instance.StepName[i]);
            j++;
           } 
           dem = j;
        }
    }
    protected float UCLN(float a, float b)
        {
            while (a != b && b != 0 && a != 0)
            {
                if (a > b) a = a - b;
                else b = b - a;
            }
            return a;
        }
    protected float MaxStepArray()
        {
            float max = 0 ;
            for (int i= 0 ; i < StepSpawner.Instance.StepName.Count ; i++)
            {
                if (100/UCLN(100,StCrPercents[i]) > max){
                    max = 100/UCLN(100,StCrPercents[i]);
                }
            }
            return max;
        }
}
