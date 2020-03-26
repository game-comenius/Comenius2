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

    [SerializeField] private Vector2 offsetDaBase = Vector2.zero;

    [SerializeField] private float magnitude = 0;

    private float posicao = 0;

    [SerializeField] private float velocity = 0;

    [SerializeField] private float frameLonging = 0.3f;

    [SerializeField] private Sprite[] goRight = new Sprite[4];
    public Sprite[] GoRight
    {
        get { return goRight; }
        set { goRight = value; }
    }

    [SerializeField] private Sprite[] goLeft = new Sprite[4];
    public Sprite[] GoLeft
    {
        get { return goLeft; }
        set { goLeft = value; }
    }

    private bool goingRight = false;

    private Coroutine walk = null;

    public Coroutine walkCoroutine
    {
        get
        {
            return walk;
        }
    }

    [SerializeField] private Sprite[] sprites = new Sprite[4];
    public Sprite[] Sprites
    {
        get { return sprites; }
        set { sprites = value; }
    }

    private SpriteRenderer sprite = null;

    [SerializeField] private Vector2 stopTimeRange;

    [SerializeField] private GameObject canvas;

    [SerializeField] private Camera _camera;

    private bool aulaAcabou = false;

    public bool CanWalk = true;


    protected void Awake()
    {
        teacher = this;

        ClassManager.EndClass += EndClass;
    }

    private void OnDestroy()
    {
        ClassManager.EndClass -= EndClass;
    }

    protected override void Start()
    {
        base.Start();

        // Para que o professor ande nos quadradinhos certos, sem usar path finder
        inicio = GridScript.gridScript.Cell(GridScript.gridScript.P2G(inicio + offsetDaBase))[0] - offsetDaBase;
        vector = GridScript.gridScript.Cell(GridScript.gridScript.P2G(inicio + vector + offsetDaBase))[0] - inicio - offsetDaBase;

        magnitude = vector.magnitude;

        vector.Normalize();

        transform.position = inicio;

        transform.position += new Vector3(0, 0, transform.position.y + offsetDaBase.y);

        sprite = GetComponent<SpriteRenderer>();
    }
    
    public void StartWalk()
    {
        if (!CanWalk) return;

        if (walk == null) walk = StartCoroutine(Walk());
    }

    private void Gerar()
    {
        float novaPos = posicao;

        while (novaPos == posicao)
        {
            novaPos = Random.value;

            if (novaPos * magnitude > posicao)
            {
                novaPos = 1 - ((1 - novaPos) * (1 - novaPos));

                sprite.sprite = sprites[2];

                goingRight = true;
            }
            else
            {
                novaPos = novaPos * novaPos;

                sprite.sprite = sprites[1];

                goingRight = false;
            }

            posicao = novaPos * magnitude;

            Vector2 pos = GridScript.gridScript.Cell(GridScript.gridScript.P2G(inicio + vector * posicao + offsetDaBase))[0] - inicio - offsetDaBase;

            posicao = pos.magnitude;
        }
    }

    private IEnumerator Walk()
    {
        Gerar();

        float t = 0;

        int frame = 0;

        while(true)
        {
            transform.position = Vector2.Lerp(transform.position, inicio + (vector * posicao), 
                (velocity * Time.deltaTime) / ((inicio + (vector * posicao)) - (Vector2)transform.position).magnitude);

            transform.position += new Vector3(0, 0, transform.position.y + offsetDaBase.y);

            yield return null;

            t += Time.deltaTime;

            frame = Mathf.FloorToInt(t / frameLonging);

            if (goingRight) 
            {
                if (frame >= goRight.Length)
                {
                    t -= frame * frameLonging;

                    frame = frame % goRight.Length;
                }

                GetComponent<SpriteRenderer>().sprite = goRight[frame];
            }
            else
            {
                if (frame >= goLeft.Length)
                {
                    t -= frame * frameLonging;

                    frame = frame % goLeft.Length;
                }

                GetComponent<SpriteRenderer>().sprite = goLeft[frame];
            }

            if ((Vector2)transform.position == inicio + vector * posicao) 
            {
                if (aulaAcabou)
                {
                    sprite.sprite = sprites[0];

                    walk = null;

                    yield break;
                }

                sprite.sprite = sprites[0];

                yield return new WaitForSeconds(Random.Range(stopTimeRange.x, stopTimeRange.y));

                Gerar();

                t = 0;
            }
        }
    }

    public void FimDaAula()
    {
        PauseWalk();
        aulaAcabou = true;
    }

    public void PauseWalk()
    {
        posicao = ((Vector2)transform.position - inicio).x / vector.x;

        if (walk != null) StopCoroutine(walk);

        sprite.sprite = sprites[0];

        walk = null;
    }

    private void EndClass()
    {
        GridScript.gridScript.ChangeGrid(GridScript.gridScript.P2G((Vector2)transform.position + offsetDaBase), false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(inicio + offsetDaBase, 0.1f);
        Gizmos.DrawSphere(inicio + vector.normalized * magnitude + offsetDaBase, 0.1f);

        Gizmos.DrawLine(inicio + offsetDaBase, inicio + vector.normalized * magnitude + offsetDaBase);

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere((Vector2)transform.position + offsetDaBase, 0.1f);
    }
}
