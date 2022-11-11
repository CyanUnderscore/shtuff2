using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public bool isStarted = false;
    public float cap;
    public float direction;
    public bool isMoving = false;
    public bool isGrounded = false;
    public float jumpHeight;
    public GameObject edgeCol;
    public float recoil;
    public float recoilDir;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        cap = 5f;
        jumpHeight = 365f;
    }

    // Update is called once per frame
    void Update()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        Recoil();

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        rb = gameObject.GetComponent<Rigidbody2D>();

        if(Input.GetKey(KeyCode.D) == true)
        {
            direction = 1f;
        }
        else{
            if(Input.GetKey(KeyCode.A) == true)
            {
                direction = -1f;
            }
            else
            {
                direction = 0f;
            }
        }

        if(isStarted == false)
        {
            StartCoroutine(Move());
        }

        edgeCol = GameObject.Find("PlayerEdgeCollider");
        PlayerGroundedScript pgs = edgeCol.GetComponent<PlayerGroundedScript>();
        isGrounded = pgs.isGrounded;

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpHeight);
        }
    }

    IEnumerator Move()
    {
        isStarted = true;

        rb = gameObject.GetComponent<Rigidbody2D>();

        if(isMoving == true)
        {
            if(rb.velocity.x <= cap && rb.velocity.x >= 0)
            {
                rb.AddForce(transform.right * speed * direction);
            }
            else
            {
                if(rb.velocity.x > cap)
                {
                    rb.velocity = new Vector2(cap, rb.velocity.y);
                }
                
            }


            if(rb.velocity.x <= 0 && rb.velocity.x >= -cap)
            {
                rb.AddForce(transform.right * speed * direction);
            }
            else
            {
                if(rb.velocity.x < -cap)
                {
                    rb.velocity = new Vector2(-cap, rb.velocity.y);
                }
                
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        isStarted = false;

        yield return null;
    }

    void Recoil()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);

        if(angle > -90 && angle < 90)
        {
            recoilDir = -1f;
        }
        else
        {
            recoilDir = 1f;
        }
        
        bullet = GameObject.Find("Bullet");
        ShootingScript ss = bullet.GetComponent<ShootingScript>();
        recoil = ss.recoil;
        rb.AddForce(transform.right * recoil * recoilDir);
        ss.recoil = 0f;
    }
}
