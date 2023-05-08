using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTarget : MonoBehaviour
{
        [SerializeField] protected GameObject Rock;
        [SerializeField] protected bool RockFalling,RockUpComing;

    protected void Start()
    {
    RockUpComing = true;
    StartCoroutine(RunTime());
    }
    protected void FixedUpdate()
    {
     CreateRock();   
    }
protected void CreateRock()
{
    LineRenderer LineRenderer = GetComponent<LineRenderer>();
    if(RockUpComing && !RockFalling)
    {
    }
    else if(!RockUpComing && RockFalling)
    {
        Instantiate(Rock,transform.position +  new Vector3 (0,30,0) , transform.rotation);
        RockFalling = false;
    }
    else if(!RockUpComing && ! RockFalling)
    {
        Destroy(gameObject);
    }
}
    IEnumerator RunTime()
    {
        RockUpComing = true;
        yield return new WaitForSeconds(RockManager.Instance.RockUpComingTime);
        RockUpComing = false;
        RockFalling = true;
    }
}
