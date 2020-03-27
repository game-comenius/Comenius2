using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameAnimation : MonoBehaviour {

    //referências aos animators das páginas

    public Animator MeioAmbiente0;
    public Animator MeioAmbiente1;
    public Animator MeioAmbiente2;
    public Animator MeioAmbiente3;

    public Animator CadeiranteFalse;
    public Animator CadeiranteTrue;

    public Animator Literatura0;
    public Animator Literatura1;
    public Animator Literatura2;
    public Animator Literatura3;

    public Animator EstranhoFalse;
    public Animator EstranhoTrue;

    public Animator Problemas0a30;
    public Animator Problemas31a60;
    public Animator Problemas61a100;

    public Animator JeanMelhor;
    public Animator JeanMuitoBoa;
    public Animator JeanBoa;
    public Animator JeanRuim;

    public Animator VladmirMelhor;
    public Animator VladmirMuitoBoa;
    public Animator VladmirBoa;
    public Animator VladmirRuim;

    public Animator PaulinoMelhor;
    public Animator PaulinoMuitoBoa;
    public Animator PaulinoBoa;
    public Animator PaulinoRuim;

    //a página que vai ser animada (ou acabou de ser animada)
    private Animator animatorNextPage;

    private void PageTransitionAnimation()
    {
        animatorNextPage.SetBool("Enter", true);
    }

    public void PageAmbiente()
    {
        switch (Player.Instance.meioambiente)
        {
            case 3:
                animatorNextPage = MeioAmbiente3;
                gameObject.transform.Find("MeioAmbiente2").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente1").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente0").gameObject.SetActive(false);
                break;
            case 2:
                animatorNextPage = MeioAmbiente2;
                gameObject.transform.Find("MeioAmbiente3").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente1").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente0").gameObject.SetActive(false);
                break;
            case 1:
                animatorNextPage = MeioAmbiente1;
                gameObject.transform.Find("MeioAmbiente2").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente3").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente0").gameObject.SetActive(false);
                break;
            default:
                animatorNextPage = MeioAmbiente0;
                gameObject.transform.Find("MeioAmbiente2").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente1").gameObject.SetActive(false);
                gameObject.transform.Find("MeioAmbiente3").gameObject.SetActive(false);
                break;
        }
        PageTransitionAnimation();
    }

    public void PageCadeirante()
    {
        animatorNextPage.gameObject.SetActive(false); //desativa a página anterior
        switch (Player.Instance.cadeirante)
        {
            case true:
                animatorNextPage = CadeiranteTrue;
                gameObject.transform.Find("CadeiranteFalse").gameObject.SetActive(false);
                break;
            default:
                animatorNextPage = CadeiranteFalse;
                gameObject.transform.Find("CadeiranteTrue").gameObject.SetActive(false);
                break;
        }
        PageTransitionAnimation();
    }

    public void PageLiteratura()
    {
        animatorNextPage.gameObject.SetActive(false);
        switch (Player.Instance.literatura)
        {
            case 3:
                animatorNextPage = Literatura3;
                gameObject.transform.Find("Literatura2").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura1").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura0").gameObject.SetActive(false);
                break;
            case 2:
                animatorNextPage = Literatura2;
                gameObject.transform.Find("Literatura3").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura1").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura0").gameObject.SetActive(false);
                break;
            case 1:
                animatorNextPage = Literatura1;
                gameObject.transform.Find("Literatura2").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura3").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura0").gameObject.SetActive(false);
                break;
            default:
                animatorNextPage = Literatura0;
                gameObject.transform.Find("Literatura2").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura1").gameObject.SetActive(false);
                gameObject.transform.Find("Literatura3").gameObject.SetActive(false);
                break;
        }
        PageTransitionAnimation();
    }

    public void PageEstranho()
    {
        animatorNextPage.gameObject.SetActive(false);
        switch (Player.Instance.estranho)
        {
            case true:
                animatorNextPage = EstranhoTrue;
                gameObject.transform.Find("EstranhoFalse").gameObject.SetActive(false);
                break;
            default:
                animatorNextPage = EstranhoFalse;
                gameObject.transform.Find("EstranhoTrue").gameObject.SetActive(false);
                break;
        }
        PageTransitionAnimation();
    }

    public void PageProblemas()
    {
        animatorNextPage.gameObject.SetActive(false);
        if (Player.Instance.porcentagemproblemas > 60)
        {
            animatorNextPage = Problemas61a100;
            gameObject.transform.Find("Problemas0a30").gameObject.SetActive(false);
            gameObject.transform.Find("Problemas61a60").gameObject.SetActive(false);
        } else if (Player.Instance.porcentagemproblemas > 30)
        {
            animatorNextPage = Problemas31a60;
            gameObject.transform.Find("Problemas61a100").gameObject.SetActive(false);
            gameObject.transform.Find("Problemas0a30").gameObject.SetActive(false);
        } else
        {
            animatorNextPage = Problemas0a30;
            gameObject.transform.Find("Problemas61a100").gameObject.SetActive(false);
            gameObject.transform.Find("Problemas31a60").gameObject.SetActive(false);
        }
        PageTransitionAnimation();
    }

    //perdão pelos nomes dos ints, tô só respeitando os nomes que deram no doc
    //pra não ter problemas de comunicação
    public void PageJean(int pontuacaomelhor, int pontuacaomuitoboa, int pontuacaoboa)
    {
        animatorNextPage.gameObject.SetActive(false);
        if (Player.Instance.MissionHistory[0].totalMissionPoints > pontuacaomelhor)
        {
            animatorNextPage = JeanMelhor;
            gameObject.transform.Find("JeanMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("JeanBoa").gameObject.SetActive(false);
            gameObject.transform.Find("JeanRuim").gameObject.SetActive(false);
        }
        else if (Player.Instance.MissionHistory[0].totalMissionPoints > pontuacaomuitoboa)
        {
            animatorNextPage = JeanMuitoBoa;
            gameObject.transform.Find("JeanMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("JeanBoa").gameObject.SetActive(false);
            gameObject.transform.Find("JeanRuim").gameObject.SetActive(false);
        }
        else if (Player.Instance.MissionHistory[0].totalMissionPoints > pontuacaoboa)
        {
            animatorNextPage = JeanBoa;
            gameObject.transform.Find("JeanMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("JeanMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("JeanRuim").gameObject.SetActive(false);
        } else
        {
            animatorNextPage = JeanRuim;
            gameObject.transform.Find("JeanMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("JeanMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("JeanBoa").gameObject.SetActive(false);
        }
        PageTransitionAnimation();
    }

    public void PageVladmir(int pontuacaomelhor, int pontuacaomuitoboa, int pontuacaoboa)
    {
        animatorNextPage.gameObject.SetActive(false);
        if (Player.Instance.MissionHistory[1].totalMissionPoints > pontuacaomelhor)
        {
            animatorNextPage = VladmirMelhor;
            gameObject.transform.Find("VladmirMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirBoa").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirRuim").gameObject.SetActive(false);
        }
        else if (Player.Instance.MissionHistory[1].totalMissionPoints > pontuacaomuitoboa)
        {
            animatorNextPage = VladmirMuitoBoa;
            gameObject.transform.Find("VladmirMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirBoa").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirRuim").gameObject.SetActive(false);
        }
        else if (Player.Instance.MissionHistory[1].totalMissionPoints > pontuacaoboa)
        {
            animatorNextPage = VladmirBoa;
            gameObject.transform.Find("VladmirMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirRuim").gameObject.SetActive(false);
        }
        else
        {
            animatorNextPage = VladmirRuim;
            gameObject.transform.Find("VladmirMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("VladmirBoa").gameObject.SetActive(false);
        }
        PageTransitionAnimation();
    }

    public void PagePaulino(int pontuacaomelhor, int pontuacaomuitoboa, int pontuacaoboa)
    {
        animatorNextPage.gameObject.SetActive(false);
        if (Player.Instance.MissionHistory[2].totalMissionPoints > pontuacaomelhor)
        {
            animatorNextPage = PaulinoMelhor;
            gameObject.transform.Find("PaulinoMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoRuim").gameObject.SetActive(false);
        }
        else if (Player.Instance.MissionHistory[2].totalMissionPoints > pontuacaomuitoboa)
        {
            animatorNextPage = PaulinoMuitoBoa;
            gameObject.transform.Find("PaulinoMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoRuim").gameObject.SetActive(false);
        }
        else if (Player.Instance.MissionHistory[2].totalMissionPoints > pontuacaoboa)
        {
            animatorNextPage = PaulinoBoa;
            gameObject.transform.Find("PaulinoMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoRuim").gameObject.SetActive(false);
        }
        else
        {
            animatorNextPage = PaulinoRuim;
            gameObject.transform.Find("PaulinoMuitoBoa").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoMelhor").gameObject.SetActive(false);
            gameObject.transform.Find("PaulinoBoa").gameObject.SetActive(false);
        }
        PageTransitionAnimation();
    }
}
