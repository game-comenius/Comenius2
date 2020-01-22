using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour 
{
    [SerializeField] Camera _camera = null;

    [SerializeField] private Vector3 footbaseOffset = Vector3.zero;

    private List<Vector2Int> path = null; //O path é armazenado de trás para frente, ou seja, path[0] é o destino e path[path.Count - 1] é a casa em que a Lurdinha está ou para qual ela está indo.

    [Range(0.01f, 0.7f)] [SerializeField] private float frameDuration = 0.1f;

    [SerializeField] private float velocity = 0.1f;

    [SerializeField] private SpriteRenderer spriteRenderer = null;

    //Sprites para a Lurdinha parada.
    [SerializeField] private Sprite forward = null;

    [SerializeField] private Sprite backward = null;

    [SerializeField] private Sprite left = null;

    [SerializeField] private Sprite right = null;

    //Sprites para a Lurdinha andando
    [SerializeField] private Sprite[] walkForward = new Sprite[4];

    [SerializeField] private Sprite[] walkBackward = new Sprite[4];

    [SerializeField] private Sprite[] walkLeft = new Sprite[4];

    [SerializeField] private Sprite[] walkRight = new Sprite[4];

    private Coroutine walkCoroutine = null; 

    private Vector3[] lookTo = null;

    public delegate void GotToInteractable();

    private static event GotToInteractable gotToInteractable; //Ações que vai ocorrer quando a Lurdinha chegar ao destino.

    private bool uiFoiUsada = false; //Server para guardar o estado da var GameManager.uiSendoUsada da frame anterior.

    //public bool hasTarget = false;

    private void Start()
    {
        StartCoroutine(WalkDecision());
    }

    //Só é recebida a ordem da movimentação no LateUpdate, para que um objeto interagível (ex.: porta, diálogo) possa mandar uma ordem no Update.
    private void LateUpdate()
    {
        if (!GameManager.uiSendoUsada && !uiFoiUsada)// && !hasTarget) //É verificado se a UI está sendo usada para não ser ativado durante o uso da UI,se foi usada na última frame para não ser ativado ao se fechar a UI
        {
            if(Input.GetMouseButtonUp(0))
            {
                if (walkCoroutine == null)
                {
                    NullifyGotToInteractable();

                    if (_camera == null) //É verificada a camera porque sempre que se mudar de cena haverá uma nova câmera. Isso só preciso pois a Lurdinha vai para o DontDestroyOnLoad e as câmeras não.
                    {
                        _camera = Camera.main;
                    }

                    path = GridScript.gridScript.FindPath(transform.position + footbaseOffset, _camera.ScreenToWorldPoint(Input.mousePosition));

                    if (path == null) //Caso não haja um path, quer dizer que o destino é onde a Lurdinha já está e ela apenas irá virar para a direção do clique.
                    {
                        Turn(_camera.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
                else
                {
                    NullifyGotToInteractable();

                    StartCoroutine(WaitFor());
                }
            }
        }

        uiFoiUsada = GameManager.uiSendoUsada;
    }
    
    public void NullifyGotToInteractable() 
    {
        gotToInteractable -= gotToInteractable;
    }

    //Lurdinha olha para uma direção
    private void Turn(Vector3 point)
    {
        Vector2Int position = GridScript.gridScript.P2G(point);

        List<Vector2Int> list = new List<Vector2Int>();

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    list.Add(position + Vector2Int.up);
                    break;
                case 1:
                    list.Add(position + Vector2Int.down);
                    break;
                case 2:
                    list.Add(position + Vector2Int.left);
                    break;
                case 3:
                    list.Add(position + Vector2Int.right);
                    break;
            }
        }

        List<Vector2> list2 = new List<Vector2>();

        for (int i = 0; i < 4; i++)
        {
            list2.Add(GridScript.gridScript.Cell(list[i])[0] - (Vector2)point);
        }

        int minor = 0;

        for (int i = 1; i < 4; i++)
        {

            if (list2[i].magnitude < list2[minor].magnitude) 
            {
                minor = i;
            }
        }

        switch (minor)
        {
            case 0:
                spriteRenderer.sprite = backward;
                break;
            case 1:
                spriteRenderer.sprite = forward;
                break;
            case 2:
                spriteRenderer.sprite = right;
                break;
            case 3:
                spriteRenderer.sprite = left;
                break;
        }
    }

    //Esse método será usado quando a lurdinha já estiver andando e for escolhida um novo destino. Então ela andará até a próxima casa e fará o novo caminho.
    private IEnumerator WaitFor()
    {
        if (path.Count > 1) //path.Count deve ser maior que 1. Se não for, indica que a próxima casa á é a última.
        {
            path.RemoveRange(0, path.Count - 2);
        }

        Vector3 pointer = _camera.ScreenToWorldPoint(Input.mousePosition);

        yield return new WaitUntil(() => walkCoroutine == null);

        if (_camera == null)
        {
            _camera = FindObjectOfType<Camera>();
        }

        path = GridScript.gridScript.FindPath(transform.position + footbaseOffset, pointer);

        if (path == null)
        {
            Turn(_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    //Espera ter um path para seguir.
    private IEnumerator WalkDecision()
    {
        yield return new WaitWhile(() => path == null);

        walkCoroutine = StartCoroutine(Walk());
    }
    
    //Animação de andar.
    private IEnumerator Walk()
    {
        float t = 0f;

        Sprite[] sprites = new Sprite[4];

        Vector2 newPosition = Vector2.zero;

        Vector2Int direction2 = Vector2Int.zero;

        while (path.Count > 0)
        {
            newPosition = GridScript.gridScript.Cell(path[path.Count - 1])[0] - (Vector2)footbaseOffset;

            Vector2 direction = (newPosition - (Vector2)transform.position).normalized;

            while (newPosition != (Vector2)transform.position)
            {
                transform.position = (Vector2)transform.position;

                int frame = Mathf.FloorToInt(t / frameDuration);

                if (frame > 3)
                {
                    t -= frame * frameDuration;

                    frame %= 4;
                }

                spriteRenderer.sprite = sprites[frame];

                transform.position = Vector3.Lerp(transform.position, newPosition, (velocity * Time.deltaTime) / (newPosition - (Vector2)transform.position).magnitude);

                transform.position += new Vector3(0, 0, transform.position.y + footbaseOffset.y);

                t += Time.deltaTime;

                yield return null;
            }

            path.RemoveAt(path.Count - 1);

            if (path.Count > 0)
            {
                direction2 = (path[path.Count - 1] - GridScript.gridScript.P2G(transform.position + footbaseOffset));

                if (direction2 == Vector2Int.up)
                {
                    sprites = walkBackward;
                }
                else if (direction2 == Vector2Int.down)
                {
                    sprites = walkForward;
                }
                else if (direction2 == Vector2Int.left)
                {
                    sprites = walkRight;
                }
                else if (direction2 == Vector2Int.right)
                {
                    sprites = walkLeft;
                }
            }
            else
            {
                if (direction2 == Vector2Int.up)
                {
                    spriteRenderer.sprite = backward;
                }
                else if (direction2 == Vector2Int.down)
                {
                    spriteRenderer.sprite = forward;
                }
                else if (direction2 == Vector2Int.left)
                {
                    spriteRenderer.sprite = right;
                }
                else if (direction2 == Vector2Int.right)
                {
                    spriteRenderer.sprite = left;
                }
            }
        }

        if (gotToInteractable != null)
        {
            gotToInteractable();

            gotToInteractable -= gotToInteractable;

            Vector3 point = lookTo[0];

            for (int i = 1; i < lookTo.Length; i++)
            {
                if ((point - transform.position).magnitude > (lookTo[i] - transform.position).magnitude)
                {
                    point = lookTo[i];
                }
            }

            Turn(point);

            lookTo = null;
        }

        path = null;

        StartCoroutine(WalkDecision());

        walkCoroutine = null;
    }

    //Começar a corotina WaitForInteractable
    public void WalkToInteractable(Vector3[] _destinoWorldPoint, GotToInteractable _event)
    {
        lookTo = _destinoWorldPoint;

        StartCoroutine(WaitForInteractable(_destinoWorldPoint, _event));
    }

    //Espera terminar de andar para interagir com o objeto.
    private IEnumerator WaitForInteractable(Vector3[] _destinoWorldPoint, GotToInteractable _event)
    {
        if (path != null && path.Count > 1)
        {
            path.RemoveRange(0, path.Count - 2);
        }

        yield return new WaitUntil(() => walkCoroutine == null);

        if (_camera == null)
        {
            _camera = FindObjectOfType<Camera>();
        }

        path = GridScript.gridScript.FindPathToInteractable(transform.position + footbaseOffset, _destinoWorldPoint);

        // Configurar as funções que serão executadas quando Lurdinha
        // chegar ao seu destino
        // Primeiro, o campo hasTarget = false para que os próximos comandos
        // de movimento não sejam ignorados (Lurdinha NÃO se movimenta se
        // ela tiver um alvo, ou seja, se hasTarget == true)
        //PathFinder.gotToInteractable += (() => hasTarget = false);
        // Então, a função relacionada ao seu objetivo será executada, ela foi
        // passada como o parâmetro _event
        PathFinder.gotToInteractable += _event;

        if (path == null)
        {
            Turn(_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    //Desenha no grid reto e no distorcido o caminho a ser feito.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + footbaseOffset, 0.1f);

        if (path != null && path.Count > 1)
        {
            Gizmos.color = Color.blue;

            for (int i = 1; i < path.Count; i++)
            {
                Gizmos.DrawLine(GridScript.gridScript.CellR(path[i - 1])[0], GridScript.gridScript.CellR(path[i])[0]);
                Gizmos.DrawLine(GridScript.gridScript.Cell(path[i - 1])[0], GridScript.gridScript.Cell(path[i])[0]);
            }

            Gizmos.DrawSphere(GridScript.gridScript.Cell(path[0])[0], 0.15f);
        }
    }
}
