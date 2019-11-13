using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour 
{
    [SerializeField] Camera _camera = null;

    [SerializeField] private Vector3 footbaseOffset = Vector3.zero;

    private List<Vector2Int> path = null;

    [Range(0.01f, 0.7f)] [SerializeField] private float frameDuration = 0.1f;

    [SerializeField] private float velocity = 0.1f;

    [SerializeField] private SpriteRenderer spriteRenderer = null;

    [SerializeField] Sprite forward = null;

    [SerializeField] Sprite backward = null;

    [SerializeField] Sprite left = null;

    [SerializeField] Sprite right = null;

    [SerializeField] Sprite[] walkForward = new Sprite[4];

    [SerializeField] Sprite[] walkBackward = new Sprite[4];

    [SerializeField] Sprite[] walkLeft = new Sprite[4];

    [SerializeField] Sprite[] walkRight = new Sprite[4];

    Coroutine coroutine = null;

    private Vector3[] lookTo = null;

    public delegate void GotToInteractable();

    public static event GotToInteractable gotToInteractable;

    public bool hasTarget = false;

    private bool uiFoiUsada = false;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        StartCoroutine(WalkDecision());
    }

    private void LateUpdate()
    {
        if (!GameManager.uiSendoUsada && !uiFoiUsada && !hasTarget)
        {
            if (Input.GetMouseButtonUp(0) && coroutine == null)
            {
                NullifyGotToInteractable();

                //.DateTime t = System.DateTime.UtcNow;

                if (_camera == null)
                {
                    _camera = Camera.main;
                }

                path = GridScript.gridScript.FindPath(transform.position + footbaseOffset, _camera.ScreenToWorldPoint(Input.mousePosition));

                if (path == null)
                {
                    Turn(_camera.ScreenToWorldPoint(Input.mousePosition));
                }

                //System.TimeSpan s = System.DateTime.UtcNow - t;

                //Debug.Log(s.TotalMilliseconds + " ms");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                NullifyGotToInteractable();

                StartCoroutine(WaitFor());
            }
        }

        uiFoiUsada = GameManager.uiSendoUsada;
    }

    public void NullifyGotToInteractable()
    {
        gotToInteractable -= gotToInteractable;
    }

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

    private IEnumerator WaitFor()
    {
        if (path.Count > 1)
        {
            path.RemoveRange(0, path.Count - 2);
        }

        yield return new WaitUntil(() => coroutine == null);

        path = GridScript.gridScript.FindPath(transform.position + footbaseOffset, _camera.ScreenToWorldPoint(Input.mousePosition));

        if (path == null)
        {
            Turn(_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private IEnumerator WalkDecision()
    {
        yield return new WaitWhile(() => path == null);

        coroutine = StartCoroutine(Walk());
    }

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
                int frame = Mathf.FloorToInt(t / frameDuration);

                if (frame > 3)
                {
                    t -= frame * frameDuration;

                    frame %= 4;
                }

                spriteRenderer.sprite = sprites[frame];

                transform.position = Vector3.Lerp(transform.position, newPosition, (velocity * Time.deltaTime) / (newPosition - (Vector2)transform.position).magnitude);

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

        coroutine = null;
    }

    public void WalkToInteractable(Vector3[] _destinoWorldPoint)
    {
        path = GridScript.gridScript.FindPathToInteractable(transform.position + footbaseOffset, _destinoWorldPoint);

        lookTo = _destinoWorldPoint;
    }

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
