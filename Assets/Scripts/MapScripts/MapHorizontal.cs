using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapHorizontal : MonoBehaviour
{
    //SingleTon
    private static MapHorizontal instance;
    public static MapHorizontal Instance { get => instance;}
    [SerializeField] public GameObject[] Steps;
    public int TypesofStep;
    
    [Header("Number and potation of Step")]
    [SerializeField] private int MinNumberof1Layer;
    [SerializeField] private int MaxNumberof1Layer;
    [SerializeField] private int MinX, MaxX, MinY, MaxY;
    protected void Awake()
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
    private void Start()
    {
          TypesofStep = Steps.Length;
    }
    protected Vector3  randomTransform(Vector3 o,string size)
    {
        if( size == "down")
        {
            return RandomtransforminField(new Vector3(MinX,-MaxY) + o, new Vector3(MaxX+1 , -MinY + 1) + o);
        }
        else if(size == "up")
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
     protected  GameObject CreateStep (GameObject[] Step,Vector3 OriginPotation, int NumberofLayer, Vector3 Minfield,Vector3 Maxfield)
    {
        GameObject NextStep = Step[0];
        int i = 0,k = NumberofLayer;
        Vector3 rd =  OriginPotation;
        while( i < k ) 
        {
            int RdStep = Random.Range(0, Step.Length);
            if(rd.y > Minfield.y && rd.y < Maxfield.y)
                {
                NextStep = Instantiate(Step[RdStep], rd, Quaternion.identity);
                }
            else
                {
                k++;
                }
            i++;
            if(i % 2 == 0) rd = randomTransform(OriginPotation,"up");
            else rd = randomTransform(OriginPotation,"down");
         }
        return NextStep;
    }
   public void CreateMap(Vector3 MinField, Vector3 MaxField, GameObject[] Steps)
    {
        GameObject CurrentObject = Steps[0];
        CurrentObject.transform.position = new Vector3(MinField.x, (MinField.y + MaxField.y)/2, 0);
        while (CurrentObject.transform.position.x >= MinField.x && CurrentObject.transform.position.x < MaxField.x)
            {               
                int h = Random.Range(MinNumberof1Layer, MaxNumberof1Layer + 1);
                CurrentObject = CreateStep(Steps, CurrentObject.transform.position, h,MinField,MaxField);
            } 
    }
}
