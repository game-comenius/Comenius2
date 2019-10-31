using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomProfessor : MonoBehaviour {

    private string falaSalaProfessores;

	// Use this for initialization
	void Start () {
        var settings = CustomGameLoadedSettings.Settings;

        falaSalaProfessores = settings.FalaDoProfessor;

        var npc = GetComponent<NpcDialogo>();
        npc.dialogoPrincipal = new GameComenius.Dialogo.Dialogo();
        npc.dialogoPrincipal.nodulos = new GameComenius.Dialogo.DialogoNodulo[1];
        npc.dialogoPrincipal.nodulos[0] = new GameComenius.Dialogo.DialogoNodulo();
        npc.dialogoPrincipal.nodulos[0].falas = new GameComenius.Dialogo.Fala[2];
        npc.dialogoPrincipal.nodulos[0].falas[0] = new GameComenius.Dialogo.Fala
        {
            fala = "Olá professor!",
            personagem = GameComenius.Dialogo.Personagens.Lurdinha,
            emocao = GameComenius.Dialogo.Expressao.Sorrindo
        };

        npc.dialogoPrincipal.nodulos[0].falas[1] = new GameComenius.Dialogo.Fala
        {
            fala = falaSalaProfessores,
            personagem = GameComenius.Dialogo.Personagens.Jean,
            emocao = GameComenius.Dialogo.Expressao.Sorrindo
        };
    }
}
