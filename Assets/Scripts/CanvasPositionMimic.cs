using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPositionMimic : MonoBehaviour
{
    [SerializeField] public RectTransform targetTransf;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(targetTransf.position);    
    }
}
