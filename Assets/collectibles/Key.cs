using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, ICollectibles
{
    [SerializeField] GameObject charKey;

    public void OnTriggerEnter(Collider col)
    {
        charKey.SetActive(true);
        gameObject.SetActive(false);
    }
}
