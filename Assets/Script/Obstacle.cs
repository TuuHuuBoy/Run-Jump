using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed;

    private void Start()
    {
        
    }

    private void Update()
    {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

