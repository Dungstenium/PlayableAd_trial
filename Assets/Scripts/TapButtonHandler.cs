using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class TapButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Button nextButton;

    [SerializeField] private List<GameObject> minersList = new List<GameObject>();
    [SerializeField] private Animator minerAnim;

    public float income = 30;

    private float phaseOneDuration;
    
    private void Start()
    {
        SetUpPhaseOne();
        
        phaseOneDuration = minerAnim.GetCurrentAnimatorClipInfo(0).Length * 3;
    }

    private void SetUpPhaseOne()
    {
        button.onClick.AddListener(() => GameManager.instance.glow.gameObject.SetActive(false));
        button.onClick.AddListener(() => StartCoroutine(AnimateMiner()));
        button.onClick.AddListener(() => StartCoroutine(HandleButton()));
        button.onClick.AddListener(() => StartCoroutine(FinishPhaseOne()));
    }

    private IEnumerator AnimateMiner()
    {
        minerAnim.SetBool("ShouldMine", true);

        yield return new WaitForSeconds(phaseOneDuration);
        
        minerAnim.SetBool("ShouldMine", false);
    }

    private IEnumerator HandleButton()
    {
        button.gameObject.SetActive(false);

        yield return new WaitForSeconds(phaseOneDuration);
        
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<Text>().text = "Hire more Miners!";
    }
    
    private IEnumerator FinishPhaseOne()
    {
        yield return new WaitForSeconds(phaseOneDuration);
        
        button.onClick.RemoveAllListeners();

        SetUpPhaseTwo();
    }

    private void SetUpPhaseTwo()
    {
        GameManager.instance.glow.gameObject.SetActive(true);
            
        AssignPhaseTwoEvents();

        GameManager.instance.LoadNextCamera();
    }

    private void AssignPhaseTwoEvents()
    {
        for (int i = 0; i < minersList.Count; i++)
        {
            var delay = (i + 1) * 0.5f;
            var miner = minersList[i];

            button.onClick.AddListener(() => StartCoroutine(ObjectActivation(miner, delay)));
        }

        button.onClick.AddListener(() => GameManager.instance.glow.gameObject.SetActive(false));

        button.onClick.AddListener(() => StartCoroutine(ObjectActivation(nextButton.gameObject, 3.0f)));

        button.onClick.AddListener(() => button.gameObject.SetActive(false));

        button.onClick.AddListener(() => StartCoroutine(FinishPhaseAfterTime(3.0f)));
    }

    private IEnumerator ObjectActivation(GameObject goToActivate,float delay)
    {
        yield return new WaitForSeconds(delay);
        
        goToActivate.SetActive(true);
    }

    private IEnumerator FinishPhaseAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        FinishPhaseTwo();
    }
    
    private void FinishPhaseTwo()
    {
        button.onClick.RemoveAllListeners();
        
        // GameManager.instance.LoadNextCamera();
    }
}
