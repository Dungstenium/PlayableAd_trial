using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GlowBlinker glow;

    public List<CameraZoomAdapter> cam = new List<CameraZoomAdapter>();
    public int ActiveCameraIndex { get; set; } = 0;

    [SerializeField] private Animator charAnim;

    [SerializeField] private Animator animCTO;
    
    private void Awake()
    {
        instance = this;
    }

    public void LoadNextCamera()
    {
        var index = instance.ActiveCameraIndex;
        instance.cam[index].gameObject.SetActive(false);
        index++;
        instance.cam[index].gameObject.SetActive(true);
        instance.ActiveCameraIndex = index;
    }
    
    public void StartPhaseFour()
    {
        charAnim.enabled = true;
        LoadNextCamera();
        StartCoroutine(AnimateCTO());
    }

    private IEnumerator AnimateCTO()
    {
        yield return new WaitForSeconds(5);

        animCTO.enabled = true;
    }
}
