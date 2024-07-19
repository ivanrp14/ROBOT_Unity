using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f, initialSpeed;
    public float jumpForce = 10f;  // Fuerza del salto
    public float bounds = 4.5f;
    public float horizontalSpeed = 10f;  // Velocidad del movimiento horizontal
    public float horizontalSensitivity = 1f;  // Sensibilidad del movimiento horizontal
    private Rigidbody rb;
    public bool isJumping, isGrounded, isBall;
    private SphereCollider sphereCollider;
    private BoxCollider boxCollider;
    public UnityEvent OnFinish;
    private float horizontalMove = 0f; // Movimiento horizontal en proceso
    public float posY;

    void Start()
    {
        initialSpeed = forwardSpeed;
        sphereCollider = GetComponent<SphereCollider>();
        boxCollider = GetComponent<BoxCollider>();
        sphereCollider.enabled = false;
        boxCollider.enabled = true; // Inicialmente el jugador es una caja
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        GetStats();
        MoveForward();
        posY = rb.velocity.y;

        // Mover horizontalmente si hay movimiento pendiente
        if (Mathf.Abs(horizontalMove) > 0.1f)
        {
            MoveHorizontally();
        }
    }

    private void GetStats()
    {
        forwardSpeed = GameManager.Instance.stats.ForwardSpeed;
        jumpForce = GameManager.Instance.stats.JumpForce;
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void StartHorizontalMove(float delta)
    {
        horizontalMove = delta * horizontalSensitivity;
    }

    void MoveHorizontally()
    {
        float newX = Mathf.Clamp(transform.position.x + horizontalMove * Time.deltaTime * horizontalSpeed, -bounds, bounds);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Reset horizontal move after applying it
        horizontalMove = 0f;
    }

    public void TransformAction()
    {
        Debug.Log("TransformAction");
        isBall = !isBall;
        if (isBall)
        {
            sphereCollider.enabled = true;
            boxCollider.enabled = false;
        }
        else
        {
            sphereCollider.enabled = false;
            boxCollider.enabled = true;
        }
    }

    public void Jump()
    {
        Debug.Log("Jump");
        if (!isGrounded || isBall || Mathf.Abs(horizontalMove) > 0.1f) // No saltar si está moviéndose horizontalmente
        {
            return;
        }
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
        isGrounded = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            OnFinish?.Invoke();
        }
    }


}
