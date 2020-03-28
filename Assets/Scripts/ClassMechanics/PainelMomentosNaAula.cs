using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelMomentosNaAula : MonoBehaviour {

    [Serializable]
    public class SlotPainelMomentosAula
    {
        public ItemInUserInterface midiaSelecionada;
        public Image imagePoderMidiaSelecionada;
    }
    public SlotPainelMomentosAula slot1;
    public SlotPainelMomentosAula slot2;
    public SlotPainelMomentosAula slot3;

    [SerializeField] private Sprite spriteMidiaFraca;
    [SerializeField] private Sprite spriteMidiaBoa;
    [SerializeField] private Sprite spriteMidiaMuitoBoa;
    [SerializeField] private Sprite spriteMidiaMelhor;

    // Use this for initialization
    void Start () {
        SlotPainelMomentosAula[] slots = { slot1, slot2, slot3 };
        foreach (var slot in slots)
        {
            slot.midiaSelecionada.GetComponent<Image>().enabled = false;
            slot.imagePoderMidiaSelecionada.enabled = false;
        }
	}

    // momento1 = 0; momento2 = 1; momento3 = 2
    public void MostrarMidiaSelecionada(int momento)
    {
        SlotPainelMomentosAula slotAlvo = null;
        switch (momento)
        {
            case 0: slotAlvo = slot1; break;
            case 1: slotAlvo = slot2; break;
            case 2: slotAlvo = slot3; break;
            default: break;
        }

        var history = Player.Instance.MissionHistory[Player.Instance.missionID];
        var midia = history.chosenMedia[momento];
        var poder = (int)history.points[momento];

        slotAlvo.midiaSelecionada.ItemName = midia;
        slotAlvo.midiaSelecionada.GetComponent<Image>().enabled = true;

        // fraca = 0; boa = 7; muitoBoa = 8; Melhor = 10
        switch (poder)
        {
            default:
                slotAlvo.imagePoderMidiaSelecionada.sprite = spriteMidiaFraca;
                break;
            case 7:
                slotAlvo.imagePoderMidiaSelecionada.sprite = spriteMidiaBoa;
                break;
            case 8:
                slotAlvo.imagePoderMidiaSelecionada.sprite = spriteMidiaMuitoBoa;
                break;
            case 10:
                slotAlvo.imagePoderMidiaSelecionada.sprite = spriteMidiaMelhor;
                break;
        }
        StartCoroutine(MostrarPoderMidiaSelecionada(slotAlvo));
    }

    private IEnumerator MostrarPoderMidiaSelecionada(SlotPainelMomentosAula slot)
    {
        // Esperar um tempo antes de mostrar o poder
        yield return new WaitForSeconds(.5f);
        slot.imagePoderMidiaSelecionada.enabled = true;
        // Se houver uma animação, executar a animação
        var animator = slot.imagePoderMidiaSelecionada.GetComponent<Animator>();
        if (animator) animator.Play("MostrarPoderPainelMomentos");
    }
}
