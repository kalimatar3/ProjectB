using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReciver : DameReciver
{
    protected float StunTime;
    public override void ReducedHp(float Dame)
    {
        base.ReducedHp(Dame);
    }
    public virtual void IsStuned( float time)
    {
        this.StunTime = time;
    }
    protected IEnumerator Stunning()
    {
        if(StunTime != 0)
        {
            PlayerCtrl.Instance.GetComponent<SpringJoint2D>().enabled = false;
            PlayerCtrl.Instance.PlayerMoving.gameObject.SetActive(false);
            PlayerCtrl.Instance.PlayerSatchelOutScript.gameObject.SetActive(false);
            PlayerCtrl.Instance.GrapplingGun.gameObject.SetActive(false);
            yield return new WaitForSeconds(StunTime);
            PlayerCtrl.Instance.PlayerMoving.gameObject.SetActive(true);
            PlayerCtrl.Instance.PlayerSatchelOutScript.gameObject.SetActive(true);
            PlayerCtrl.Instance.GrapplingGun.gameObject.SetActive(true);
            StunTime = 0 ;
        }
    }
    protected void FixedUpdate()
    {
        this.StartCoroutine(Stunning());
    }
}
