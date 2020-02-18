using UnityEngine;

public class Fichario : MonoBehaviour {

    [SerializeField]
    private GameObject folhaInventario;
    [SerializeField]
    private GameObject folhaMapa;
    [SerializeField]
    private GameObject folhaDiario;

    private static GameObject folhaSelecionada;

    private CanvasGroup canvasGroup;

    private bool aberto;
    public bool Aberto
    {
        get { return aberto; }

        private set
        {
            aberto = value;
            if (aberto) GameManager.UISendoUsada();
            else GameManager.UINaoSendoUsada();
        }
    }

    void Start () {
        canvasGroup = GetComponent<CanvasGroup>();

        // O fichário começa com a página do inventário
        SelecionarInventario();

        // Esconder o fichário
        DefinirVisibilidadeGeral(false);
    }

    private void DefinirVisibilidadeGeral(bool visivel)
    {
        canvasGroup.alpha = visivel ? 1 : 0;
        canvasGroup.blocksRaycasts = visivel;
    }

    public void Fechar()
    {
        if (!Aberto) return;
        DefinirVisibilidadeGeral(false);
        Aberto = false;
    }

    public void Abrir()
    {
        if (Aberto) return;
        DefinirVisibilidadeGeral(true);
        Aberto = true;
    }

    public void SelecionarDiario()
    {
        if (folhaSelecionada == folhaDiario) return;

        folhaSelecionada = folhaDiario;
        folhaInventario.transform.SetAsFirstSibling();
        folhaMapa.transform.SetAsFirstSibling();
    }

    public void SelecionarMapa()
	{
        if (folhaSelecionada == folhaMapa) return;

        folhaSelecionada = folhaMapa;
        folhaInventario.transform.SetAsFirstSibling();
        folhaDiario.transform.SetAsFirstSibling();
    }

    public void SelecionarInventario()
	{
        if (folhaSelecionada == folhaInventario) return;

        folhaSelecionada = folhaInventario;
        folhaMapa.transform.SetAsFirstSibling();
        folhaDiario.transform.SetAsFirstSibling();
	}
}
