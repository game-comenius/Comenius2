using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeacherScript : AgenteAulaScript 
{
    public static TeacherScript teacher
    {
        get;
        private set;
    }

    [SerializeField] private Vector2 inicio = Vector2.zero;

    [SerializeField] private Vector2 vector = Vector2.zero;

    [SerializeField] private float magnitude = 0;

    private float posicao = 0;

    [SerializeField] private float velocity = 0;

    private Coroutine walk = null;

    [SerializeField] private Sprite[] sprites = new Sprite[4];

    private SpriteRenderer sprite = null;

    [SerializeField] private Vector2 stopTimeRange;

    [SerializeField] private GameObject canvas;

    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject balao;

    private GameObject balaoIns;

    private TMP_Text text;

    [SerializeField] private float speechWait;

    [SerializeField] private Vector2 offset;

    private Coroutine speech;

    protected void Awake()
    {
        teacher = this;
    }

    protected override void Start()
    {
        base.Start();

        vector.Normalize();

        transform.position = inicio;

        sprite = GetComponent<SpriteRenderer>();
    }
    
    public void StartWalk()
    {
        if (walk == null)
        {
            balaoIns = Instantiate(balao, _camera.WorldToScreenPoint((Vector2)transform.position + offset), balao.transform.rotation, canvas.transform);

            text = balaoIns.GetComponentInChildren<TMP_Text>();

            walk = StartCoroutine(Walk());

            speech = StartCoroutine(Speak());
        }
    }

    private void Gerar()
    {
        float novaPos = Random.value;

        if (novaPos * magnitude > posicao)
        {
            novaPos = 1 - ((1 - novaPos) * (1 - novaPos));

            sprite.sprite = sprites[2];
        }
        else
        {
            novaPos = novaPos * novaPos;

            sprite.sprite = sprites[1];
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

            balaoIns.transform.position = _camera.WorldToScreenPoint((Vector2)transform.position + offset);

            yield return null;

            if ((Vector2)transform.position == inicio + vector * posicao) 
            {
                sprite.sprite = sprites[0];

                yield return new WaitForSeconds(Random.Range(stopTimeRange.x, stopTimeRange.y));

                Gerar();
            }
        }
    }

    private IEnumerator Speak()
    {
        text.text = "...";

        while (true) 
        {
            yield return new WaitForSeconds(speechWait);

            text.text = "";

            yield return new WaitForSeconds(speechWait);

            text.text = ".  ";

            yield return new WaitForSeconds(speechWait);

            text.text = ".. ";

            yield return new WaitForSeconds(speechWait);

            text.text = "...";
        }
    }

    public void PauseWalk()
    {
        posicao = ((Vector2)transform.position - inicio).x / vector.x;

        StopCoroutine(walk);

        sprite.sprite = sprites[0];

        walk = null;

        StopCoroutine(speech);

        Destroy(balaoIns);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(inicio, 0.1f);
        Gizmos.DrawSphere(inicio + vector.normalized * magnitude, 0.1f);

        Gizmos.DrawLine(inicio, inicio + vector.normalized * magnitude);

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere((Vector2)transform.position + offset, 0.1f);
    }
}
