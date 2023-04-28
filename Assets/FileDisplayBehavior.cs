using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileDisplayBehavior : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Sprite locked, unlocked, noLock, folder, corruptFolder, document, corruptDocument, image, nuke, bug, trash;
    public File File;
    private void Start()
    {
        //Debug.Log(gameObject.transform.childCount);
        if(File is Folder f)
        {
            
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = !IsCorrupt(f) ? folder: corruptFolder;
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = f.Locked ? locked : unlocked;
        }
        else if (File is Executable e)
        {
            if(e.AttackType == AttackType.Nuke)
            {  
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = nuke;
            }
            else if(e.AttackType == AttackType.Corrupt)
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = bug;

            }
            else if (e.AttackType == AttackType.Trash)
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = trash;

            }
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = noLock;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = File.Corrupt ? corruptDocument:document;
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = noLock;
        }
        gameObject.transform.GetChild(2).GetComponent<Text>().text = File.Name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(File is Executable e)
        {
            switch (e.AttackType)
            {
                case AttackType.Nuke:
                    ((Folder)e.ParentFile).Files = new List<File>();
                    gameObject.transform.parent.parent.GetComponent<FileSystemDisplayBehavior>().DisplayCurrentFile();
                    break;
                case AttackType.Corrupt:
                    foreach (File file in ((Folder)e.ParentFile).Files)
                    {
                        if(file is not Folder && file is not Executable)
                        {
                            file.Corrupt = true;
                        }
                    }
                    gameObject.transform.parent.parent.GetComponent<FileSystemDisplayBehavior>().DisplayCurrentFile();
                    break;
                case AttackType.Trash:
                    List<File> filesToDelete = new List<File>();
                    foreach (File file in ((Folder)e.ParentFile).Files)
                    {
                        if (file is not Folder)
                        {
                            filesToDelete.Add(file);
                        }
                    }
                    foreach (File file in filesToDelete)
                    {
                        ((Folder)e.ParentFile).Files.Remove(file);
                    }
                    gameObject.transform.parent.parent.GetComponent<FileSystemDisplayBehavior>().DisplayCurrentFile();
                    break;
            }
        }
        else
        {
            gameObject.transform.parent.parent.GetComponent<FileSystemDisplayBehavior>().ChangeCurrentFile(File);
        }
        Debug.Log("Clicked " + File.Name);
    }

    public bool IsCorrupt(Folder f)
    {
        foreach(File fi in f.Files)
        {
            if(fi is Folder fo)
            {
                if (!IsCorrupt(fo))
                {
                    return false;
                }
            }
            else if(fi is not Executable)
            {
                if (!fi.Corrupt)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
