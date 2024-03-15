using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, ICollectibles
{
    public GameObject charKatana;

    public void Start()
    {
        charKatana = GameObject.Find("Katana");
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject Collectible = GetComponent<GameObject>();
        if (Collectible.CompareTag("Katana"))
        {

        }
    }
}
