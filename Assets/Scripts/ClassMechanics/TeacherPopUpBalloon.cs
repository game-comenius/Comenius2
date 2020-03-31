using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherPopUpBalloon : MonoBehaviour
{
	private TeacherScript teacher;

	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private GameObject balloonContent;

	public void ShowBalloon()
	{
		spriteRenderer.enabled = true;
		balloonContent.SetActive(true);
		// Posicionar o sprite logo acima do sprite do npcDialogo
		var npcDialogoSpriteRenderer = teacher.GetComponent<SpriteRenderer>();
		var npcSpriteHeight = npcDialogoSpriteRenderer.bounds.extents.y;
		var mySpriteHeight = spriteRenderer.bounds.extents.y;
		transform.localPosition = new Vector3(0, npcSpriteHeight + mySpriteHeight);
	}

	public void HideBalloon()
	{
		balloonContent.SetActive(false);
		spriteRenderer.enabled = false;
	}

	private void Awake()
	{
		teacher = GetComponentInParent<TeacherScript>();
		if (!teacher)
		{
			Debug.LogError("O componente do tipo " + this.GetType() +
			"deve ser filho de um componente do tipo " + typeof(TeacherScript));
		}

		spriteRenderer = GetComponent<SpriteRenderer>();

        // O balão começa escondido
        HideBalloon();
	}
}
