using UnityEngine;
using UnityEngine.UI;

public class SetButtonInteractivity : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        GameManager.uiNaoSendoUsadaEvent += MakeButtonInteractable;

        GameManager.uiSendoUsadaEvent += MakeButtonNotInteractable;
	}

    private void OnDisable()
    {
        GameManager.uiNaoSendoUsadaEvent -= MakeButtonInteractable;

        GameManager.uiSendoUsadaEvent -= MakeButtonNotInteractable;
    }

    private void MakeButtonInteractable() { button.interactable = true; }
    private void MakeButtonNotInteractable() { button.interactable = false; }
}
