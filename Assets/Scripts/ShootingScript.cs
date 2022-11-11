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
    public float recoil;
    public int weapon = 1;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        speed = 700f;
        weapon = 1;

        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerWeaponScript pws = player.GetComponent<PlayerWeaponScript>();
        weapon = pws.weapon;

        if(gameObject.name != "Bullet")
        {
            switch(weapon)
            {
            case 1:
                offset = 0f;
                break;
            case 2:
                offset = Random.Range(-10f, 10f);
                break;
            }

            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);

            rb.AddForce(transform.right * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerWeaponScript pws = player.GetComponent<PlayerWeaponScript>();
        weapon = pws.weapon;

        if(gameObject.name == "Bullet")
        {
            if(Input.GetMouseButton(0) == true && isStarted == false)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        switch(weapon)
        {
        case 1:
            recoil = 250f;
            offset = 0f;
            isStarted = true;
            player = GameObject.Find("Player");

            Instantiate(Prefab, player.transform.position, Quaternion.AngleAxis(angle + offset, Vector3.forward));
            yield return new WaitForSeconds(0.8f);
            isStarted = false;
            break;
        case 2:
            recoil = 350f;
            offset = Random.Range(-10f, 10f);
            isStarted = true;
            player = GameObject.Find("Player");

            Instantiate(Prefab, player.transform.position, Quaternion.AngleAxis(angle + offset, Vector3.forward));
            yield return new WaitForSeconds(0.1f);
            isStarted = false;
            break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "ground" && gameObject.name != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
