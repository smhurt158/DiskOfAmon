using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackType
{
    Read,
    Corrupt,
    Trash,
    Nuke
}
public delegate void OnSuccess(AttackType attack);
public static class FireWallInfo
{
    public static int Width, Height;
    public static bool success = false;
    public static OnSuccess OnSuccess;
    public static AttackType AttackType;
}
