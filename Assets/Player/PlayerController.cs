using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
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
        Recoil();

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) isMoving = true;
        else isMoving = false;

        rb = gameObject.GetComponent<Rigidbody2D>();

        if(Input.GetKey(KeyCode.D)) direction = 1f;
		else if(Input.GetKey(KeyCode.A)) direction = -1f;
		else direction = 0f;

        if(isMoving)
        {
            if(rb.velocity.x*direction <= cap) // don't apply speed up beyond speed limit ( *direction switches velocity when it's negative )
            {
                rb.AddForce(transform.right * speed * direction);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y); // apply friction
        }

        PlayerGroundedScript pgs = edgeCol.GetComponent<PlayerGroundedScript>();
        isGrounded = pgs.isGrounded;

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpHeight);
        }
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
        
        ShootingScript ss = bullet.GetComponent<ShootingScript>();
        recoil = ss.recoil;
        rb.AddForce(transform.right * recoil * recoilDir);
        ss.recoil = 0f;
    }
}
