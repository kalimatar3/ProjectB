using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MyMonoBehaviour
{
    protected LineRenderer LazerRen;
    [SerializeField] protected LayerMask CanHitLayer;
    [SerializeField] protected Transform lazertarget;
    [SerializeField] protected RaycastHit2D StepIsHitted;
    [SerializeField] protected RaycastHit2D PlayerIsHitted;
    [SerializeField] protected float NonMoveTime,MoveTime;
    [SerializeField] protected bool NonMove,Move,ReLoad;
    [SerializeField] protected int dem;
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
                Vector3 targetoffset = lazertarget.position + new Vector3(-(LazerRen.positionCount-1)*LazerRen.endWidth * 2/4 + i*LazerRen.endWidth,0,0);
                Vector3 direction = targetoffset - transform.position;
                StepIsHitted = Physics2D.Raycast(transform.position, direction , direction.magnitude , CanHitLayer );
                if(StepIsHitted)
                {
                    if(StepIsHitted.transform.CompareTag("Player"))                   
                    {
                        Lv1Boss.Instance.FoundPlayer = true;           
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
        if(dem == LazerRen.positionCount/2)
        {
        Lv1Boss.Instance.FoundPlayer = false;           
        }
    }
    protected void MovingTarget()
    {
        LazerRen = GetComponent<LineRenderer>();
        float radius = - lazertarget.position.y;
        if(NonMove && !Move)
        {
            lazertarget.position = transform.position + new Vector3(40,-40,0);
        }
        else if(!NonMove && Move)
        {
            lazertarget.Translate(new Vector3(Mathf.Sqrt((radius * radius - lazertarget.position.x * lazertarget.position.x))/radius,lazertarget.position.x/radius,0) * (120/MoveTime) * Time.deltaTime);
        }
        else
        {
            lazertarget.position = transform.position;
        }
    }   
    }
