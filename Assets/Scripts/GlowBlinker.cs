using UnityEngine;

public class GlowBlinker : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private bool isRaising = true;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var rendererColor = _renderer.color;
        var prevRendererColor = rendererColor;

        if (isRaising)
        {
            rendererColor.a += 1 * Time.deltaTime;
        }
        else
        { 
            rendererColor.a -= 1 * Time.deltaTime;
        }

        if (rendererColor.a >= 1)
        {
            isRaising = false;
        }
        else if (rendererColor.a <= 0)
        {
            isRaising = true;
        }

        _renderer.color = rendererColor;
    }
}
