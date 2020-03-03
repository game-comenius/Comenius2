using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static ItemName[] MidiasDisponiveisNaMissao1()
    {
        ItemName[] array =
        {
            ItemName.Caderno,
            ItemName.QuadroNegro,
            ItemName.LivroDidatico,
            ItemName.LivroIlustrado,
            ItemName.Jornais,
            ItemName.JornaisERevistas,
            ItemName.Cartazes,
            ItemName.CartazComColecaoDePenas,
            ItemName.Gravador,
            ItemName.GravacaoPassaro,
            ItemName.CameraPolaroid,
            ItemName.FotografiaPassaro,
            ItemName.TV,
            ItemName.ReprodutorAudio,
            ItemName.Cd,
        };
        // Elimina do array itens inseridos mais de uma vez sem querer
        return array.Distinct().ToArray();
    }


    private static GameManager _gameManager;

    public static GameManager gameManager
    {
        get
        {
            return _gameManager;
        }
    }

    private static bool _uiSendoUsada = false;
    
    public static bool uiSendoUsada
    {
        get
        {
            var clickSobreUI = EventSystem.current.IsPointerOverGameObject();
            // Ou a interface bloqueia o jogo completamente ou ela não bloqueia
            // o jogo mas o último click do jogador foi sobre um objeto da UI
            return _uiSendoUsada || clickSobreUI;
        }
    }


    public delegate void UsoUI();

    public static event UsoUI uiSendoUsadaEvent;

    public static event UsoUI uiNaoSendoUsadaEvent;

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;

            DontDestroyOnLoad(this);

            uiSendoUsadaEvent += () => { _uiSendoUsada = true; };

            uiNaoSendoUsadaEvent += () => { _uiSendoUsada = false; };
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        if (_gameManager == this)
        {
            uiSendoUsadaEvent -= () => { _uiSendoUsada = true; };

            uiNaoSendoUsadaEvent -= () => { _uiSendoUsada = false; };
        }
    }

    public static void UISendoUsada()
    {
        uiSendoUsadaEvent();
    }

    public static void UINaoSendoUsada()
    {
        uiNaoSendoUsadaEvent();
    }

    public void UISendoUsadaParaBotao()
    {
        uiSendoUsadaEvent();
    }

    public void UINaoSendoUsadaParaBotao()
    {
        uiNaoSendoUsadaEvent();
    }
}
