using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    private List<ARRaycastHit> s_Hits;

    [SerializeField]
    GameObject m_ObjectToPlace;

    [SerializeField]
    GameObject indicator;
    
    [SerializeField]
    GameObject guidancePanel;

    private bool objectPlaced;
    private GameObject instantiatedObject;
    
    //AR Scale Variables
    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 instatiatedScale;

    void Start()
    {
        objectPlaced = false;
        indicator.SetActive(false);
        
        s_Hits = new List<ARRaycastHit>();
    }

    void Update()
    {
        if (!objectPlaced)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            m_RaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2),
                hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                indicator.SetActive(true);

                indicator.transform.position = hits[0].pose.position;
                indicator.transform.rotation = hits[0].pose.rotation;
            }
            else
                indicator.SetActive(false);
        }

        if (Input.touchCount > 0 && !objectPlaced)
        {
            Touch touch = Input.GetTouch(index: 0);

            if (touch.phase == TouchPhase.Began)
            {
                s_Hits.Clear();
                
                if (m_RaycastManager.Raycast(touch.position, s_Hits, trackableTypes: TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    instantiatedObject = Instantiate(m_ObjectToPlace, hitPose.position, hitPose.rotation);
                    instantiatedObject.transform.Rotate(Vector3.up, 180);

                    instatiatedScale = instantiatedObject.transform.localScale;

                    indicator.gameObject.SetActive(false);

                    guidancePanel.SetActive(false);

                    objectPlaced = true;
                }
            }
        }

        ScaleControl();
    }

    void ScaleControl()
    {
        if (Input.touchCount == 2 && objectPlaced)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = instantiatedObject.transform.localScale;
            }
            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                var factor = currentDistance / initialDistance;
                instantiatedObject.transform.localScale = initialScale * factor;
            }

        }
    }
    
    public void ResizePavilion()
    {
        instantiatedObject.transform.localScale = instatiatedScale;
    }
}
