using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFileBehavior : MonoBehaviour
{
    public TextDocument TextDocument;
    private void Start()
    {
        GetComponent<Text>().text = TextDocument.Contents;
    }
}
