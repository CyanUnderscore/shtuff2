using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float cap;
    public float jumpHeight;
    public GameObject edgeCol;
    public float recoil;
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
		rb.velocity = new Vector3(rb.velocity.x * 0.95f, rb.velocity.y); // apply friction

        Recoil();

		float direction = 0f;

        if(Input.GetKey(KeyCode.D)) direction = 1f;
		else if(Input.GetKey(KeyCode.A)) direction = -1f;

		if(rb.velocity.x*direction <= cap) // don't apply speed up beyond speed limit ( *direction switches velocity when it's negative )
		{
			if(direction != 0f) rb.AddForce(transform.right * speed * direction);
		}

        PlayerGroundedScript pgs = edgeCol.GetComponent<PlayerGroundedScript>();

        if(pgs.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpHeight);
        }
    }

    void Recoil()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        ShootingScript ss = bullet.GetComponent<ShootingScript>();
        recoil = ss.recoil;
        rb.AddForce(-dir.normalized * recoil);
        ss.recoil = 0f;
    }
}
