using UnityEngine;
using UnityEngine.UI;

public class MinerComponent : BaseCharacter
{
    [SerializeField] private GameObject prefabText;

    [SerializeField] private AudioClip mineClip;
    
    [SerializeField] private int miningValue;

    [SerializeField] private IncomeIncreaser incomeIncreaser;
    
    private new void OnEnable()
    {
        prefabText.GetComponent<Text>().text = $"+{miningValue}";

        base.OnEnable();

        incomeIncreaser.AddIncome(miningValue);

        incomeIncreaser.gameObject.SetActive(false);
        incomeIncreaser.gameObject.SetActive(true);
    }
    
    public void GatherGold()
    {
        PlaySound(mineClip);
    }
}
