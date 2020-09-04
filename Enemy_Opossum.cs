using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Opossum : Enemy 
{
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
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.1f)
            SwitchPoint();
        Movement();
    }

    public void Movement()
    {
        if (targetPoint == pointA)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (targetPoint == pointB)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        FilpDirection();
    }

    public void FilpDirection()
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void SwitchPoint()
    {
        if (Mathf.Abs(pointA.position.x - transform.position.x) > Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }
}
