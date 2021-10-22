using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARFunctionality : MonoBehaviour
{
    private ARRaycastManager rayManager;
    public GameObject visual;
    public Animator directionsAnimation;
    public TMP_Text directionText;
    
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual.SetActive(true);
    }

    void Update()
    {
        if(!ARData.activated)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2),
                hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;

                if (!visual.activeSelf)
                    visual.SetActive(false);
            }
            else
                visual.SetActive(false);
        }
    }

    public void LockVisual()
    {
        visual.SetActive(false);
        visual.transform.position = ARData.position;
        visual.transform.rotation = ARData.rotation;
    }

    public void FadeDirections()
    {
        directionsAnimation.SetTrigger("FadeOut");
        directionText.gameObject.SetActive(false);
    }
}
