using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	// Variables
	GameManager gameManager;
	// [SerializeField] float deathJump = 0f;
	[SerializeField] float minSpeed = 10f;
	[SerializeField] float maxSpeed = 15f;
	float actualSpeed;



	// Pre-built Functions
	void Awake()
	{
		gameManager = GameManager.Instance;
		actualSpeed = Random.Range(minSpeed, maxSpeed);
	}
	void FixedUpdate()
	{
		MoveEnemy();
		RemoveEnemy();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			gameManager.Die();
			// Time.timeScale = 0f;
			// Player player = collision.gameObject.GetComponent<Player>();
			// player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * deathJump, ForceMode2D.Impulse);
			// player.GetComponent<CapsuleCollider2D>().enabled = false;
		}
		else if (collision.gameObject.CompareTag("Enemy"))
			Destroy(gameObject);
	}



	// Custom Functions
	void MoveEnemy()
	{
		transform.position += Vector3.left * actualSpeed * Time.deltaTime;
	}
	void RemoveEnemy()
	{
		if (transform.position.x < gameManager.FindLeftEdgeOfCamera(actualSpeed * -1).x)
			Destroy(gameObject);
	}
}
