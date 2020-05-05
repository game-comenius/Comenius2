using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using System.IO;

public class BotaoImprimirCustomMenu : MonoBehaviour {

    // Importar funções Javascript que vão auxiliar a impressão
	[DllImport("__Internal")]
	private static extern void Hello();
	[DllImport("__Internal")]
	private static extern void WriteToNewTab(string str);

	private CreateCustomGamePanel createCustomGamePanel;

	// Use this for initialization
	void Start () {
		createCustomGamePanel = GetComponentInParent<CreateCustomGamePanel>();
	}
	
	public void AcionarBotao()
    {
        WriteToNewTab(TransformarMissaoCustomEmPaginaHTML());

        // Para testar localmente, comentar WriteToNewTab e descomentar este
        //WriteToDisk(TransformarMissaoCustomEmPaginaHTML());
    }

    private string TransformarMissaoCustomEmPaginaHTML()
    {
		var settings = createCustomGamePanel.CriarCustomGameSettings();
        return "<p>" + settings.ToHTMLString() + "</p>";
    }

    // Função para testar localmente
    private void WriteToDisk(string htmlString)
    {
        using (StreamWriter outputFile = new StreamWriter("testeImprimir.html"))
        {
            outputFile.WriteLine(htmlString);
        }

    }
}
