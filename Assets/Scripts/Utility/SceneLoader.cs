using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNewScene(string newSceneName)
    {
        SceneManager.LoadSceneAsync(newSceneName);
    }

    public void LoadFreshNewScene(string newSceneName)
    {
        // Destruir objetos que podem estar no DontDestroyOnLoad para impedir
        // que eles sobrevivam à troca de cena
        var botaoFichario = FindObjectOfType<BotaoAbrirFichario>();
        if (botaoFichario) botaoFichario.Visivel = false;

        var conselheiroComenius = FindObjectOfType<ConselheiroComenius>();
        if (conselheiroComenius) Destroy(conselheiroComenius.gameObject);

        var player = FindObjectOfType<Player>();
        if (player) Destroy(player.gameObject);

        SceneManager.LoadSceneAsync(newSceneName);
    }
}