using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, ICollectibles
{
    [SerializeField] GameObject charKatana;

    public void OnTriggerEnter(Collider col)
    {
        charKatana.SetActive(true);
    }
}
