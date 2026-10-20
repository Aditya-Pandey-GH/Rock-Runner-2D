using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	// Variables
	Rigidbody2D rb;
	Animator playerAnim;
	AudioManager audioManager;
	private bool isJumping = false;
	[SerializeField] float jumpSpeed = 10f;



	// Pre-built Functions
	void Awake()
	{
		audioManager = AudioManager.instance;
		rb = GetComponent<Rigidbody2D>();
		playerAnim = GetComponentInChildren<Animator>();
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			isJumping = false;
	}
	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			isJumping = true;
	}



	// Input Action Functions
	public void Jump(InputAction.CallbackContext context)
	{
		if (context.started)
			playerAnim.SetBool("isJumping", true);
		if (context.performed && !isJumping)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
			rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			audioManager.PlayAudio("Jump");
		}
		if (context.canceled)
			playerAnim.SetBool("isJumping", false);
	}
	public void Crouch(InputAction.CallbackContext context)
	{
		if (context.performed)
			playerAnim.SetBool("isCrouching", true);
		if (context.canceled)
			playerAnim.SetBool("isCrouching", false);
	}



	// Custom Functions
}
