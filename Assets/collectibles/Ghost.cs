using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, ICollectibles
{
    [SerializeField] GameObject charGhost;
    GameObject ghostCollectible;

    private void Start()
    {
        ghostCollectible = GetComponent<GameObject>();
    }

    public void OnTriggerEnter(Collider other)
    {
        charGhost.SetActive(true);
        ghostCollectible.SetActive(false);
    }
}
