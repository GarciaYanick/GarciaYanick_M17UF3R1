using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ICollectibles
{
    [SerializeField] GameObject charGhost;
    [SerializeField]GameObject ghostCollectible;

    public void OnTriggerEnter(Collider col)
    {
        charGhost.SetActive(true);
        ghostCollectible.SetActive(false);
    }
}
