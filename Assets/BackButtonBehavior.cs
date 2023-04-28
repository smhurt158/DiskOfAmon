using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButtonBehavior : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Moving Back");
        FileSystemDisplayBehavior fdb = gameObject.transform.parent.GetComponent<FileSystemDisplayBehavior>();
        fdb.GoBack();
    }
}
