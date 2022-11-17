using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;

    Vector2 movement;
    public VectorValue startingPosition;

    private void Start()
    {
        transform.position = startingPosition.intitalValue;//Puts the icon in the correct place when the scene loads
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale != 1f)
        {
            animator.SetBool("Paused", true);
        }
        else
        {
            animator.SetBool("Paused", false);
        }
        movement.x = Input.GetAxisRaw("Horizontal");//Input
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0.01f)//Records the last direction of movement to play the correct idle animation
        {
            animator.SetInteger("LastDirection", 0);
        }
        else if (movement.x < -0.01f)
        {
            animator.SetInteger("LastDirection", 1);
        }
        else if (movement.x == 0f && movement.y > 0.01)
        {
            animator.SetInteger("LastDirection", 2);
        }
        else if (movement.x == 0f && movement.y < -0.01)
        {
            animator.SetInteger("LastDirection", 3);
        }
    }

    // Using fixed update so walking is consistent no matter the framerate
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime); // Makes the icon move in the chosen direction at the correct speed
    }
}
