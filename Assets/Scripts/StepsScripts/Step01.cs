using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step01 : StepBaseScript
{
    private Rigidbody2D myBody;
    [SerializeField] protected float LeftX, RightX;
     private bool Right;
    int changeX;
    private Vector3 OriginalTranform;
    protected override void Awake()
    {
        OriginalTranform = transform.localPosition;
        LeftX = -Random.Range(5, 10);
        RightX = Random.Range(5, 10);
    }
    protected override void Start()
    {
        int rd = Random.Range(0, 2);
        myBody = GetComponent<Rigidbody2D>();
        if (rd == 0) Right = true;
        else Right = false;
    }
    protected void FixedUpdate()
    {
        base.DestroyStep();
        this.MovingLR();
    }
    private void MovingLR()
    {
        if (transform.localPosition.x <= LeftX + OriginalTranform.x)
            Right = false;
        if (transform.localPosition.x >= RightX + OriginalTranform.x)
            Right = true;
        if (Right == true) myBody.velocity = new Vector2(-6, myBody.velocity.y);
        else myBody.velocity = new Vector2( 6, myBody.velocity.y);
    }
}
