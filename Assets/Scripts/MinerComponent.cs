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

        var cam = Camera.main;
        int counter = 0;
        while(counter < miningValue / 10)
        {
            var gold = Instantiate(goldPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            gold.GetComponent<GoldSpawnerNonUI>().targetTransf = target;
            counter++;
        }
    }
    
    public void GatherGold()
    {
        PlaySound(mineClip);
    }
}
