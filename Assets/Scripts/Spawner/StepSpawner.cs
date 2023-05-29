using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StepSpawner : Spawner
{
    private static StepSpawner instance;
    public static StepSpawner Instance => instance;
    public  List<string> StepName;
    [SerializeField] protected int j;
    
    [Header("Number and potation of Step")]
    [SerializeField] protected int MinNumberof1Layer;
    [SerializeField] protected int MaxNumberof1Layer;
    [SerializeField] protected Vector2  Min , Max ;
    public string horizontal,vertical;
    protected override void Awake()
    {
        base.Awake();
        if(StepSpawner.instance != null)
        {
            Debug.LogError(" Onlly 1 StepSpawner allow to exits");
            Destroy(this);
        }
        else StepSpawner.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStepName();
        this.LoadData();
    }
    protected void LoadStepName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(StepName.Count < prefabs.Count) StepName.Add("");
            StepName[i] = prefabs[i].name;
        }
    }
    protected virtual void LoadData()
    {
       this.MinNumberof1Layer = 1;
       MaxNumberof1Layer = 2;
       Min =  new Vector2(5,6);
       Max =  new Vector2(10,10);
       horizontal = "horizontal";
       vertical = "vertical";
    }
    protected int[] BinaryModeIndex(int length)
    {
        int[] array = new int[length];
        array[0] = Random.Range(0,2);
        for(int i = 1 ; i < length;i++)
        {
            if(array[i-1] == 0) array[i] = 1;
            else array[i] = 0;
        }
        return array;
    }
    protected Vector3 RandomtransforminField(Vector3 Start,Vector3 End)
    
    {
        return new Vector3(Random.Range(Start.x,End.x),Random.Range(Start.y,End.y));
    }
    protected Vector3  randomTransform(Vector3 o,string mode, int site)
    {
        if(mode == vertical)
        {
            if(site == 1)   return RandomtransforminField(new Vector3(-Max.x,Min.y) + o, new Vector3(-Min.x , Max.y + 1) + o);
            else  return RandomtransforminField(new Vector3(Min.x, Min.y + 1 ) + o, new Vector3(Max.x,Max.y +1) + o);
        }
       else if(mode == horizontal)
        {
            if( site  == 1)    return RandomtransforminField(new Vector3(Min.x +2 ,Min.y-2) + o, new Vector3(Max.x + 1 +2 ,Max.y -2 +1) + o);
            else                return RandomtransforminField(new Vector3(Min.x +2 ,-Max.y+2) + o, new Vector3(Max.x  + 1 +2 , -Min.y +2 + 1) + o);
        }
        else return o;
    }
   public void CreateMap(Vector3 Start, Vector3 End,List<string> ListSteps,string mode)
    {
        if(mode == "vertical")
        {
        Transform NextStep = this.Spawn(ListSteps[0], new Vector3((Start.x + End.x)/2, Start.y, 0), Quaternion.identity);
        Vector3 NextPositon =  new Vector3((Start.x + End.x)/2, Start.y, 0);
        while (NextPositon.y >= Start.y && NextPositon.y < End.y)
            { 
                int k = Random.Range(0,2); 
                int i = 0,h = Random.Range(MinNumberof1Layer, MaxNumberof1Layer + 1);
                Vector3 CurentStepPos = NextPositon;
                while(i < h)
                {
                NextPositon = randomTransform(CurentStepPos,vertical,k);
                int RdStep = Random.Range(0, ListSteps.Count);
                if(NextPositon.x >= Start.x && NextPositon.x <= End.x)
                {
                i++;
                NextStep = base.Spawn(ListSteps[RdStep], NextPositon, Quaternion.identity);
                j++;
                }                
                if(k == 0) k = 1;
                else  k = 0;
                if(j >10000) return;
                }
            } 
        }
        if(mode == "horizontal")
        {
        Vector3 NextPositon =  new Vector3(Start.x,( Start.y + End.y)/2, 0);   
        int RdStep = Random.Range(0, ListSteps.Count);
        Transform NextStep = base.Spawn(ListSteps[RdStep], NextPositon, Quaternion.identity);
        while (NextPositon.x >= Start.x && NextPositon.x < End.x)
            { 
                int k = Random.Range(0,2); 
                int i = 0,h = Random.Range(MinNumberof1Layer, MaxNumberof1Layer + 1);
              Vector3  CurentStepPos = NextPositon;
                while(i < h)
                {
                NextPositon = randomTransform(CurentStepPos,horizontal,k);
                RdStep = Random.Range(0, ListSteps.Count);
                if(NextPositon.y >= Start.y && NextPositon.y <= End.y)
                {
                i++;
                NextStep = base.Spawn(ListSteps[RdStep], NextPositon, Quaternion.identity);
                j++;
                }                
                if(k == 0) k = 1;
                else  k = 0;
                if(j >10000) return;
                }
            } 
        }
    }
}
