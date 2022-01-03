using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 5f;
    public float liveTime = 2f;
    
    private float liveDone;
    private Vector2 tPos;

    void Start()
    {
        liveDone = Time.time + liveTime;    
        transform.SetParent(null);
    }
    
    void FixedUpdate()
    {
        tPos = transform.position;
        tPos = tPos + (Vector2)transform.right * speed * Time.fixedDeltaTime;
        transform.position = tPos;

        if (Time.time > liveDone)
        {
            Destroy(this.gameObject);
        }
    }
}
