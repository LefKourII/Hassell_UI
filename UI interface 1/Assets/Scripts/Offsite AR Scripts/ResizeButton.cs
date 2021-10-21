using UnityEngine;

public class ResizeButton : MonoBehaviour
{
    public GameObject pavilion;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = pavilion.transform.localScale;
    }

    public void ResizePavilion()
    {
        pavilion.transform.localScale = initialScale;
    }
}
