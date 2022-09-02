using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : BaseCharacter
{

    [SerializeField] private GameObject goldCart;
    [SerializeField] private Transform targetTransf;
    [SerializeField] private SpriteRenderer spriteRend;

    void Start()
    {
        
    }

    private IEnumerator MoveTo()
    {
        yield return new WaitForSeconds(1.0f);

        while(Vector3.Distance(transform.position, targetTransf.position) > 0.04f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransf.position, 2 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(MoveTo());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        goldCart.gameObject.SetActive(true);
        Debug.Log("aaaaa");
    }
}
