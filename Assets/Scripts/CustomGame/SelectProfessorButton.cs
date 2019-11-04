using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectProfessorButton : MonoBehaviour, IPointerClickHandler
{

    private static SelectProfessorButton currentlySelectedButton;
    public static SelectProfessorButton CurrentlySelectedButton
    {
        get
        {
            return currentlySelectedButton;
        }
        private set
        {
            if (currentlySelectedButton)
                currentlySelectedButton.buttonBackgroundImage.color = offColor;
            currentlySelectedButton = value;
            currentlySelectedButton.buttonBackgroundImage.color = onColor;
        }
    }

    [SerializeField]
    private bool startSelected;

    [SerializeField]
    private CharacterName professor;
    public CharacterName Professor
    {
        get { return professor; }
        private set { professor = value; }
    }

    // Campos relacionados à UI
    private static Color offColor = new Color(0.6483624f, 0.6640738f, 0.8867924f, 0.8f);
    private static Color onColor = new Color(0.700088f, 0.8862745f, 0.6470588f);
    private Image buttonBackgroundImage;
    private Image professorImage;


    // Use this for initialization
    void Start()
    {
        buttonBackgroundImage = GetComponent<Image>();
        buttonBackgroundImage.color = offColor;


        if (startSelected)
            CurrentlySelectedButton = this;

        professorImage = transform.GetChild(0).GetComponent<Image>();
        professorImage.sprite = CharacterSpriteDatabase.SpriteSW(professor);
        professorImage.preserveAspect = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CurrentlySelectedButton = this;
    }
}
