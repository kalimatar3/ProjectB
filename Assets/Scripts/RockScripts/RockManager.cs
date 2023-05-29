using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MyMonoBehaviour
{
    // Singleton
    private static RockManager instance;
    public static RockManager Instance {get => instance;}
    public int CurrentThunderPercent;
    public float ImpactCircle;
    public float RockUpComingTime;
    protected float Timerunner;
    protected GameObject ThisTarget;
    [SerializeField]protected Vector3 FallingZone;
    [SerializeField] protected float ScanStepTime;
    [SerializeField] protected float TakeStepCircle;
    [SerializeField] protected LayerMask StepLayer;
    [SerializeField] protected GameObject Target;
    [SerializeField] protected int[] TakeStepArray  = new int[30];
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadData();
    }
    protected virtual void LoadData()
    {
        this.CurrentThunderPercent = 50;
        this.ScanStepTime = 1;
        this.RockUpComingTime = 1;
        this.ImpactCircle = 5;
        this.TakeStepCircle = 5;
        this.StepLayer = LayerMask.NameToLayer("Step");
    }
    protected override void Awake()
    {
        if(instance != null && instance != this)
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
        FallingZone = PlayerMoving.Instance.transform.position + new Vector3(0, 5, 0);
    }
    protected void FixedUpdate()
    {
        this.ScanStep();
    }
    
    protected void ScanStep()
    {
        int i = 0;
        Timerunner = Timerunner + 1f * Time.deltaTime;
        Collider2D[] objects = Physics2D.OverlapCircleAll(FallingZone, TakeStepCircle, StepLayer);
        if (i >= objects.Length)   i = 0;
        if (Timerunner > ScanStepTime)
        {
            Timerunner = 0;
            if(objects.Length != 0 && RockManager.Instance.CurrentThunderPercent != 0)
            {
            TakeStepArray[i] = Random.Range(0, 100 / RockManager.Instance.CurrentThunderPercent);
            if (TakeStepArray[i] == (100 / RockManager.Instance.CurrentThunderPercent) - 1 ) ThisTarget = Instantiate(Target,objects[i].transform.position,objects[i].transform.rotation);
            i++;
            }
            FallingZone = PlayerMoving.Instance.transform.position + new Vector3(0,10,0);
        }
    }
}
