using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ICollectibles
{
    //This trigger enables the gameobject attached to the character.
    public void OnTriggerEnter(Collider col);
}
