using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour, IPointerClickHandler {

    // Campos relacionados à UI
    private static Color offColor = new Color(0.6483624f, 0.6640738f, 0.8867924f, 0);
    private static Color onColor = new Color(0.700088f, 0.8862745f, 0.6470588f);
    private Image buttonBackgroundImage;

    private bool selected;
    public bool Selected
    {
        get
        {
            return selected;
        }
        private set
        {
            selected = value;
            if (selected)
                buttonBackgroundImage.color = onColor;
            else
                buttonBackgroundImage.color = offColor;
        }
    }

    private ItemName item;
    public ItemName Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            var itemImage = transform.GetChild(0).GetComponent<Image>();
            itemImage.sprite = ItemSpriteDatabase.GetSpriteOf(item);
        }
    }


    // Use this for initialization
    void Start () {
        buttonBackgroundImage = GetComponent<Image>();

        Selected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected = !Selected;
    }
}