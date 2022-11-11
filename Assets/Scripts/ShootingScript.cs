using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float speed;
    public GameObject Prefab;
    public GameObject player;
    public bool isStarted = false;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 800f;

        rb = gameObject.GetComponent<Rigidbody2D>();

        if(gameObject.name != "Bullet")
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            rb.AddForce(transform.up * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        if(gameObject.name == "Bullet" && Input.GetMouseButton(0) == true && isStarted == false)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        isStarted = true;
        player = GameObject.Find("Player");
        Instantiate(Prefab, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        isStarted = false;
    }
}
