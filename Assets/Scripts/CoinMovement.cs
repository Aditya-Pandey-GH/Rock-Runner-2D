using UnityEngine;

public class CoinMovement : MonoBehaviour
{

	// Variables
	GameManager gameManager;
	AudioManager audioManager;
	[SerializeField] float actualSpeed = 10f;



	// Pre-built Functions
	void Awake()
	{
		gameManager = GameManager.instance;
		audioManager = AudioManager.instance;
	}
	void FixedUpdate()
	{
		MoveCoin();
		RemoveCoin();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			audioManager.PlayAudio("Coin");
			gameManager.ScoreIncrease(25f);
			Destroy(gameObject);
		}

	}



	// Custom Functions
	void MoveCoin()
	{
		transform.position += Vector3.left * actualSpeed * Time.deltaTime;
	}
	void RemoveCoin()
	{
		if (transform.position.x < gameManager.FindLeftEdgeOfCamera(actualSpeed * -1).x)
			Destroy(gameObject);
	}
}
