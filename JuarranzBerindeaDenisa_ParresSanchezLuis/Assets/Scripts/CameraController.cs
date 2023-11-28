using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    #region Parameters

    /// <summary>
    /// Horizonal distance from Camera to CameraTarget.
    /// </summary>
    [SerializeField] private float _horizontalOffset = 1.0f;

    /// <summary>
    /// Vertical distance from Camera to CameraTarget.
    /// </summary>
    [SerializeField] private float _verticalOffset = 1.0f;

    /// <summary>
    /// Pitch rotation for Camera.
    /// </summary>
    [SerializeField] private float _pitchRotationOffset = 1.0f;


    /// <summary>
    /// Multiplier factor to regulate camera responsiveness to target's movement.
    /// </summary>
    [SerializeField] private float _followFactor = 1.0f;

    #endregion

    #region References

    /// <summary>
    /// Camera target transform. Actually, the one the camera needs to follow.
    /// </summary>
    [SerializeField] private Transform _targetTransform;

    /// <summary>
    /// Reference to own transform.
    /// </summary>
    private Transform _myTransform;

    #endregion

    #region Properties

    /// <summary>
    /// If disabled, the camera does not follow target in vertical axis and keeps its own Y coordinate.
    /// </summary>
    private bool _yFollowEnabled = true;

    /// <summary>
    /// Stores own previous position's Y coordinate, to be able to keep it in case vertical following is disabled.
    /// </summary>
    private float _yPreviousFrameValue;

    #endregion

    #region Methods

    /// <summary>
    /// Public methods to allow others to set vertical following behaviour
    /// </summary>
    /// <param name="verticalFollowEnabled"></param>
    public void SetVerticalFollow(bool verticalFollowEnabled)
    {
        _yFollowEnabled = verticalFollowEnabled;
    }

    #endregion

    /// <summary>
    /// START
    /// Needs to assign _myTransform and initialize _yPreviousFrameValue
    /// </summary>
    void Start()
    {
        _myTransform = transform;
        _yPreviousFrameValue = _targetTransform.position.y + _verticalOffset;

        //Vector3 rotationOffset = new Vector3(_pitchRotationOffset, 0, 0); // PREGUNTAR - Basta con ponerle rotación a la propia cámara
        //_myTransform.eulerAngles = rotationOffset;
    }

    /// <summary>
    /// LATE UPDATE
    /// Needs to calculate the desired position for the camera.
    /// This calculation will differ depending on _yFollowEnabled.
    /// Once calculated, the new camera position can be assigned accorging to it, in a smoothed way.
    /// </summary>
    void LateUpdate()
    {
        Vector3 movementOffset = new Vector3(0, _verticalOffset, _horizontalOffset);

        // Calculamos la posición de la camara suavizada respecto al player
        _myTransform.position = Vector3.Lerp(_myTransform.position, _targetTransform.position + movementOffset, _followFactor * Time.deltaTime);

        if (_yFollowEnabled)
        {
            _yPreviousFrameValue = _myTransform.position.y;
        }
        else
        {
            // Si player !onGround, seteamos la componente "y" de la cámara a su última posición onGround.
            _myTransform.position = new Vector3(_myTransform.position.x, _yPreviousFrameValue, _myTransform.position.z);
        }
    }
}