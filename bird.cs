// bird
using System.Collections;
using UnityEngine;

public class bird : MonoBehaviour
{
	private SlingShotLine slingShotLine;

	[SerializeField]
	private bool isPressed;

	private bool isReleased;

	[SerializeField]
	private AudioSource slingSoundEffect;

	[SerializeField]
	private AudioSource looseSoundEffect;

	private float releaseDelay;

	private float maxDragDistance = 2f;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private SpringJoint2D sj;

	[SerializeField]
	private Rigidbody2D slingRb;

	[SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private GameObject particleSystem;

	[SerializeField]
	private CameraMovement cameraMovement;

	[SerializeField]
	private BackgroundParallax backGroundParallax;

	private float vel;

	private int damage;

	private bool isBlackBird;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private ParticleSystem particleSystemTracer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		slingShotLine = GameObject.Find("SlingshotFront").GetComponent<SlingShotLine>();
		sj = GetComponent<SpringJoint2D>();
		sj.connectedBody = GameObject.Find("CentrePoint").GetComponent<Rigidbody2D>();
		cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		backGroundParallax = GameObject.Find("Background").GetComponent<BackgroundParallax>();
		slingRb = sj.connectedBody;
	}

	private void Start()
	{
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		releaseDelay = 1f / (sj.frequency * 4f);
		isPressed = false;
		isReleased = false;
		particleSystemTracer.Stop();
	}

	private void Update()
	{
		vel = rb.velocity.magnitude;
		damage = (int)vel;
		if (isPressed)
		{
			DragBall();
		}
		vel = rb.velocity.magnitude;
		damage = (int)vel;
		damage /= 3;
		if (damage > 3)
		{
			damage = 3;
		}
	}

	private void DragBall()
	{
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Vector2.Distance(vector, slingRb.position) > maxDragDistance)
		{
			Vector2 normalized = (vector - slingRb.position).normalized;
			rb.position = slingRb.position + normalized * maxDragDistance;
		}
		else
		{
			rb.position = vector;
		}
	}

	private void OnMouseDown()
	{
		slingSoundEffect.Play();
		rb.constraints = RigidbodyConstraints2D.None;
		isPressed = true;
		rb.isKinematic = true;
	}

	private void OnMouseUp()
	{
		isReleased = true;
		isPressed = false;
		StartCoroutine(Release());
		rb.isKinematic = false;
	}

	private IEnumerator Release()
	{
		looseSoundEffect.Play();
		yield return new WaitForSeconds(releaseDelay);
		sj.enabled = false;
		gameManager.setBool(set: true);
		slingShotLine.setLineRendererActive(active: false);
		particleSystemTracer.Play();
		StartCoroutine(cameraMovement.moveCamera());
		StartCoroutine(backGroundParallax.movebackground());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Destructable" && isReleased)
		{
			StartCoroutine(spriteChange());
			Object.Instantiate(particleSystem, base.gameObject.transform.position, base.gameObject.transform.rotation);
			BlockDamage component = collision.gameObject.GetComponent<BlockDamage>();
			component.minusHealth(damage);
			component.minusHealth(1);
		}
	}

	private IEnumerator spriteChange()
	{
		spriteRenderer.sprite = sprites[0];
		yield return new WaitForSeconds(0.5f);
		spriteRenderer.sprite = sprites[1];
		yield return new WaitForSeconds(0.5f);
		spriteRenderer.sprite = sprites[0];
		yield return new WaitForSeconds(0.5f);
		spriteRenderer.sprite = sprites[1];
		yield return new WaitForSeconds(0.5f);
	}
}
