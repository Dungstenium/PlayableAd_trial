using UnityEngine;

public class WorldPositionFixerComponent : MonoBehaviour
{
    public Transform target;
    private Camera cam;

    private RectTransform rectTransform;

    private void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 pos = target.position;           // get the game object position
        Vector2 viewportPoint = cam.WorldToViewportPoint(pos); //convert game object position to VievportPoint
 
        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        rectTransform.anchorMin = viewportPoint;  
        rectTransform.anchorMax = viewportPoint; 
    }
}
