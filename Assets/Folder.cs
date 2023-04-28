using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Folder: File
{
    public List<File> Files = new List<File>();
    public bool Locked = true;
    public string FireWallScene;
    public Folder(string name, File parentFile, bool locked, string fireWallScene) : base(name, parentFile)
    {
        Locked = locked;
        FireWallScene = fireWallScene;
    }
}

