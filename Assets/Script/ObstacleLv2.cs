using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLv2 : MonoBehaviour
{

    public float speed = -15;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}