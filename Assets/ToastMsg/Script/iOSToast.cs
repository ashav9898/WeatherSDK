using UnityEngine;
using System.Runtime.InteropServices;

public static class iOSToast
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowToast(string message);

    public static void Show(string message)
    {
        ShowToast(message);
    }
#else
    public static void Show(string message)
    {
        Debug.Log("Toast: " + message);
    }
#endif
}
