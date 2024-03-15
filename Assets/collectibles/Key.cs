using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, ICollectibles
{

    public GameObject charKey;

    public void Start()
    {
        charKey = GameObject.Find("Key");
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject Collectible = GetComponent<GameObject>();
        if (Collectible.CompareTag("Key"))
        {

        }
    }
}
