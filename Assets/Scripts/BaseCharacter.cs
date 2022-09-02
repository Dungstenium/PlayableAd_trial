using System.Collections;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] private AudioClip spawnClip;

    public virtual void OnEnable()
    {
        StartCoroutine(FallDownSpawnEffect());
        StartCoroutine(EnlargeEffect());

        PlaySound(spawnClip);
    }

    protected virtual IEnumerator EnlargeEffect()
    {
        float speed = 2;
        Vector3 initialScale = transform.localScale;
        transform.localScale = Vector3.zero;

        while (Vector3.Distance(transform.localScale, initialScale) > 0.02f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, initialScale, speed * Time.deltaTime);
            
            yield return null;
        }
    }

    protected virtual IEnumerator FallDownSpawnEffect()
    {
        float counter = 0;
        while (counter <= 0.3f)
        {
            counter += Time.deltaTime;
            
            Vector3 transformPosition = transform.position;
            transformPosition.y -= Time.deltaTime;
            transform.position = transformPosition;
            
            yield return null;
        }
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
