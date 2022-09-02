using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldSpawner : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] public RectTransform targetTransf;
    private RectTransform rectTransf;

    private void Awake()
    {
        rectTransf = GetComponent<RectTransform>();
        initialPosition = rectTransf.anchoredPosition;
    }

    void OnEnable()
    {
        rectTransf.anchoredPosition = initialPosition;
        
        if(GetComponent<Image>() != null)
        {
            GetComponent<Image>().enabled = true;
        }
         
        StopAllCoroutines();
        StartCoroutine(MoveTo());
    }

    
   private IEnumerator MoveTo()
   {
        if(targetTransf == null) 
        {
            StopAllCoroutines();
            Destroy(gameObject);        
        }

        float counter = 0;
        while(Vector3.Distance(rectTransf.anchoredPosition, targetTransf.anchoredPosition) > 0.7f)
        {
            counter += Time.deltaTime;
            float speed = Random.Range(120, 200);
            rectTransf.anchoredPosition = Vector3.MoveTowards(rectTransf.anchoredPosition, targetTransf.anchoredPosition, speed * Time.deltaTime);
            yield return null;
        }

        if(GetComponent<Image>() != null)
        {
            GetComponent<Image>().enabled = false;
        }
   }
}
