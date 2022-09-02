using System.Collections;
using UnityEngine;

public class CameraZoomAdapter : MonoBehaviour
{
    [SerializeField] private float portraitSize;
    [SerializeField] private float landscapeSize;

    private Camera cam;

    [SerializeField] private float speed;

    private bool canUpdate = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        StartCoroutine(MoveTowardsNewPosition());

        StartCoroutine(LerpTowardsNewCameraSize());
    }

    private IEnumerator MoveTowardsNewPosition()
    {
        while (Vector3.Distance(cam.transform.position, transform.position) > 0.01f)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, transform.position, speed * Time.deltaTime);
            
            yield return null;
        }
    }
    
    private IEnumerator LerpTowardsNewCameraSize()
    {
        if (Screen.orientation is ScreenOrientation.Portrait or ScreenOrientation.PortraitUpsideDown)
        {
            while (cam.orthographicSize < portraitSize - 0.05f)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, portraitSize, speed * Time.deltaTime);

                yield return null;
            }
        }
        else if (Screen.orientation is ScreenOrientation.LandscapeLeft or ScreenOrientation.LandscapeRight)
        {
            while (cam.orthographicSize < landscapeSize - 0.05f)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, landscapeSize, speed * Time.deltaTime);

                yield return null;
            }
        }

        canUpdate = true;
    }

    private void Update()
    {
        if (canUpdate == false) return;

        if (Screen.orientation is ScreenOrientation.Portrait or ScreenOrientation.PortraitUpsideDown)
        {
            cam.orthographicSize = portraitSize;
        }
        else if (Screen.orientation is ScreenOrientation.LandscapeLeft or ScreenOrientation.LandscapeRight)
        {
            cam.orthographicSize = landscapeSize;
        }
    }
}
