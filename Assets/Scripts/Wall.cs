using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Material[] textureList;
    public int lifeMax, life = 3;
	public bool breakable = true;

    void Start(){
        int currentLife = life;
    }

    void TextureChange(){
        Material mat = textureList[lifeMax - life];
        gameObject.GetComponent<SpriteRenderer>().material = mat;
    }

    void TakeDamage(){
		if(!breakable) return;

        if (life == 1){
            Destroy(gameObject, 0);
        }
        else {
            life--;
            TextureChange();
        }
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "projectile"){
            TakeDamage();
        }
    }
}
