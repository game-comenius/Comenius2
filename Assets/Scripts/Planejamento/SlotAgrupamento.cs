    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAgrupamento : MonoBehaviour {

    [SerializeField]
    private Agrupamento agrupamento;
    public Agrupamento Agrupamento
    {
        get
        {
            return agrupamento;
        }

        set
        {
            agrupamento = value;
            UpdateSprite();
        }
    }

    private Image agrupamentoImage;

    // Use this for initialization
    void Awake()
    {
        CreateImageChild();
    }

    private void Start()
    {
        UpdateSprite();
    }

    private void CreateImageChild()
    {
        var obj = new GameObject();
        var obj_rect = obj.AddComponent<RectTransform>();
        agrupamentoImage = obj.AddComponent<Image>();
        obj.transform.SetParent(this.transform);
        obj_rect.anchorMin = Vector2.zero;
        obj_rect.anchorMax = Vector2.one;
        obj_rect.offsetMin = new Vector2(15, 15);
        obj_rect.offsetMax = -new Vector2(15, 15);
        obj_rect.localScale = Vector3.one;
        obj.SetActive(true);
    }

    private void UpdateSprite()
    {
        agrupamentoImage.sprite = AgrupamentoSpriteDatabase.SpriteOf(agrupamento);
        agrupamentoImage.preserveAspect = true;
    }
}
