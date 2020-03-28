using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaginaProcedimentoAgrupamento : MonoBehaviour {

    public Procedimento ProcedimentoMomento1 { get; private set; }
    public Procedimento ProcedimentoMomento2 { get; private set; }
    public Procedimento ProcedimentoMomento3 { get; private set; }

    public Agrupamento AgrupamentoMomento1 { get; private set; }
    public Agrupamento AgrupamentoMomento2 { get; private set; }
    public Agrupamento AgrupamentoMomento3 { get; private set; }


    [SerializeField] private CarrosselProcedimento carrosselProc1;
    [SerializeField] private CarrosselProcedimento carrosselProc2;
    [SerializeField] private CarrosselProcedimento carrosselProc3;

    [SerializeField] private CarrosselAgrupamento carrosselAgrup1;
    [SerializeField] private CarrosselAgrupamento carrosselAgrup2;
    [SerializeField] private CarrosselAgrupamento carrosselAgrup3;


    // Use this for initialization
    private void Awake () {
        carrosselProc1.QuandoValorMudar +=
            () => ProcedimentoMomento1 = carrosselProc1.Selecionado;
        carrosselProc2.QuandoValorMudar +=
            () => ProcedimentoMomento2 = carrosselProc2.Selecionado;
        carrosselProc3.QuandoValorMudar +=
            () => ProcedimentoMomento3 = carrosselProc3.Selecionado;

        carrosselAgrup1.QuandoValorMudar +=
            () => AgrupamentoMomento1 = carrosselAgrup1.Selecionado;
        carrosselAgrup2.QuandoValorMudar +=
            () => AgrupamentoMomento2 = carrosselAgrup2.Selecionado;
        carrosselAgrup3.QuandoValorMudar +=
            () => AgrupamentoMomento3 = carrosselAgrup3.Selecionado;
    }
}
