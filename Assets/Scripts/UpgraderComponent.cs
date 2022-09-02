using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgraderComponent : MonoBehaviour
{
    private int upgradeLevel = 0;

    [SerializeField] private IncomeIncreaser incomeIncreaser;
    [SerializeField] private SpriteRenderer[] corridorRenderer;
    [SerializeField] private SpriteRenderer boxRenderer;
    [SerializeField] private SpriteRenderer goldRenderer;
    [SerializeField] private GameObject[] newMiners;
    [SerializeField] private GameObject[] oldMiners;
    
    [SerializeField] private Sprite[] corridorSpriteVersion;
    [SerializeField] private Sprite[] boxSpriteVersion;
    [SerializeField] private string[] buttonTextList;

    private Button button;

    public event Action OnLastUpgradeEnd;
    
    private bool canBeClickedAgain = true;
    
    [SerializeField] private float rotationMultiplier;
    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;

    private Camera cam;
    private Vector3 camInitialPos;
    private Quaternion camInitialRot;

    private UpgradeArrowComponent arrow;

    [SerializeField] private AudioClip upgradeClip;

    void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(Upgrade);
        
        OnLastUpgradeEnd += GameManager.instance.StartPhaseFour;
        OnLastUpgradeEnd += DeactivateObject;

        cam = Camera.main;

        arrow = FindObjectOfType<UpgradeArrowComponent>(true);
    }

    public void Upgrade()
    {
        if (canBeClickedAgain == false) return;
        
        ++upgradeLevel;

        StartCoroutine(Shake());
        StartCoroutine(ManipulateButtonActivation());
        
        arrow.gameObject.SetActive(false);
        arrow.gameObject.SetActive(true);
        
        PlaySound();

        SwapSprites();

        if(upgradeLevel < boxSpriteVersion.Length)
        {
            boxRenderer.sprite = boxSpriteVersion[upgradeLevel -1];
        }
        else
        {
            goldRenderer.transform.localPosition += new Vector3(0, 0.32f, 0);
        }

        SwapText();

        incomeIncreaser.AddIncome(upgradeLevel * 300);
        incomeIncreaser.gameObject.SetActive(false);
        incomeIncreaser.gameObject.SetActive(true);
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(upgradeClip, GameManager.instance.transform.position, 0.6f);
    }

    private IEnumerator Shake()
    {
        var camTransf = cam.transform;
        camInitialPos = camTransf.localPosition;
        camInitialRot = camTransf.localRotation;
        
        float counter = 0;
        float shakeLength = 0.2f * upgradeLevel;
        float shakePower = 0.02f;
        
        StartShake(shakeLength, shakePower);

        while (counter < shakeLength)
        {
            ShakeScreen();
            counter += Time.deltaTime;
            yield return null;
        }

        camTransf.localPosition = camInitialPos;
        camTransf.localRotation = camInitialRot;
    }
   
    private void StartShake(float length, float power)
    {
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;
    }
    
    private void ShakeScreen()
    {
        float xAmount = Random.Range(-1f, 1f) * shakePower;
        float yAmount = Random.Range(-1f, 1f) * shakePower;

        shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

        cam.transform.position += new Vector3(xAmount, yAmount, 0f);
        
        shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        
        cam.transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation* Random.Range(-1f, 1f));
    }
    
    private IEnumerator ManipulateButtonActivation()
    {
        canBeClickedAgain = false;

        yield return new WaitForSeconds(0.1f * upgradeLevel);

        canBeClickedAgain = true;
        
        if (upgradeLevel == corridorSpriteVersion.Length - 1)
        {
            OnLastUpgradeEnd?.Invoke();
        }
    }

    private void DeactivateObject()
    {
        button.onClick.RemoveListener(Upgrade);
        
        StopAllCoroutines();
        cam.transform.localPosition = camInitialPos;
        cam.transform.localRotation = camInitialRot;
        
        gameObject.SetActive(false);
    }

    private void SwapSprites()
    {
        foreach (var renderer in corridorRenderer)
        {
            renderer.sprite = corridorSpriteVersion[upgradeLevel];
        }
        
        if (upgradeLevel == corridorSpriteVersion.Length - 1)
        {
            SwapMinersSprites();
        }
    }

    private void SwapMinersSprites()
    {
        if (newMiners.Length != oldMiners.Length)
        {
            Debug.LogError($"oldMiners and newMiners dont have the same numbers", this);
            return;
        }

        for (int i = 0; i < newMiners.Length; i++)
        {
            newMiners[i].SetActive(true);
            oldMiners[i].SetActive(false);
        }
    }
    
    private void SwapText()
    {
        button.GetComponentInChildren<Text>().text = buttonTextList[upgradeLevel];
    }
}
