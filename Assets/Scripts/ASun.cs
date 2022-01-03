using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASun : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameController gC = GameController.instance;
        if (other.tag == "Arrow")
        {
            Destroy(other.gameObject);
            if (gC.options[gC.number-1] == "A")
            {
                gC.ShootRight();
            }
            else
            {
                gC.ShootError(1);
            }
        }       
    }
}
