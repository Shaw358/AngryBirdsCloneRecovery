// BlockDamage
using UnityEngine;

public class BlockDamage : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private GameObject deathParticles;

	[SerializeField]
	internal int currentHealth;

	[SerializeField]
	private Sprite[] damageLevel;

	[SerializeField]
	private AudioSource audioSource;

	private Rigidbody2D rb;

	private float vel;

	private int damage;

	private GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = base.gameObject.GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		currentHealth = 3;
		if (base.gameObject.name == "Pig")
		{
			currentHealth = 2;
		}
	}

	public void minusHealth(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			if (deathParticles != null)
			{
				Object.Instantiate(deathParticles, base.gameObject.transform.position, base.gameObject.transform.rotation);
			}
			if (base.gameObject.name == "Pig")
			{
				gameManager.score += 5000;
			}
			else
			{
				int num = Random.Range(250, 750);
				gameManager.score += num;
			}
			Object.Destroy(base.gameObject);
		}
		switch (currentHealth)
		{
		case 1:
			if (audioSource != null && deathParticles != null)
			{
				audioSource.Play();
				Object.Instantiate(deathParticles, base.gameObject.transform.position, base.gameObject.transform.rotation);
			}
			spriteRenderer.sprite = damageLevel[0];
			break;
		case 2:
			if (audioSource != null && deathParticles != null)
			{
				audioSource.Play();
				Object.Instantiate(deathParticles, base.gameObject.transform.position, base.gameObject.transform.rotation);
			}
			spriteRenderer.sprite = damageLevel[1];
			break;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		vel = rb.velocity.magnitude;
		damage = (int)vel;
		vel = rb.velocity.magnitude;
		damage = (int)vel;
		damage /= 3;
		if ((bool)collision.gameObject && vel > 1f)
		{
			Debug.Log(damage);
			if (damage > 3)
			{
				damage = 3;
			}
			minusHealth(damage);
		}
	}
}
