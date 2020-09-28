// BackgroundParallax
using System.Collections;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
	private Vector3 pos;

	public IEnumerator movebackground()
	{
		for (int i = 0; i < 80; i++)
		{
			yield return new WaitForSeconds(0.001f);
			base.transform.position += new Vector3(0.07f, 0f);
		}
		Move();
	}

	private void Move()
	{
		StartCoroutine(movebackgroundBack());
	}

	public IEnumerator movebackgroundBack()
	{
		yield return new WaitForSeconds(4.5f);
		for (int i = 0; i < 80; i++)
		{
			yield return new WaitForSeconds(0.001f);
			base.transform.position -= new Vector3(0.07f, 0f);
		}
	}
}
