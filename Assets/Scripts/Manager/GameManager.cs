using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static ItemName[] MidiasDisponiveisEmTodasAsMissoes = 
    {
        ItemName.ReprodutorAudio,
        ItemName.Gravador,
        ItemName.CameraPolaroid,
        ItemName.TVComVHS,
        ItemName.VHS,
        ItemName.LivroDidatico,
        ItemName.LivroIlustrado,
        ItemName.QuadroNegro,
        ItemName.Caderno,
        ItemName.Cartazes,
        ItemName.Jornais,
        ItemName.JornaisERevistas,
    };

    public static ItemName[] MidiasExclusivasDaMissao1 =
    {
        ItemName.GravacaoPassaro,
        ItemName.FotografiaPassaro,
        ItemName.CartazComColecaoDePenas,
        ItemName.TVComVHSPassaros,
        ItemName.Cd,
        ItemName.Mapa,
    };

    public static ItemName[] MidiasDisponiveisNaMissao1
    {
        get
        {
            return MidiasDisponiveisEmTodasAsMissoes.Union(MidiasExclusivasDaMissao1).ToArray();
        }
    }

    public static ItemName[] MidiasExclusivasDaMissao2 = 
    {
        ItemName.Diario,
        ItemName.Retroprojetor,
        ItemName.RetroprojetorSlideCicloTrabalho,
        ItemName.RetroprojetorSlideLinhaTempo,
        ItemName.RetroprojetorSlideMapa,
        ItemName.CartazComCanetas,
        ItemName.FotografiaRevolucaoIndustrial,
        ItemName.ReprodutorAudioComCDRevolucaoIndustrial,
        ItemName.TVComVHSRevolucaoIndustrial,
        ItemName.TVComVHSRevolucaoIndustrialEditado,
    };

    public static ItemName[] MidiasDisponiveisNaMissao2
    {
        get
        {
            return MidiasDisponiveisEmTodasAsMissoes.Union(MidiasExclusivasDaMissao2).ToArray();
        }
    }

    public static ItemName[] MidiasExclusivasDaMissao3 = 
    {
        ItemName.FolhaSulfite,
        ItemName.VHSregionalismo,
        ItemName.VHSregionalismoEditado,
        ItemName.Enciclopedia,
        ItemName.Adedonha,
        ItemName.Forca,
        ItemName.PalavrasCruzadas,
        ItemName.CDsotaques,
    };

    public static ItemName[] MidiasDisponiveisNaMissao3
    {
        get
        {
            return MidiasDisponiveisEmTodasAsMissoes.Union(MidiasExclusivasDaMissao3).ToArray();
        }
    }

    public static ItemName[] MidiasDoJogo
    {
        get
        {
            var u = MidiasDisponiveisNaMissao1.Union(MidiasDisponiveisNaMissao2);
            return u.Union(MidiasDisponiveisNaMissao3).ToArray();
        }
    }

    public static bool IsCustomGame { get; set; }

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
