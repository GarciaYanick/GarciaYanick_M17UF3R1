using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ICollectibles
{
    [SerializeField] GameObject charGhost;

    public void OnTriggerEnter(Collider col)
    {
        charGhost.SetActive(true);
        gameObject.SetActive(false);
    }
}
