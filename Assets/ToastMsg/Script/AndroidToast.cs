using UnityEngine;

public static class AndroidToast
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static void Show(string message, bool longToast = false)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            AndroidJavaObject toast = toastClass.CallStatic<AndroidJavaObject>(
                "makeText",
                activity,
                message,
                longToast ? toastClass.GetStatic<int>("LENGTH_LONG")
                          : toastClass.GetStatic<int>("LENGTH_SHORT")
            );
            toast.Call("show");
        }));
    }
#else
    public static void Show(string message, bool longToast = false)
    {
        Debug.Log("Toast: " + message);
    }
#endif
}
