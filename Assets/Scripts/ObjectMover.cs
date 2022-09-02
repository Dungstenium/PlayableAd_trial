using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float moveValue;

    // Update is called once per frame
    void Update()
    {
        var tempPosition = transform.position;
        tempPosition.x += moveValue * Time.deltaTime;

        if (tempPosition.x >= 15)
        {
            tempPosition.x = -15;
        }
        else if (tempPosition.x <= -15)
        {
            tempPosition.x = 15;
        }
        
        transform.position = tempPosition;
    }
}
