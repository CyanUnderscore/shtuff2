using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Material[] textureList;
    public int lifeMax = 3;
    public int life;
    private int currentLife;

    // Update is called once per frame
    void Start()
    {
        int life = lifeMax;
        int currentLife = life;
    }
    void TextureChange()
    {
        Material mat = textureList[lifeMax - life];
        gameObject.GetComponent<SpriteRenderer>().material = mat;
    }
    void TakeDamage()
    {
        life -= 1;
    }
    private void Update()
    {
        if (life == 0)
        {
            Destroy(gameObject, 0);
        }
        if (life != currentLife) {
            TextureChange();
            currentLife = life;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "projectile")
        {
            TakeDamage();
        }
    }
    
}
