using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class File
{
    public string Name;
    public File ParentFile;
    public bool Corrupt = false;
    public File(string name, File parentFile)
    {
        Name = name;
        ParentFile = parentFile;
        
    }
}
