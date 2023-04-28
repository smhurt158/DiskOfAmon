using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileSystemDisplayBehavior : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject FolderView, LockedFolderView, TextView;
    [SerializeField] GameObject FileDisplay, blocker;
    [SerializeField] Sprite Documents, FireWall;
    string currScene = "";


    public void ChangeCurrentFile(File file)
    {
        FileManager.CurrentFile = file;
        DisplayCurrentFile();

    }
    public void GoBack()
    {
        if(currScene == "")
        {
            ChangeCurrentFile(FileManager.CurrentFile.ParentFile);
        }
        else
        {
            DisplayCurrentFile();
        }
    }
    public void DisplayCurrentFile()
    {
        if(currScene != "")
        {
            SceneManager.UnloadSceneAsync(currScene);
            currScene = "";
        }
        GetComponent<Image>().enabled = true;
        blocker.SetActive(false);

        gameObject.transform.parent.GetComponent<Image>().sprite = Documents;

        Debug.Log("Displaying");
        File f = FileManager.CurrentFile;
        foreach (Transform child in gameObject.transform)
        {
            if(child.gameObject.tag != "NoDelete")
            {
                Destroy(child.gameObject);
            }
        }
        if (f is Folder fo)
        {
            if (fo.Locked)
            {
                GameObject lfv = Instantiate(LockedFolderView, gameObject.transform);
            }
            else
            {
                GameObject fv = Instantiate(FolderView, gameObject.transform);
                foreach (File i in fo.Files)
                {
                    //Debug.Log("adding file");
                    GameObject fd = Instantiate(FileDisplay, fv.transform);
                    fd.GetComponent<FileDisplayBehavior>().File = i;
                }
            }
            
        }
        else if(f is TextDocument td)
        {
            GameObject tv = Instantiate(TextView, gameObject.transform);
            tv.transform.GetChild(0).GetComponent<TextFileBehavior>().TextDocument = td;
        }
    }

    public void DisplayFireWall(Folder f, DragAndDrop dad)
    {
        if(f is Folder fo && dad.AttackType != AttackType.Read)
        {
            if (fo.Locked) return;
        }
        gameObject.transform.parent.GetComponent<Image>().sprite = FireWall;
        blocker.SetActive(true);
        FireWallInfo.Width = dad.Width;
        FireWallInfo.Height = dad.Height;
        FireWallInfo.AttackType = dad.AttackType;

        currScene = f.FireWallScene;
        SceneManager.LoadScene(currScene, LoadSceneMode.Additive);
        GetComponent<Image>().enabled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (FileManager.CurrentFile is not Folder) return;
        DragAndDrop dad = eventData.pointerDrag.GetComponent<DragAndDrop>();

        DisplayFireWall((Folder)FileManager.CurrentFile, dad);
    }

    private void Start()
    {
        Folder fo = new Folder("Tester", null, false, "first 1");

            Folder j = new Folder("ImportantFiles", fo, true, "first");
                j.Files.Add(new TextDocument("blackmail.txt", j, "I have so much dirt on this guy that im going to totally share to the whole world unless this gets deleted"));
                j.Files.Add(new TextDocument("ss-number.txt", j, "123-45-6789"));


            Folder i = new Folder("Pictures", fo, true, "first 2");
                i.Files.Add(new TextDocument("embarrassing-snapshot-of-player-at-the-christmas-party.png", i, "Pretend there is a super embarrassing picture here. Please I only had two days"));
            
            Folder a = new Folder("AssortedFiles", fo, true, "first 3");
                a.Files.Add(new TextDocument("how-much-i-love-horses.txt", a, "As a man who loves horses, I can tell you that these majestic animals have a special place in my heart. There's something about the way they move and their gentle demeanor that just draws me to them.\r\n\r\nWhether it's watching them gallop through a field, riding them on a trail, or caring for them in the stable, being around horses fills me with a sense of peace and happiness that's hard to describe.\r\n\r\nThere's also a unique bond that forms between a horse and its rider. The trust and understanding that develops over time is a truly special experience.\r\n\r\nIn short, horses are amazing creatures and I feel lucky to have them in my life."));

        fo.Files.Add(j);
        fo.Files.Add(i);
        fo.Files.Add(a);

        fo.Files.Add(new TextDocument("text.txt", fo, "Hi there"));
        fo.Files.Add(new TextDocument("grocery-list.txt", fo, "1. Eggs"));
        


        ChangeCurrentFile(fo);
        FireWallInfo.OnSuccess = OnSuccess;
    }

    public void OnSuccess(AttackType at)
    {
        Debug.Log("adding corrupt");

        switch (at)
        {
            case AttackType.Read:
                if(FileManager.CurrentFile is Folder f)
                {
                    f.Locked = false;
                }
                break;
            case AttackType.Corrupt:
                Debug.Log("adding corrupt");

                if (FileManager.CurrentFile is Folder fo)
                {
                    Debug.Log("adding corrupt");
                    fo.Files.Add(new Executable("corrupt.exe", fo, AttackType.Corrupt));
                }
                break;
            case AttackType.Trash:
                if (FileManager.CurrentFile is Folder fol)
                {
                    fol.Files.Add(new Executable("trash.exe", fol, AttackType.Trash));
                }
                break;
            case AttackType.Nuke:
                if (FileManager.CurrentFile is Folder fold)
                {
                    fold.Files.Add(new Executable("nuke.exe", fold, AttackType.Nuke));
                }
                break;
        }
        DisplayCurrentFile();
    }
}
