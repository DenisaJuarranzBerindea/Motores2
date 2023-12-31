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
        if (other.GetComponent<CharacterController>() != null) // Sabemos que el Player y solo el Player debe tener un CharacterController
        {
            GameManager.Instance.ReleaseFlower();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// START
    /// Register Flower on GameManager
    /// </summary>
    void Start()
    {
        GameManager.Instance.RegisterFlower();
    }
}