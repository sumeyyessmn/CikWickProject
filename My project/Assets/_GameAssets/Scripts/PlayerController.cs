using UnityEngine;

public class PlayerController : MonoBehaviour
{   [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    private Rigidbody _playerRigidBody;
    private float _horizontalInput, _verticalInput; 
    private Vector3 _movementDirection;

    void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerRigidBody.freezeRotation = true;
    }
    void Update()
    {
        SetInputs();
    }
    void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput +
        _orientationTransform.right * _horizontalInput;
        _playerRigidBody.AddForce(_movementDirection*_movementSpeed,ForceMode.Force);
    }

}
