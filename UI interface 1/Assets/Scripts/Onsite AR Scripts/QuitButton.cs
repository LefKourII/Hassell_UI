using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitApp()
    {
        Debug.Log("Quit Application!");
        Application.Quit();
    }
}
