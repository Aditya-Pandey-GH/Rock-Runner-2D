using UnityEngine;

public class BGParallax : MonoBehaviour
{

	// Variables
	private MeshRenderer parallaxObject;
	[SerializeField] float parallaxSpeed = 0f;
	private float offsetX = 0f;



	// Pre-built Functions
	void Awake()
	{
		parallaxObject = GetComponent<MeshRenderer>();
	}
	void FixedUpdate()
	{
		parallaxEffect();
	}



	// Custom Functions
	void parallaxEffect()
	{
		offsetX += parallaxSpeed * Time.deltaTime;
		offsetX %= 80f;

		parallaxObject.material.mainTextureOffset = new Vector2(offsetX, 0f);

	}
}
