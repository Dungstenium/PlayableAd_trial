using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageChangerComponent : MonoBehaviour
{
    [SerializeField] private Sprite portraitImage;
    [SerializeField] private Sprite landscapeImage;
    [SerializeField] private RectTransform incomeGO;

    private float counter = 0;
    
    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        
        SwapOrientationImage();
        ExpandImage();
        SwapIncomeLocation();
    }

    private void OnRectTransformDimensionsChange()
    {
        SwapOrientationImage();
        SwapIncomeLocation();
    }

    private void SwapOrientationImage()
    {
        if (imageComponent == null) return;

        if (Screen.orientation is ScreenOrientation.Portrait or ScreenOrientation.PortraitUpsideDown)
        {
            imageComponent.sprite = portraitImage;
        }
        else if (Screen.orientation is ScreenOrientation.LandscapeLeft or ScreenOrientation.LandscapeRight)
        {
            imageComponent.sprite = landscapeImage;
        }
    }

    private void SwapIncomeLocation()
    {
        if (incomeGO == null) return;

        if (Screen.orientation is ScreenOrientation.Portrait or ScreenOrientation.PortraitUpsideDown)
        {
            incomeGO.anchoredPosition = new Vector2(0 , -361);
        }
        else if (Screen.orientation is ScreenOrientation.LandscapeLeft or ScreenOrientation.LandscapeRight)
        {
            incomeGO.anchoredPosition = new Vector2(-390 , 0);
        }
    }

    public void ExpandImage()
    {
        StartCoroutine(Expand());
    }

    private IEnumerator Expand()
    {
        while (true)
        {
            var localScale = imageComponent.rectTransform.localScale;
            localScale += localScale * 0.05f * Time.deltaTime;
            imageComponent.rectTransform.localScale = localScale;

            counter += Time.deltaTime;
            if (counter >= 3)
            {
                gameObject.SetActive(false);
            }
            
            yield return null;
        }
    }
}
