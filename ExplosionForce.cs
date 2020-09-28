// ExplosionForce
using System.Collections;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
	[SerializeField]
	private float radius = 3f;

	[SerializeField]
	private float power = 5f;

	[SerializeField]
	private LayerMask obstacleLayer;

	[SerializeField]
	private LayerMask groundLayer;

	[SerializeField]
	private AudioSource explosieAudio;

	[SerializeField]
	private AudioClip explosie;

	[SerializeField]
	private AudioClip explosieTimer;

	[SerializeField]
	private bool canExplode = true;

	[SerializeField]
	private bool playTimer = true;

	[SerializeField]
	private bool startTimer = true;

	private Collider2D[] colliders;

	private Vector2 explosionPos = new Vector2(0f, 0f);

	private Rigidbody2D rb;

	[SerializeField]
	private Rigidbody2D playerRb;

	private BlockDamage blockHealth;

	[SerializeField]
	private CameraMovement cameraMovement;

	[SerializeField]
	private BackgroundParallax backGroundParallax;

	private void Awake()
	{
		playerRb = GetComponent<Rigidbody2D>();
		cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		backGroundParallax = GameObject.Find("Background").GetComponent<BackgroundParallax>();
	}

	private void Start()
	{
		power = 4.5f;
		radius = 3f;
		playerRb.AddForce(new Vector2(300f * playerRb.mass, 45f * playerRb.mass));
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		colliders = Physics2D.OverlapCircleAll(position, radius, obstacleLayer);
		if (Input.GetKeyDown("space") && canExplode)
		{
			playTimer = false;
			StartCoroutine(ExplosionTimer());
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.layer == 8 && startTimer) || (collision.gameObject.layer == 9 && startTimer))
		{
			StartCoroutine(ExplosionTimer());
		}
	}

	private IEnumerator ExplosionTimer()
	{
		if (playTimer)
		{
			startTimer = false;
			explosieAudio.clip = explosieTimer;
			explosieAudio.Play();
			yield return new WaitForSeconds(explosieAudio.clip.length);
			playTimer = false;
		}
		else
		{
			yield return new WaitForSeconds(0f);
		}
		Collider2D[] array = colliders;
		foreach (Collider2D collider2D in array)
		{
			rb = collider2D.GetComponent<Rigidbody2D>();
			AddExplosionForce(rb, power, base.transform.position, radius);
		}
		canExplode = false;
	}

	private void AddExplosionForce(Rigidbody2D rb, float explosionForce, Vector2 explodingPosition, float radius, float upwardsModifier = 0f, ForceMode2D mode = ForceMode2D.Impulse)
	{
		BlockDamage component = rb.GetComponent<BlockDamage>();
		if (canExplode)
		{
			explosieAudio.clip = explosie;
			Vector2 a = rb.position - explodingPosition;
			float magnitude = a.magnitude;
			explosieAudio.Play();
			if (magnitude < radius / 2f)
			{
				component.minusHealth(3);
			}
			else
			{
				if (magnitude > radius / 3f && magnitude < radius / 2f)
				{
					component.minusHealth(2);
				}
				if (magnitude > radius && magnitude < radius / 3f)
				{
					component.minusHealth(1);
				}
			}
			if (upwardsModifier == 0f)
			{
				a /= magnitude;
			}
			else
			{
				a.Normalize();
			}
			rb.AddForce(Mathf.Lerp(0f, explosionForce, radius / magnitude) * a, mode);
		}
		Object.Destroy(base.gameObject);
	}
}
