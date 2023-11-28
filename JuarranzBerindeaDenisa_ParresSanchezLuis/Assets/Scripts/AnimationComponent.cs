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

    /// <summary>
    /// Tiempo mínimo a esperar antes de iniciar la animación de Idle cuando la velocity es 0.
    /// </summary>
    private float _minTime = 0.05f;

    /// <summary>
    /// Contador de tiempo hasta alcanzar _minTime
    /// </summary>
    private float _timer;

    #endregion

    /// <summary>
    /// START
    /// Assign _myCharacterController and _myAnimator
    /// Check if both are correct or disable component
    /// </summary>
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myAnimator = GetComponent<Animator>(); 

        if (_myCharacterController == null ||
            _myAnimator == null ||
            _myCharacterController.enabled == false ||
            _myAnimator.enabled == false)
        {
            enabled = false;
        }
    }

    /// <summary>
    /// UPDATE
    /// Evaluate _myCharacterController velocity
    /// Assign the right animation according to this using integer parameter "AnimState"
    /// </summary>
    void Update()
    {
        //Debug.Log("(" + _myCharacterController.velocity.x + " , ");
        //              + _myCharacterController.velocity.y + " , "
        //              + _myCharacterController.velocity.z + ")");

        if (!_myCharacterController.isGrounded)
        {
            _myAnimator.SetInteger(_animationState, 2); //Estado Jump
            if (_timer > 0) { _timer = 0; }
        }
        else if (Mathf.Abs(_myCharacterController.velocity.x) > 0.1 || Mathf.Abs(_myCharacterController.velocity.z) > 0.1)
        {
            _myAnimator.SetInteger(_animationState, 1); //Estado Move
            if (_timer > 0) { _timer = 0; }
        }       
        else
        {
            //Si ya está en Idle no cuenta.
            if (_myAnimator.GetInteger(_animationState) != 0)
            {
                _timer += Time.deltaTime;
            }

            if (_timer >= _minTime)
            {
                _myAnimator.SetInteger(_animationState, 0); //Estado Idle
                _timer = 0;
            }
        }
    }
}