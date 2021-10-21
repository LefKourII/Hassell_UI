using UnityEngine;

public class RotateOnSwipe : MonoBehaviour
{
    public GameObject pavilion;
    private Transform pavilionTransform;
    
    // ----------- Screen Boundaries -----------
    public float screenLowerBoundary;
    public float screenUpperBoundary;
    
    // ----------- Touch Control Variables -----------
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
   
    // Rotation Variables
    private float rotationAmount;
    public float rotationMax;

    void Start()
    {
        pavilionTransform = pavilion.transform;
        rotationAmount = 0;
    }

    void Update()
    {
        SwipeManager();
    }
    
    void SwipeManager()
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

                if (currentSwipe.x < 0 && currentSwipe.y > -0.2f && currentSwipe.y < 0.2f)
                {
                    rotationAmount = Remap(currentSwipe.x, -0.5f, -rotationMax, 0, 0);
                }

                if (currentSwipe.x > 0 && currentSwipe.y > -0.2f && currentSwipe.y < 0.2f)
                {
                    rotationAmount = Remap(currentSwipe.x, 0, 0, 0.5f, rotationMax);
                }
                
                float pavilionYRotation = pavilionTransform.rotation.eulerAngles.y + rotationAmount;
                
                if(t.position.y < Screen.height * 0.5f + screenUpperBoundary && t.position.y > screenLowerBoundary)
                    pavilionTransform.transform.rotation = Quaternion.Euler(0, pavilionYRotation, 0);
            }
        }
    }
    

    // ----------- Helping Functions -----------
    float Remap(float val, float min1, float min2, float max1, float max2)
    {
        return (float) (val - min1) / (max1 - min1) * (max2 - min2) + min2;;
    }
}
