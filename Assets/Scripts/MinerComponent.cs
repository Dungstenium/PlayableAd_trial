using UnityEngine;
using UnityEngine.UI;

public class MinerComponent : BaseCharacter
{
    [SerializeField] private GameObject prefabText;
    [SerializeField] private GameObject goldPrefab;

    [SerializeField] private AudioClip mineClip;
    
    [SerializeField] private int miningValue;

    [SerializeField] private IncomeIncreaser incomeIncreaser;
    [SerializeField] private Transform target;
    
    private new void OnEnable()
    {
        prefabText.GetComponent<Text>().text = $"+{miningValue}";

        base.OnEnable();

        incomeIncreaser.AddIncome(miningValue);

        // incomeIncreaser.gameObject.SetActive(false);
        // incomeIncreaser.gameObject.SetActive(true);

        var cam = Camera.main;
        int counter = 0;
        while(counter < miningValue / 10)
        {
            // RectTransform tempRect = new RectTransform();
            // tempRect.position = cam.ScreenToWorldPoint(incomeIncreaser.GetComponent<RectTransform>().position);

            // goldPrefab.GetComponent<GoldSpawner>().targetTransf = tempRect;
            var gold = Instantiate(goldPrefab, transform.position, Quaternion.identity);
            gold.GetComponent<GoldSpawnerNonUI>().targetTransf = target;
            counter++;
        }
    }
    
    public void GatherGold()
    {
        PlaySound(mineClip);
    }
}
