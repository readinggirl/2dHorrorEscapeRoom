using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _playerAnimation;
    [SerializeField] private int keyPickupCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
        _playerAnimation.SetFloat("Velocity", _moveInput.magnitude);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            //layer 8 == pushable
            other.gameObject.GetComponent<PushableTall>().Push(_moveInput.normalized);
        }

        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            keyPickupCount++;
            
        }

        if (other.gameObject.CompareTag("Door") && keyPickupCount == 2)
        {
            //todo: make win scene
            SceneManager.LoadScene("GameOver");
        }
    }
}