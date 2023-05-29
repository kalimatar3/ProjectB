using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeing : MyMonoBehaviour
{
    protected LineRenderer LazerRen;
    [SerializeField] protected LayerMask CanHitLayer;
    [SerializeField] public Transform Target;
    [SerializeField] protected RaycastHit2D StepIsHitted;
    [SerializeField] protected RaycastHit2D PlayerIsHitted;
    [SerializeField] protected float NonMoveTime,MoveTime;
    [SerializeField] protected bool NonMove,Move,ReLoad;
    [SerializeField]protected int dem;
    [SerializeField] protected float time,R;
    protected Vector3 VecPra;
    protected void FixedUpdate()
    {
        this.Lazer();
    }
    IEnumerator RunTime()
    {
        NonMove = true;
        yield return new WaitForSeconds(NonMoveTime);
        NonMove = false;
        Move = true;
        yield return new WaitForSeconds(MoveTime);
        Move = false;
        yield return new WaitForFixedUpdate();
        this.gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        LazerRen = GetComponent<LineRenderer>();
        LazerRen.positionCount = 1001;
        StartCoroutine(RunTime());
    }
    protected void Lazer()
    {
        dem = 0;
        this.MovingTarget();
        for( int i  = 0 ; i < LazerRen.positionCount ; i ++ )
        {
            if(i % 2 == 0)
            {
                LazerRen.SetPosition(i,transform.position + new Vector3(0,0,0));
            }
            else
            {
                dem ++;
                VecPra =  - Vector2.Perpendicular(Target.position - transform.position).normalized;
                Vector3 targetoffset = Target.position - VecPra * (-(LazerRen.positionCount-1)*LazerRen.endWidth  * 2/4 + i*LazerRen.endWidth) ; //new Vector3(-(LazerRen.positionCount-1)*LazerRen.endWidth  * 2/4 + i*LazerRen.endWidth,0,0);
                Vector3 direction = targetoffset - transform.position;
                StepIsHitted = Physics2D.Raycast(transform.position, direction , direction.magnitude , CanHitLayer );
                if(StepIsHitted)
                {
                    if(StepIsHitted.transform.CompareTag("Player"))                   
                    {
                        time = time + Time.deltaTime * 0.1f ;
                        dem--;
                    }
                    float ratio = ((StepIsHitted.transform.position.y - this.transform.position.y) / ( targetoffset.y - this.transform.position.y) );
                    LazerRen.SetPosition(i,StepIsHitted.transform.position + new Vector3((targetoffset.x - this.transform.position.x) * ratio - (StepIsHitted.transform.position.x-this.transform.position.x),0,0));
                }
                else
                {
                    LazerRen.SetPosition(i,targetoffset);
                }
            }
        }
        if(time > 1f)  Lv1Boss.Instance.FoundPlayer = true;
        if(dem == LazerRen.positionCount/2)
        {
            this.StartCoroutine(Found());
        }
    }
    protected IEnumerator Found()
    {
        time = 0;
        yield return new WaitForSeconds(0.5f);
        Lv1Boss.Instance.FoundPlayer = false;           
    }
    protected void MovingTarget()
    {
        R = 70;
        if(Lv1Boss.Instance.FoundPlayer) 
        {
           Target.position = this.transform.position + (PlayerCtrl.Instance.transform.position - this.transform.position).normalized * R ;
           NonMove = true;
           Move = false;
        }
        else
        {
            LazerRen = GetComponent<LineRenderer>();
            float radius = - Target.position.y;
            if(NonMove && !Move)
            {
                Target.position = transform.position + new Vector3(R*Mathf.Sqrt(2)/2,-R*Mathf.Sqrt(2)/2,0);
            }
            else if(!NonMove && Move)
            {
                Target.Translate(VecPra * (Mathf.PI * R /2) / MoveTime * Time.deltaTime);
                VecPra = - Vector2.Perpendicular(Target.position - transform.position).normalized;
            }
            else
            {
                Target.position = transform.position;
            }
        }
    }   
    }
