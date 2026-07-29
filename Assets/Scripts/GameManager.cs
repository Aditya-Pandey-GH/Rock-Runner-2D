using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	// Variables
	public static GameManager Instance;
	[SerializeField] Player player;
	[SerializeField] TextMeshProUGUI scoreText;
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
		Instance = this;
		SetDefaultSettings(frameRate);
		GameLoad();
	}
	void FixedUpdate()
	{
		if (isGameRunning)
			ScoreIncrease();
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
		Vector2 left = FindLeftEdgeOfCamera(edgePhaseX);
		TeleportPlayer(left);
		ResetGame();
		playButton.SetActive(false);
		quitButton.SetActive(false);
		isGameRunning = true;
		Time.timeScale = 1f;
	}
	void ScoreIncrease()
	{
		score += 0.25f;
		scoreText.text = score.ToString("0");
	}
	public void Die()
	{
		Time.timeScale = 0f;
		isGameRunning = false;
		playButton.SetActive(true);
		quitButton.SetActive(true);
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
		ResetScore();
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
