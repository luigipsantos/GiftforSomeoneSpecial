using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody rb;

    Vector3 movement;

    public bool isGrounded;
    public bool canDoubleJump;

    public AudioSource jumpSound;
    public AudioSource crashSound;

    public Transform cam;
    Vector2 input;

    public float minRotation = -30;
    public float maxRotation = 60;

    public static bool gameIsPaused;

    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.x = ConvertToAngle180(currentRotation.x);
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(currentRotation);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        rb.position += (camF * input.y + camR * input.x) * Time.deltaTime * moveSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed * 1.5f;

            if (moveSpeed > 7.5f)
                moveSpeed = moveSpeed / 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed / 1.5f;
        }

        if (isGrounded == true)
        {
            canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpSound.pitch = 1.0f;
                jumpSound.Play();
                rb.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump == true)
            {
                jumpSound.pitch = 1.5f;
                jumpSound.Play();
                rb.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);

                canDoubleJump = false;
            }
        }

        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Animations
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("isRight", true);
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRight", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("isLeft", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("isLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("isMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isMoving", false);
        }
    }

    public static float ConvertToAngle180(float input)
    {
        while (input > 360)
        {
            input = input - 360;
        }
        while (input < -360)
        {
            input = input + 360;
        }
        if (input > 180)
        {
            input = input - 360;
        }
        if (input < -180)
            input = 360 + input;
        return input;
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            crashSound.pitch = 1.5f;
            crashSound.Play();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            crashSound.pitch = 1.0f;
            crashSound.Play();
        }

    }
}
