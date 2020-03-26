using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicPlayerMethods : MonoBehaviour {


    public void HelpMeioAmbiente()
    {
        Player.Instance.HelpMeioAmbiente();
    }

    public void HelpCadeirante()
    {
        Player.Instance.HelpCadeirante();
    }

    public void HelpLiteratura()
    {
        Player.Instance.HelpLiteratura();
    }

    public void HelpEstranho()
    {
        Player.Instance.HelpEstranho();
    }

    public void SolvedProblem()
    {
        Player.Instance.SolvedProblem();
    }

    public void SomarProblemasTotais(int newproblems)
    {
        Player.Instance.SomarProblemasTotais(newproblems);
    }

    public void ResultadoM1(int res)
    {
        Player.Instance.ResultadoM1(res);
    }

    public void ResultadoM2(int res)
    {
        Player.Instance.ResultadoM2(res);
    }

    public void ResultadoM3(int res)
    {
        Player.Instance.ResultadoM3(res);
    }
}
