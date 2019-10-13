using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScript : AgenteAulaScript 
{
    [SerializeField] private Vector2 inicio = Vector2.zero;

    [SerializeField] private Vector2 vector = Vector2.zero;

    [SerializeField] private float magnitude = 0;

    private float posicao = 0;

    [SerializeField] private float velocity = 0;

    private Coroutine walk = null;

    protected override void Start()
    {
        base.Start();

        vector.Normalize();

        transform.position = inicio;

        StartWalk();
    }
    
    public void StartWalk()
    {
        walk = StartCoroutine(Walk());
    }

    private void Gerar()
    {
        float novaPos = Random.value;

        if (novaPos * magnitude > posicao)
        {
            novaPos = 1 - ((1 - novaPos) * (1 - novaPos));
        }
        else
        {
            novaPos = novaPos * novaPos;
        }

        posicao = novaPos * magnitude;
    }

    private IEnumerator Walk()
    {
        Gerar();

        while(true)
        {
            transform.position = Vector2.Lerp(transform.position, inicio + (vector * posicao), 
                (velocity * Time.deltaTime) / ((inicio + (vector * posicao)) - (Vector2)transform.position).magnitude);

            yield return null;

            if ((Vector2)transform.position == inicio + vector * posicao) 
            {
                Gerar();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(inicio, 0.1f);
        Gizmos.DrawSphere(inicio + vector.normalized * magnitude, 0.1f);

        Gizmos.DrawLine(inicio, inicio + vector.normalized * magnitude);
    }
}
