using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapVertical : MyMonoBehaviour
{
    //SingleTon
    private static MapVertical instance;
    public static MapVertical Instance { get => instance;}
    [SerializeField] public GameObject[] Steps;
    public int TypesofStep;
    
    [Header("Number and potation of Step")]
    [SerializeField] protected int MinNumberof1Layer;
    [SerializeField] protected int MaxNumberof1Layer;
    [SerializeField] protected int MinX, MaxX, MinY, MaxY;
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
    protected override void Start()
    {
          TypesofStep = Steps.Length;
    }
    protected Vector3  randomTransform(Vector3 o,string size)
    {
        int rd = Random.Range(0,2);
        if( size == "left")
        {
            return RandomtransforminField(new Vector3(-MaxX,MinY) + o, new Vector3(-MinX+1 , MaxY + 1) + o);
        }
        if(size == "right")  
        {
            return RandomtransforminField(new Vector3(MinX,MinY) + o, new Vector3(MaxX + 1,MaxY +1) + o);
        }
        else
        {
            return o;
        }
    }
    protected Vector3 RandomtransforminField(Vector3 Start,Vector3 End)
    
    {
        return new Vector3(Random.Range(Start.x,End.x),Random.Range(Start.y,End.y));
    }
     protected  GameObject CreateStep (GameObject[] Step,Vector3 OriginPotation,int NumberofLayer, Vector3 Minfield,Vector3 Maxfield)
    {
        GameObject NextStep = Step[0];
        int i = 0,k = NumberofLayer;
        Vector3 rd =  OriginPotation;
        while(i <= k) 
                {
            int RdStep = Random.Range(0, Step.Length);
            if(rd.x >= Minfield.x && rd.x <= Maxfield.x)
                {
                NextStep = Instantiate(Step[RdStep], rd, Quaternion.identity);
                }
            else
                {
                k++;
                }
            i++;
            if(i % 2 == 0)  rd = randomTransform(OriginPotation,"left");
            else rd = randomTransform(OriginPotation,"right");
                }
        return NextStep;
    }
   public void CreateMap(Vector3 MinField, Vector3 MaxField, GameObject[] Steps)
    {
        GameObject CurrentObject = Steps[0];
        CurrentObject.transform.position = new Vector3((MinField.x + MaxField.x)/2, MinField.y, 0);
        while (CurrentObject.transform.position.y >= MinField.y && CurrentObject.transform.position.y < MaxField.y)
            {               
                int h = Random.Range(MinNumberof1Layer, MaxNumberof1Layer + 1 );
                CurrentObject = CreateStep(Steps, CurrentObject.transform.position, h,MinField,MaxField);
            } 
    }
}
