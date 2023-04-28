using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    RectTransform rectTransform;
    Vector3 startingPosition;
    CanvasGroup canvasGroup;
    [SerializeField]
    public int Width, Height;

    [SerializeField]
    public AttackType AttackType;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startingPosition;
        Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
