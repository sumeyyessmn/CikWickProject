using UnityEngine;

public class PlayerController : MonoBehaviour
{   [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpCooldown;

    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
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

        if(Input.GetKey(_jumpKey)&& _canJump && IsGrounded())
        {
            _canJump = false;
            //zıplama işlemi gerçekleşecek 
            SetPlayerJumping();
            Invoke(nameof(ResetJumping),_jumpCooldown);
        }
    }
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput +
        _orientationTransform.right * _horizontalInput;
        _playerRigidBody.AddForce(_movementDirection.normalized*_movementSpeed,ForceMode.Force);
    }
    private void SetPlayerJumping()
    {
        //y pozisyonunda hızımızı zıplamaya başlamadan önce sıfırlamam lazım zıplama bozulmasın diye
        _playerRigidBody.linearVelocity = new Vector3(_playerRigidBody.linearVelocity.x,0f, _playerRigidBody.linearVelocity.z);
        _playerRigidBody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position,Vector3.down,_playerHeight * 0.5f+0.2f,_groundLayer);
    }

}
