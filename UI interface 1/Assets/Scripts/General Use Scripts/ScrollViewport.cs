using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewport : MonoBehaviour
{
    public ScrollViewport scrollViewport;
    public Scrollbar verticalScrollbar;
    
    // ------- Swipe Variables -------
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector3 currentSwipe;

    private void Start()
    {
        verticalScrollbar.value = 0.5f;
    }

    void Update()
    {
        ScrollManager();
    }

    void ScrollManager()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Moved)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    verticalScrollbar.value = verticalScrollbar.value + currentSwipe.y;
                    if (verticalScrollbar.value > 1)
                        verticalScrollbar.value = 1;
                        
                    //Debug.Log("Swipe Down");
                }

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    verticalScrollbar.value = verticalScrollbar.value - currentSwipe.y;
                    if (verticalScrollbar.value < 0)
                        verticalScrollbar.value = 0;
                    
                    //Debug.Log("Swipe Up");
                }
            }
        }
    }
}
