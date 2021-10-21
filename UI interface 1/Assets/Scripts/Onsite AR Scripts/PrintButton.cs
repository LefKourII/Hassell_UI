using UnityEngine;

public class PrintButton : MonoBehaviour
{
    public void Print()
    {
        Debug.Log("Image Saved");
        ScreenCapture.CaptureScreenshot("ScreenCapture");
    }
}
