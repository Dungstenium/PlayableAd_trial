using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeIncreaser : MonoBehaviour
{
    [SerializeField] private Text incomeText;
    public int incomeValue = 30;
    private Vector3 initialPosition;
    private Transform targetTransf;
    private RectTransform rectTransf;

    private void Start()
    {
        rectTransf = GetComponent<RectTransform>();
        initialPosition = rectTransf.anchoredPosition;

        gameObject.SetActive(false);
    }

   public void AddIncome(int value)
   {
        incomeValue += value;
        incomeText.text = $"Income: {incomeValue}";
   }

   private IEnumerator MoveTo()
   {
        float counter = 0;
        while(counter < 0.7f)
        {
            counter += Time.deltaTime;
           rectTransf.anchoredPosition = Vector3.MoveTowards(rectTransf.anchoredPosition, targetTransf.position, 1);
            yield return null;
        }
   }
}
