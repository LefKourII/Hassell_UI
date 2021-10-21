using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnlargeGraph : MonoBehaviour
{
    public Image sourceImage;
    public Image enlargedImage;
    
    // ---------- Graphic Raycasting ----------
    public GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        TouchEvent();
    }

    void TouchEvent()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();

                graphicRaycaster.Raycast(pointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.name == sourceImage.gameObject.name)
                    {
                        float remappedValue = Remap(result.screenPosition.x, 0, 0, 
                            Screen.width, 1);
                        float enlargedImageXPosition = Remap(remappedValue, 0, enlargedImage.rectTransform.sizeDelta.x, 
                            1, 0);
                            
                        enlargedImage.gameObject.SetActive(true);
                        enlargedImage.transform.position = new Vector3(enlargedImageXPosition + 125.0f, 
                            enlargedImage.transform.position.y);
                    }
                }
            }
            
            if (touch.phase == TouchPhase.Ended)
            {
                enlargedImage.gameObject.SetActive(false);
            }
        }
    }
    
    // ----------- Helping Functions -----------
    float Remap(float val, float min1, float min2, float max1, float max2)
    {
        return (float) (val - min1) / (max1 - min1) * (max2 - min2) + min2;;
    }
}
