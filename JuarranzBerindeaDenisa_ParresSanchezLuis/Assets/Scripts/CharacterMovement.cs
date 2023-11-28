using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    #region Paramaters

    /// <summary>
    /// Movement speed of the player. Needs to keep constant while the player moves
    /// </summary>
    /// Desired horizontal movement speed
    [SerializeField] private float _movementSpeed = 3.0f;

    /// <summary>
    /// Rotation speed of the player, smoothed with a Slerp
    /// </summary>
    /// Desired horizontal movement speed
    [SerializeField] private float _rotationSpeed = 10.0f;

    /// <summary>
    /// Vertical speed assigned to character when jump starts
    /// </summary>
    [SerializeField] private float _jumpSpeed = 20.0f;

    /// <summary>
    /// Minimum vertical speed to limitate falling speed
    /// </summary>
    [SerializeField] private float _minSpeed = -10.0f;

    #endregion

    #region References

    /// <summary>
    /// Reference to Player's character controller
    /// </summary>
    private CharacterController _myCharacterController;

    /// <summary>
    /// Reference to Player's Transform
    /// </summary>
    private Transform _myTransform;

    /// <summary>
    /// Reference to Camera's CameraController
    /// </summary>
    private CameraController _cameraController;

    #endregion

    #region Properties

    /// <summary>
    /// Horizontal axis input received from InputManager
    /// </summary>
    private float _xAxis;

    /// <summary>
    /// Vertical axis input received from InputManager
    /// </summary>
    private float _zAxis;

    /// <summary>
    /// Movement direction vector
    /// </summary>
    private Vector3 _movementDirection;

    /// <summary>
    /// Movement vertical speed (needs to be updated every frame due to gravity)
    /// </summary>
    private float _verticalSpeed; 

    #endregion

    #region Methods

    /// <summary>
    /// Public method to set the horizontal component of input. (Will be called from InputManager)
    /// </summary>
    /// <param name="x">Received horizontal component</param>
    public void SetHorizontalInput(float x)  
    {
        _xAxis = x;
    }

    /// <summary>
    /// Public method to set the vertical component of input. (Will be called from InputManager)
    /// </summary>
    /// <param name="y">Received vertical component</param>
    public void SetVerticalInput(float y)
    {
        _zAxis = y;
    }

    /// <summary>
    /// Public method called when the player tries to perform a new Jump. (Will be called from InputManager)
    /// If the Character is grounded, it overrides current value or _verticalSpeed with _jumpSpeed.
    /// Otherwise, the request to jump is ignored.
    /// </summary>
    public void Jump()
    {
        //Si está tocando suelo, y se solicita salto, la velocidad vertical será la del salto
        if (_myCharacterController.isGrounded) 
            _verticalSpeed = _jumpSpeed;
    }   
    #endregion

    /// <summary>
    /// START
    /// Needs to assign _myCharacterController, _myTransform and _cameraController.
    /// If InputManager is already assigned, it will also register the player on it.
    /// </summary>
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();

        _myTransform = transform;

        _cameraController = Camera.main.GetComponent<CameraController>();

        // Registramos al player en el input
        GameManager.Instance.Input.RegisterPlayer(this);
    }

    /// <summary>
    /// UPDATE
    /// Needs to calculate and normalize horizontal movement direction
    /// Needs to update vertical speed according to gravity
    /// Finally move the character according to desired _movementSpeed in horizontal and updated _verticalSpeed
    /// Final details:
    /// -Ensure the character looks in the desired direction according to move direction.
    /// -Ensure to set the vertical following behaviour for camera depending on whether the character is grounded or not.
    /// </summary>
    void Update()
    {
        //Dirección horizontal normalizado
        _movementDirection = new Vector3(_xAxis, 0, _zAxis).normalized;

        //Velocidad vertical clampeada cuando se encuentra en el aire
        if (!_myCharacterController.isGrounded)
            _verticalSpeed = Mathf.Clamp(_verticalSpeed + Physics.gravity.y * Time.deltaTime, _minSpeed, _jumpSpeed);

        //Vector de movimiento (parte horizontal + parte vertical)
        Vector3 movementVector = _movementSpeed * _movementDirection + _verticalSpeed * Vector3.up;

        _myCharacterController.Move(movementVector * Time.deltaTime);

        // Velocidad vertical por defecto si se encuentra en el suelo
        if (_myCharacterController.isGrounded) _verticalSpeed = -1f;

        // Rotación del personaje igualando su eje local forward a su dirección de movimiento.
        // Usamos un Slerp para suavizar la rotación
        if (_movementDirection != Vector3.zero) 
            _myTransform.forward = Vector3.Slerp(_myTransform.forward, _movementDirection, Time.deltaTime * _rotationSpeed);

        // Seteamos la cámara para que siga al personaje verticalmente solo si está en el suelo.
        _cameraController.SetVerticalFollow(_myCharacterController.isGrounded);
    }
}