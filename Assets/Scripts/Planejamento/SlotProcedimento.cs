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

    private Image procedimentoImage;

    private void Start()
    {
        UpdateSprite();
    }

    private void CreateImageChild()
    {
        var obj = new GameObject();
        var obj_rect = obj.AddComponent<RectTransform>();
        procedimentoImage = obj.AddComponent<Image>();
        obj.transform.SetParent(this.transform);
        obj_rect.anchorMin = Vector2.zero;
        obj_rect.anchorMax = Vector2.one;
        obj_rect.offsetMin = new Vector2(15, 15);
        obj_rect.offsetMax = - new Vector2(15, 15);
        obj_rect.localScale = Vector3.one;
        obj.SetActive(true);
    }

    private void UpdateSprite()
    {
        if (!procedimentoImage) CreateImageChild();

        procedimentoImage.sprite = ProcedimentoSpriteDatabase.SpriteOf(procedimento);
        procedimentoImage.preserveAspect = true;
    }
}
