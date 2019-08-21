using UnityEngine;

[System.Serializable]
public class Opacidade
{
    [Range(0, 1)]
    public float alphaLigado;

    [Range(0, 1)]
    public float alphaDesligado;
    
    public Color Ligar()
    {
        return new Color(1, 1, 1, alphaLigado);
    }

    public Color Desligar()
    {
        return new Color(1, 1, 1, alphaDesligado);
    }
}
