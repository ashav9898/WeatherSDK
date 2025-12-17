using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowToastMsg(string message)
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
