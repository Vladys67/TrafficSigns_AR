using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CameraPermissionManager : MonoBehaviour
{
    public GameObject permissionPanel;

    void Start()
    {
        CheckCameraPermission();
    }

    public void CheckCameraPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            if (permissionPanel != null)
                permissionPanel.SetActive(true);

            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            if (permissionPanel != null)
                permissionPanel.SetActive(false);
        }
#else
        if (permissionPanel != null)
            permissionPanel.SetActive(false);
#endif
    }

   
    public void RetryPermission()
    {
        CheckCameraPermission();
    }
    public void OpenAppSettings()
    {
#if UNITY_ANDROID
        using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var intent = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS"))
        using (var uriClass = new AndroidJavaClass("android.net.Uri"))
        using (var uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", Application.identifier, null))
        {
            intent.Call<AndroidJavaObject>("setData", uriObject);
            currentActivity.Call("startActivity", intent);
        }
#endif
    }
}