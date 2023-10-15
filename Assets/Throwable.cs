using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector2 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPosition = transform.position;
        gameObject.active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
