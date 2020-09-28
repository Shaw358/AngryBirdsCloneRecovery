// CameraMovement
using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public IEnumerator moveCamera()
	{
		for (int i = 0; i < 80; i++)
		{
			yield return new WaitForSeconds(0.001f);
			base.transform.position += new Vector3(0.19f, 0f);
		}
		yeet();
	}

	private void yeet()
	{
		StartCoroutine(moveCameraBack());
	}

	public IEnumerator moveCameraBack()
	{
		yield return new WaitForSeconds(4.5f);
		for (int i = 0; i < 80; i++)
		{
			yield return new WaitForSeconds(0.001f);
			base.transform.position -= new Vector3(0.19f, 0f);
		}
	}
}
