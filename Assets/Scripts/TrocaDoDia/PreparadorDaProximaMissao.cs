using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparadorDaProximaMissao : MonoBehaviour {

	public void LimparMissaoAtual()
    {
        // Apagar as quests da missão atual, a próxima missão terá suas quests
        Destroy(FindObjectOfType<ManagerQuest>().gameObject);
    }
}
