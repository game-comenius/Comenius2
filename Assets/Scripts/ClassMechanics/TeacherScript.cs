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

    [SerializeField] private float frameLonging = 0.3f;

    [SerializeField] private Sprite[] goRight = new Sprite[4];

    [SerializeField] private Sprite[] goLeft = new Sprite[4];

    private bool goingRight = false;

    private Coroutine walk = null;

    [SerializeField] private Sprite[] sprites = new Sprite[4];

    private SpriteRenderer sprite = null;

    [SerializeField] private Vector2 stopTimeRange;

    [SerializeField] private GameObject canvas;

    [SerializeField] private Camera _camera;

    //[SerializeField] private GameObject balao;

    //private GameObject balaoIns;

    //private TMP_Text text;

    //[SerializeField] private float speechWait;

    [SerializeField] private Vector2 offset;

    //private Coroutine speech;

    protected void Awake()
    {
        teacher = this;

        ClassManager.EndClass += PauseWalk;
    }

    private void OnDestroy()
    {
        ClassManager.EndClass -= PauseWalk;
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
            //balaoIns = Instantiate(balao, _camera.WorldToScreenPoint((Vector2)transform.position + offset), balao.transform.rotation, canvas.transform);

            //text = balaoIns.GetComponentInChildren<TMP_Text>();

            walk = StartCoroutine(Walk());

            //speech = StartCoroutine(Speak());
        }
    }

    private void Gerar()
    {
        float novaPos = Random.value;

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

            //balaoIns.transform.position = _camera.WorldToScreenPoint((Vector2)transform.position + offset);

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

                transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            }
            else
            {
                if (frame >= goLeft.Length)
                {
                    t -= frame * frameLonging;

                    frame = frame % goLeft.Length;
                }

                GetComponent<SpriteRenderer>().sprite = goLeft[frame];

                transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            }

            if ((Vector2)transform.position == inicio + vector * posicao) 
            {
                sprite.sprite = sprites[0];

                transform.localScale = new Vector3(1f, 1f, 1f);

                yield return new WaitForSeconds(Random.Range(stopTimeRange.x, stopTimeRange.y));

                Gerar();

                t = 0;
            }
        }
    }

    //private IEnumerator Speak()
    //{
    //    text.text = "...";

    //    while (true) 
    //    {
    //        yield return new WaitForSeconds(speechWait);

    //        text.text = "";

    //        yield return new WaitForSeconds(speechWait);

    //        text.text = ".  ";

    //        yield return new WaitForSeconds(speechWait);

    //        text.text = ".. ";

    //        yield return new WaitForSeconds(speechWait);

    //        text.text = "...";
    //    }
    //}

    public void PauseWalk()
    {
        posicao = ((Vector2)transform.position - inicio).x / vector.x;

        StopCoroutine(walk);

        sprite.sprite = sprites[0];

        walk = null;

        //StopCoroutine(speech);

        //Destroy(balaoIns);
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
