using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy 
{
    //private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D Coll;


    [Header("Movement")]
    public float speed,jumpForce;
    public LayerMask Ground;
    public Transform pointA, pointB;
    public Transform targetPoint;
    public List<Transform> attackList = new List<Transform>();
    protected override void Start()                          //调用父类函数
    {
        base.Start();
        //rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();

        SwitchPoint();
    }


    void Update()
    {
        if (Mathf .Abs (transform .position .x-targetPoint .position .x )<0.1f)       //到达目标点后执行以下函数
            SwitchPoint();
        SwitchAnim();
        //Movement();
    }

    public void Movement()                        //目标移动
    {
        if (targetPoint == pointA && Coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(-speed, jumpForce);
            Anim.SetBool("jumping", true);
        }
        else if (targetPoint == pointB && Coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(speed, jumpForce);
            Anim.SetBool("jumping", true);
        }
        FilpDirection();
    }

    public void FilpDirection()                     //面向翻转
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void SwitchPoint()               //切换目标点
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
    public void SwitchAnim()
    {
        if (Anim.GetBool("jumping") && rb.velocity .y <0.1)
        {
            Anim.SetBool("jumping", false);
            Anim.SetBool("falling", true);
        }
        if(Coll.IsTouchingLayers (Ground )&&Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
           // Anim.SetBool("idle", true);
        }
    }
    
}
