using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3f;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _PlayerAnimation;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _PlayerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
        _PlayerAnimation.SetFloat("Velocity", _moveInput.magnitude);
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 8) { //layer 8 == pushable
            other.gameObject.GetComponent<Pushable>().Push(_moveInput.normalized);
        }
    }
}
