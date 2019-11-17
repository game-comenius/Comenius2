using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotProcedimento : MonoBehaviour {

    [SerializeField]
    private Procedimento procedimento;
    public Procedimento Procedimento
    {
        get
        {
            return procedimento;
        }

        set
        {
            procedimento = value;
            UpdateSprite();
        }
    }

    private Image midiaImage;

    // Use this for initialization
    void Awake () {
        midiaImage = transform.GetChild(0).GetComponent<Image>();
	}

    private void Start()
    {
        UpdateSprite();
    }


    private void UpdateSprite()
    {
        midiaImage.sprite = ProcedimentoSpriteDatabase.SpriteOf(procedimento);
        midiaImage.preserveAspect = true;
    }
}
