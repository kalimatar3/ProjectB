using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step02 : StepBaseScript
{
    Rigidbody2D myBody;
    public float MaxY, MinY;
    bool MaxHight;
    int changeY;
    protected override void Start()
    {
     
        changeY = Random.Range(5, 10);
        MinY = transform.position.y - changeY;
        MaxY = transform.position.y + changeY;
        int rd = Random.Range(0, 2);
        myBody = GetComponent<Rigidbody2D>();
        if (rd == 0) MaxHight = true;
        else MaxHight = false;
    }
      void Update()
    {
        base.Stepbase();
        if (transform.localPosition.y <= MinY)
             MaxHight = false;
        if (transform.localPosition.y >= MaxY)
            MaxHight = true;
        if (MaxHight == true) myBody.velocity = new Vector2(myBody.velocity.x, -6);
        else myBody.velocity = new Vector2(myBody.velocity.x, 6);
    }
}
