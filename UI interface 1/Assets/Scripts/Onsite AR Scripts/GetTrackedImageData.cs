using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class GetTrackedImageData : MonoBehaviour
{
    public ARSessionOrigin AROrigin;
    public GameObject generator;
    public ARFunctionality arFunctionality;
    
    void Start()
    {
        generator.SetActive(false);
        ARData.position = Vector3.zero;
        ARData.activated = false;
        //ActivateGenerator();
    }

    void Update()
    {
        if (AROrigin.trackablesParent.childCount > 0 && !ARData.activated)
        {
            ARData.position = AROrigin.trackablesParent.GetChild(0).position;
            ARData.rotation = AROrigin.trackablesParent.GetChild(0).rotation;

            ActivateGenerator();
            arFunctionality.LockVisual();
            arFunctionality.FadeDirections();
        }
    }

    private void ActivateGenerator()
    {
        generator.transform.position = ARData.position;
        generator.transform.rotation = ARData.rotation;
        generator.SetActive(true);
        ARData.activated = true;
    }
}
