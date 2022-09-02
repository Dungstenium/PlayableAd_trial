using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawnerNonUI : MonoBehaviour
{
    [SerializeField] public Transform targetTransf;
    float speed = 100;

    void Start()
    {
        speed = Random.Range(10, 20);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransf.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetTransf.position) < 0.07f)
        {
            Destroy(gameObject);
        }
    }
}
