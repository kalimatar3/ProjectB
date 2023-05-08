using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    // Singleton
    private static RockManager instance;
    public int CurrentThunderPercent;
    [SerializeField] private float ScanStepTime;
    public static RockManager Instance {get => instance;}
    public float TakeDamezone;
    public float RockUpComingTime;
    [SerializeField] private float ZoneTakeStep;
    [SerializeField] private LayerMask StepLayer;
    [SerializeField] private float CreateThunderTime;
    [SerializeField] private int i, n;

    [SerializeField] protected GameObject RockTarget;
    [SerializeField] protected int[] h  = new int[30];
    protected GameObject ThisRockTarget;
    private Vector3 tamvungtaoset;
    protected void Awake()
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
    protected void Start()
    {
        tamvungtaoset = PlayerController.Instance.transform.position + new Vector3(0, 10, 0);
    }
    protected void FixedUpdate()
    {
        this.ScanStepForThundering();
    }
    protected void ScanStepForThundering()
    {
        CreateThunderTime = CreateThunderTime + 1f * Time.deltaTime;
         Collider2D[] objects = Physics2D.OverlapCircleAll(tamvungtaoset, ZoneTakeStep, StepLayer);
            n = objects.Length;
            if (objects.Length <= i)
            {
                i = 0;
            }   
                if (CreateThunderTime > ScanStepTime)
                {
                    CreateThunderTime = 0;
                    if(objects.Length != 0 && RockManager.Instance.CurrentThunderPercent != 0)
                    {
                        h[i] = Random.Range(0, 100 / RockManager.Instance.CurrentThunderPercent);
                        if (h[i] == (100 / RockManager.Instance.CurrentThunderPercent) - 1 ) 
                        {
                        ThisRockTarget = Instantiate(RockTarget,objects[i].transform.position,objects[i].transform.rotation);
                        }
                        i++;
                    }
                    tamvungtaoset = PlayerController.Instance.transform.position + new Vector3(0,10,0);
                }
    }
}
