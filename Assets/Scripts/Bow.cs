using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Vector2 dir = Vector2.zero;
    public float Cd;

    [Header("Reference")]
    public  GameObject arrow;

    private float nextShoot;

    void Start()
    {
        
    }

    void Update()
    {
        dir = mousePos2D() - (Vector2)transform.position;
        dir.Normalize();

        

        if (Input.GetMouseButtonDown(0) && Time.time > nextShoot && dir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(dir.y/dir.x)*(180/Mathf.PI));

            nextShoot = Time.time + Cd;
            GameObject tArrow = Instantiate(arrow, Vector3.zero, transform.rotation);
            tArrow.transform.SetParent(this.transform);
            tArrow.transform.localPosition = Vector3.right;
        }    
    }

    public Vector2 mousePos2D()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePos;
    }
}
