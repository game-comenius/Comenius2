using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrocaDoDia : MonoBehaviour {

    [SerializeField]
    private NpcDialogo dialogoQueAtivaEstaTroca;

    Canvas canvas;

    Image backgroundTranslucido;
    JanelaTrocaDoDia janelaTrocaDoDia;

	// Use this for initialization
	private void Start () {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;

        var obj = canvas.transform.GetChild(0);
        backgroundTranslucido = obj.GetComponent<Image>();
        backgroundTranslucido.color = new Color(0, 0, 0, 0.7f);
        backgroundTranslucido.enabled = false;

        janelaTrocaDoDia = GetComponentInChildren<JanelaTrocaDoDia>();

        dialogoQueAtivaEstaTroca.OnEndDialogueEvent += DispararCoroutineApresentarJanela;
	}

    private void DispararCoroutineApresentarJanela() { StartCoroutine(ApresentarJanela()); }

    private IEnumerator ApresentarJanela()
    {
        GameManager.UISendoUsada();

        canvas.enabled = true;

        backgroundTranslucido.enabled = true;
        yield return new WaitForSeconds(1);

        janelaTrocaDoDia.Mostrar();

        GameManager.UINaoSendoUsada();
    }

    public void DispararCoroutineTrocar() { StartCoroutine(Trocar()); }

    private IEnumerator Trocar()
    {
        janelaTrocaDoDia.Esconder();
        yield return new WaitForSeconds(0.5f);
        GetComponent<SceneLoader>().LoadNewScene("M1_FimMissao");
        //backgroundTranslucido.enabled = false;
    }
}
