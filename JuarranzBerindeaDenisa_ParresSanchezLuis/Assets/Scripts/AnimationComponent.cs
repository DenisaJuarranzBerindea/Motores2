using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationComponent : MonoBehaviour
{
    #region References

    /// <summary>
    /// Reference to player's Character Controller.
    /// Needs to be assigned on Start
    /// </summary>
    private CharacterController _myCharacterController;

    /// <summary>
    /// Reference to player's Animator.
    /// Needs to be assigned on Start.
    /// </summary>
    private Animator _myAnimator;

    #endregion

    #region Properties

    /// <summary>
    /// Stores the string, to avoid errors because of stringTyping
    /// </summary>
    private string _animationState = "AnimState";

    #endregion

    /// <summary>
    /// START
    /// Assign _myCharacterController and _myAnimator
    /// Check if both are correct or disable component  (PREGUNTAR, ¿cómo que si están correctos? ¿Y si no lo están, lo desactivo?) - ==null
    /// </summary>
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myAnimator = GetComponent<Animator>(); 

        if (_myCharacterController == null || _myAnimator == null)
        {
            enabled = false; //(?No se desactiva el componente)
        }
    }

    /// <summary>
    /// UPDATE
    /// Evaluate _myCharacterController velocity
    /// Assign the right animation according to this using integer parameter "AnimState"
    /// </summary>
    /// //PREGUNTAR:    isGrounded para el salto - Guay
    ///                 flechas grises animator - Dejarlo así
    ///                 animacion idle al cambio de sentido - Hay que echarle un ojo
    void Update()
    {
        //Debug.Log("(" + _myCharacterController.velocity.x + " , "
        //              + _myCharacterController.velocity.y + " , "
        //              + _myCharacterController.velocity.z + ")");

        if (!_myCharacterController.isGrounded)
        {
            _myAnimator.SetInteger(_animationState, 2); //Estado Jump
        }
        else if (Mathf.Abs(_myCharacterController.velocity.x) > 0.1 || Mathf.Abs(_myCharacterController.velocity.z) > 0.1)
        {
            _myAnimator.SetInteger(_animationState, 1); //Estado Move
        }       
        else
        {
            _myAnimator.SetInteger(_animationState, 0); //Estado Idle
        }
    }
}