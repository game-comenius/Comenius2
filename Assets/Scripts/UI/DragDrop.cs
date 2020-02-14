using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Drag and drop script para ser utilizado com objetos da UI/Canvas
public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    private Canvas canvas;

    private Transform originalParent;

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Vector3 originalScale;
    private CanvasGroup canvasGroup;

    private GameObject shadow;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();

        originalParent = transform.parent;

        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        // Canvas Group é adicionado ao objeto para alterar o alpha dele
        // enquanto arrastamos o objeto
        if (!GetComponent<CanvasGroup>())
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Deixa uma sombra pra trás
        shadow = Instantiate(this.gameObject, originalParent);
        shadow.GetComponent<CanvasGroup>().alpha = 0.6f;

        // Desabilitar o bloqueio de raycasts para que quando o jogador soltar
        // este objeto, o click atravesse o mesmo e acerte o objeto que estará
        // em baixo (que pode implementar um handler para o click)
        canvasGroup.blocksRaycasts = false;
        // O novo pai se torna o próprio canvas para que o jogador possa
        // arrastar este objeto sobre qualquer outro objeto do canvas
        this.transform.SetParent(canvas.transform);

        originalScale = this.transform.localScale;
        this.transform.localScale *= 1.3f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(shadow);
        canvasGroup.blocksRaycasts = true;
        this.transform.SetParent(originalParent);
        this.transform.localScale = originalScale;
        rectTransform.anchoredPosition = originalPosition;
    }
}
