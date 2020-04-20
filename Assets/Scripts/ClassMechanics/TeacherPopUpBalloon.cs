using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherPopUpBalloon : MonoBehaviour
{
	private TeacherScript teacher;

	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private GameObject balloonContent;

    [SerializeField]
    private SpriteRenderer rendererMidiaMencionada;

	public void ShowBalloon(ItemName midiaMencionada)
	{
		spriteRenderer.enabled = true;

        // Apresentar conteúdo do balão e fazer com que a mídia ocupe o espaço
        // aproximado do interior do balão
		balloonContent.SetActive(true);
        rendererMidiaMencionada.sprite = ItemSpriteDatabase.GetSpriteOf(midiaMencionada);
        var balloonBounds = spriteRenderer.bounds;
        var itemBounds = rendererMidiaMencionada.sprite.bounds;
        var factor = 0.7f * Math.Max((balloonBounds.size.x / itemBounds.size.x), (balloonBounds.size.y / itemBounds.size.y));
        rendererMidiaMencionada.transform.localScale = new Vector3(factor, factor, factor);
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
