using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraAjusteScript : MonoBehaviour
{
    [SerializeField] private Vector2Int dimensoesTelaStandart = new Vector2Int(1920, 1080);

    private float RatioStandart
    {
        get
        {
            return (float)dimensoesTelaStandart.x / (float)dimensoesTelaStandart.y;
        }
    }

    private Vector2 dimensoesTela = Vector2.zero;

    private float Ratio
    {
        get
        {
            return dimensoesTela.x / dimensoesTela.y;
        }
    }

	void Start ()
    {
        dimensoesTela = new Vector2(Screen.width, Screen.height);

        if (Ratio < RatioStandart)
        {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize * RatioStandart / Ratio;
        }
	}
}