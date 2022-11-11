using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Material[] textureList;
    public int lifeMax = 3;
    public int life;
    private int index = 3;
    private int currentLife;

    // Update is called once per frame
    void Start()
    {
        textureList = new Material[lifeMax];
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
        if (life != currentLife) {
            TextureChange();
            currentLife = life;
        }else if (life == 0){
            Destroy(gameObject, 0);}
    }
    
}
