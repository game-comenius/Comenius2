using UnityEngine;
using UnityEngine.UI;

public class SetButtonInteractivity : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        GameManager.uiNaoSendoUsadaEvent += () => { button.interactable = true; };

        GameManager.uiSendoUsadaEvent += () => { button.interactable = false; };
	}

    private void OnDisable()
    {
        GameManager.uiNaoSendoUsadaEvent -= () => { button.interactable = true; };

        GameManager.uiSendoUsadaEvent -= () => { button.interactable = false; };
    }
}
