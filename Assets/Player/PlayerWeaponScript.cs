using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    public int weapon = 1;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) == true)
        {
            switch(weapon)
            {
            case 1:
                weapon = 2;
                break;
            case 2:
                weapon = 1;
                break;
            }
        }
    }
}
