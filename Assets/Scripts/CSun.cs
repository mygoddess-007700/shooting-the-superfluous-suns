using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSun : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameController gC = GameController.instance;
        if (other.tag == "Arrow")
        {
            Destroy(other.gameObject);
            if (gC.options[gC.number-1] == "C")
            {
                gC.ShootRight();
            }
            else
            {
                gC.ShootError(3);
            }
        }       
    }
}
