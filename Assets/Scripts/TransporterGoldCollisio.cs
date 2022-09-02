using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterGoldCollisio : MonoBehaviour
{
    [SerializeField] private GameObject goldCart;

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
        goldCart.SetActive(true);
    }
}
