using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy 
{
    //private Rigidbody2D rb;

    [Header("Movement")]
    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;
    public List<Transform> attackList = new List<Transform>();

    protected override void Start()
    {
        base.Start();

        SwitchPoint();
    }


    void Update()
    {
        if (Mathf.Abs(transform.position.y - targetPoint.position.y) < 0.1f)
            SwitchPoint();
        
        Movement();
        
    }

    public void Movement()
    {
        if (targetPoint == pointA)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed );
        }
        else if (targetPoint == pointB)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        //FilpDirection();
    }

    public void FilpDirection()
    {
       /* if (transform.position.y < targetPoint.position.y)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);\
       */
    }

    public void SwitchPoint()
    {
        if (Mathf.Abs(pointA.position.y - transform.position.y) > Mathf.Abs(pointB.position.y - transform.position.y))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }
}
