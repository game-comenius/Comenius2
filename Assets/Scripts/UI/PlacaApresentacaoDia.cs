using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacaApresentacaoDia : MonoBehaviour {

    private enum Dia { PrimeiroDia, SegundoDia, TerceiroDia }
    [SerializeField]
    private Dia dia;

    private void Awake()
    {
        // Destruir placas anteriores
        var placas = FindObjectsOfType<PlacaApresentacaoDia>();
        foreach (var placa in placas)
            if (placa != this) Destroy(placa.gameObject);

        // Desativar diálogos obrigatórios temporariamente
        var npcDialogoObrigatorioList = new List<NpcDialogo>();
        var npcDialogoArray = FindObjectsOfType<NpcDialogo>();
        foreach (var npcDialogo in npcDialogoArray)
        {
            if (npcDialogo.dialogoObrigatorio)
            {
                npcDialogoObrigatorioList.Add(npcDialogo);
                npcDialogo.dialogoObrigatorio = false;
            }
        }

        // Reativar os diálogos obrigatórios
        StartCoroutine(RestartDialogosAposAnimacao(npcDialogoObrigatorioList));

        // Placa vai continuar nas cenas seguintes
        transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    private void Start () {
        string textoDaPlaca;
        switch (dia)
        {
            default: textoDaPlaca = "Primeiro Dia"; break;
            case Dia.SegundoDia: textoDaPlaca = "Segundo Dia"; break;
            case Dia.TerceiroDia: textoDaPlaca = "Terceiro Dia"; break;
        }
        var tMPro = GetComponentInChildren<TextMeshProUGUI>();
        tMPro.text = textoDaPlaca;

        // Impedir que o jogador caminhe enquanto a animação está executando
        // A animação será executada sem parâmetros, ou seja, na criação deste
        GameManager.UISendoUsada();
    }

    private IEnumerator RestartDialogosAposAnimacao(List<NpcDialogo> npcDialogos)
    {
        var animator = GetComponentInChildren<Animator>();
        // Linha que eu achei na internet para verificar se um animator já
        // fez mais que 99% da animação atual, ou seja, se ela terminou
        System.Func<bool> animacaoAcabou = () => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .99f;
        yield return new WaitUntil(animacaoAcabou);

        yield return new WaitForSeconds(1);
        foreach (var npcDialogo in npcDialogos)
            StartCoroutine(npcDialogo.Interact());
    }
}
