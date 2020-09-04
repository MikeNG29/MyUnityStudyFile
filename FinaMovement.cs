using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FinaMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    //private Collider2D coll;
    private Animator anim;
    //public AudioSource jumpAudio,hurtAudio,cherryAudio;

    public Collider2D DisColl;
    public float speed, jumpForce, crouchSpeed;
    public int Cherry;
    public Transform groundCheck;
    public Transform cellingCheck;
    public LayerMask ground;

    public Text CherryNum;

    private  bool isGround, isJump, isCrouch,isHurt;

    bool jumpPressed;
    bool crouchPressed;
    int jumpCount;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)                //判断跳跃键按下是否成功跳跃
        {
            jumpPressed = true;
        }


        if (!isHurt)
        {
            if (Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))      //判断趴下键按下
            {
                isCrouch = true;
            }
            else if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))      //判断趴下键按下
            {
                isCrouch = false;
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouchPressed = true;
            }
            else if (Input.GetButtonUp("Crouch") || jumpPressed)
            {
                crouchPressed = false;
            }
        }



    }

    private void FixedUpdate()             //单位时间内执行帧数的引用
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);     //检测是否在地面

        if (!isHurt)
        {
            GroundMovement();

            Jump();

            Crouch();
        }

        SwitchAnim();

        
    }

    void GroundMovement()               //角色移动
    {
       // if (!isHurt)
        //{
            float horizontalMove = Input.GetAxisRaw("Horizontal");                  /*角色按键按下的值（即方向1，-1，0）*seed速度=横轴方向的移动*/
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

            if (isCrouch || crouchPressed)
            {
                rb.velocity = new Vector2(horizontalMove * crouchSpeed, rb.velocity.y);         //趴下移动速度
            }

            if (horizontalMove != 0)                                              //按下的值不等于0时判定角色方向
            {
                transform.localScale = new Vector3(horizontalMove, 1, 1);
            }


       // }
        
    }

    void Jump()                     //角色跳跃
    {
        if (isGround)
        {
            jumpCount = 2;              //设定跳跃次数
            isJump = false;
        }
        if (jumpPressed && isGround && !isHurt)
        {
            isJump = true;
            SoundManager.instance.JumpAudio();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump && !isHurt)
        {
            SoundManager.instance.JumpAudio();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()                //角色动画
    {
        anim.SetFloat("runing", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (!isGround && rb.velocity.y < 0 )
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
            anim.SetFloat("runing", 0);
        }

        
        if (isHurt == true )            //受伤动画
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("runing", 0);
            anim.SetBool("crouch", false);
            if (Mathf.Abs(rb.velocity .x)<0.1f)
            {
                anim.SetBool("hurt",false);
                isHurt = false;
            }
        }

    }

    void Crouch()                        //角色趴下
    {
        
        if (isCrouch || crouchPressed)
        {
            DisColl.enabled = false;
            anim.SetBool("crouch", true);
        }
        else if (!isCrouch || !crouchPressed)
        {
            DisColl.enabled = true;
            anim.SetBool("crouch", false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)          //收集物
    {
        if (collision .tag =="Collection")
        {
            Collections collections = collision.gameObject.GetComponent<Collections>();
            collections.IsGot();
            Cherry += 1;
            CherryNum.text = Cherry.ToString ();
        }

        if (collision.tag == "Gem")
        {
            Collections collections = collision.gameObject.GetComponent<Collections>();
            collections.IsGot();
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)        //敌人碰撞反馈
	{
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();              
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;               
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
        }
	}

}
