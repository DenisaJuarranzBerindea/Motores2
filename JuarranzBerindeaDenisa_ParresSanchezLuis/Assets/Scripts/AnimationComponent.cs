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
    /// Check if both are correct or disable component  (PREGUNTAR, ¿cómo que si están correctos? ¿Y si no lo están, lo desactivo?)
    /// </summary>
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myAnimator = GetComponent<Animator>(); 
    }

    /// <summary>
    /// UPDATE
    /// Evaluate _myCharacterController velocity
    /// Assign the right animation according to this using integer parameter "AnimState"
    /// </summary>
    void Update()
    {
        //Debug.Log("(" + _myCharacterController.velocity.x + " , "
        //              + _myCharacterController.velocity.y + " , "
        //              + _myCharacterController.velocity.z + ")");

        if (_myCharacterController.velocity.x != 0 || _myCharacterController.velocity.z != 0) 
        {
            _myAnimator.SetInteger(_animationState, 1); //Estado Move
        }
        else if (_myCharacterController.velocity.y != 0)  
        {
            _myAnimator.SetInteger(_animationState, 2); //Estado Jump
        }
        else
        {
            _myAnimator.SetInteger(_animationState, 0); //Estado Idle
        }
    }
}