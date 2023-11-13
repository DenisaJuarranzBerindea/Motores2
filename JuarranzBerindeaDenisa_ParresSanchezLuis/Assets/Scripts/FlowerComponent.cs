using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlowerComponent : MonoBehaviour
{
    /// <summary>
    /// Evaluates if colliding object corresponds to player.
    /// If it does, the Flower is released on GameManager and own object is deactivated.
    /// </summary>
    /// <param name="other">Collider of colliding object</param>
    private void OnTriggerEnter(Collider other)
    {
        //TODO
    }

    /// <summary>
    /// START
    /// Register Flower on GameManager
    /// </summary>
    void Start()
    {
        //TODO
    }
}