using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region References

    /// <summary>
    /// Reference to Player's CharacterMovement component, to be set from editor first time,
    /// and reassigned in run time for subsequent times.
    /// </summary>
    [SerializeField] private CharacterMovement _playerCharacterMovement;

    #endregion

    #region Methods

    /// <summary>
    /// Public method to allow CharacterMovement to register on InputManager so it can receive input.
    /// </summary>
    /// <param name="playerCharacterMovement">Player's CharacterMovement (Component to be registered)</param>
    public void RegisterPlayer(CharacterMovement playerCharacterMovement)
    {
        //TODO
    }

    #endregion

    /// <summary>
    /// UPDATE
    /// Receive Horizontal input from player, if any, and set it on Player's CharacterMovement
    /// Receive Vertical input from player, if any, and set it on Player's CharacterMovement
    /// Receive Jump input from player, if any, and call corresponding method on Player's CharacterMovement
    /// </summary>
    void Update()
    {
        //Recibimos el input
        //Duda 1: Esto no debería contar como StringTyping, porque son los únicos parámetros que admite el GetAxis.
        //La cosa es que se tienen que poner como string
        //Duda 2: ¿Deberíamos comprobar si hay input en estos casos? Es que si no hay, simplemente devuelven float 0.0f... 
        //Lo que significa que no recibirá movimiento en esa dirección
        _playerCharacterMovement.SetHorizontalInput(Input.GetAxis("Horizontal"));
        _playerCharacterMovement.SetVerticalInput(Input.GetAxis("Vertical"));

        //Detectamos salto, mismamente con el Espace
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _playerCharacterMovement.Jump();
        }
    }
}