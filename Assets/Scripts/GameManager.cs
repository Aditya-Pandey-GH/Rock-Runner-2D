using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	// Variables
	public static GameManager instance;
	AudioManager audioManager;
	[SerializeField] EnemySpawner enemySpawn;
	[SerializeField] CoinSpawner coinSpawn;
	[SerializeField] Player player;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI gameAndName;
	[SerializeField] GameObject playButton;
	[SerializeField] GameObject quitButton;
	[SerializeField] int frameRate = -1;
	[SerializeField] float edgePhaseX = 1.5f;
	[SerializeField] float yAxisPos4Spawn = -2.45f;
	float score;
	bool isGameRunning;



	// Pre-built Functions
	void Awake()
	{
		instance = this;
		SetDefaultSettings(frameRate);
		audioManager = AudioManager.instance;
		GameLoad();
	}
	void FixedUpdate()
	{
		if (isGameRunning)
			ScoreIncrease(0.25f);
	}



	// Custom Functions
	void SetDefaultSettings(int frameRate)
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = frameRate;
	}
	void TeleportPlayer(Vector2 position)
	{
		player.transform.position = position;
	}
	public Vector2 FindLeftEdgeOfCamera(float edgePhaseX)
	{
		return new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + edgePhaseX, yAxisPos4Spawn);
	}
	public Vector2 FindRightEdgeOfCamera(float edgePhaseX)
	{
		return new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + edgePhaseX, yAxisPos4Spawn);
	}
	void GameLoad()
	{
		isGameRunning = false;
		Time.timeScale = 0f;
		score = scoreText.text == "" ? 0 : float.Parse(scoreText.text);
	}
	public void GameStart()
	{
		gameAndName.enabled = false;
		Vector2 left = FindLeftEdgeOfCamera(edgePhaseX);
		TeleportPlayer(left);
		ResetGame();
		playButton.SetActive(false);
		quitButton.SetActive(false);
		enemySpawn.enabled = true;
		coinSpawn.enabled = true;
		isGameRunning = true;
		Time.timeScale = 1f;
	}
	[HideInInspector]
	public void ScoreIncrease(float incScore)
	{
		score += incScore;
		scoreText.text = score.ToString("0");
	}
	public void Die()
	{
		audioManager.PlayAudio("Death");
		Time.timeScale = 0f;
		if (score > PlayerPrefs.GetFloat("highScore", 0))
			PlayerPrefs.SetFloat("highScore", score);
		isGameRunning = false;
		playButton.SetActive(true);
		quitButton.SetActive(true);
		enemySpawn.enabled = false;
		coinSpawn.enabled = false;
		PlayerPrefs.Save();
	}
	public void ResetScore()
	{
		score = 0f;
		scoreText.text = "0";
	}
	public void ResetGame()
	{
		Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
		foreach (Enemy enemy in enemies)
			Destroy(enemy.gameObject);
		CoinMovement[] coins = FindObjectsByType<CoinMovement>(FindObjectsSortMode.None);
		foreach (CoinMovement coin in coins)
			Destroy(coin.gameObject);
		ResetScore();
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
