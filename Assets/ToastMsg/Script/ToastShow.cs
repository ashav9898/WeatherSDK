using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToastShow
{
    public static void ShowToastMsg(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidToast.Show(message);
#elif UNITY_IOS && !UNITY_EDITOR
        iOSToast.Show(message);
#else
        Debug.Log("Toast: " + message);
#endif
    }
}
