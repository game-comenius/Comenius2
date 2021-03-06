﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpNPCRequestBalloon : MonoBehaviour {

    private NpcDialogo npcDialogo;

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject interiorDoBalao;

    private bool shouldUpdate;


	private void Start () {
        npcDialogo = GetComponentInParent<NpcDialogo>();
        if (!npcDialogo) return;

        spriteRenderer = GetComponent<SpriteRenderer>();

        shouldUpdate = true;
	}

    private void Update()
    {
        if (!shouldUpdate) return;

        if (npcDialogo.WantsToTalk)
        {
            ShowRequestBalloon();
            // Esconder sprite quando o diálogo terminar
            npcDialogo.OnEndDialogueEvent += HideRequestBalloon;
            // Não precisa mais executar este Update, o sprite vai sumir
            // assim que o diálogo terminar
            shouldUpdate = false;
        }
        else
            HideRequestBalloon();
    }


    private void ShowRequestBalloon()
    {
        spriteRenderer.enabled = true;
        interiorDoBalao.SetActive(true);
        // Posicionar o sprite logo acima do sprite do npcDialogo
        var npcDialogoSpriteRenderer = npcDialogo.GetComponent<SpriteRenderer>();
        var npcSpriteHeight = npcDialogoSpriteRenderer.bounds.extents.y;
        var mySpriteHeight = spriteRenderer.bounds.extents.y;
        transform.localPosition = new Vector3(0, npcSpriteHeight + mySpriteHeight);

        gameObject.AddComponent<DynamicCursor>();
    }

    private void HideRequestBalloon()
    {
        interiorDoBalao.SetActive(false);
        spriteRenderer.enabled = false;
        
        var cursorDiferente = GetComponent<DynamicCursor>();
        if (cursorDiferente) Destroy(cursorDiferente);
    }

    // Quando o jogador apertar sobre o balão, é como se ele apertasse no
    // NPC que possui este balão para conversar
    private void OnMouseUp()
    {
        var dialogo = transform.parent.GetComponent<NpcDialogo>();
        if (dialogo && dialogo.WantsToTalk) dialogo.OnMouseUp();
    }
}
