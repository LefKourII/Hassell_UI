using UnityEngine;
using UnityEngine.UI;

public class AssemblyOperator : MonoBehaviour
{
    public Slider slider;
    public GameObject pavilion;
    public GameObject pavilionBase;
    private Transform pavilionBaseTransform;
    private Transform pavilionTransform;

    private float currentSliderValue;
    
    // ----------- Touch Control Variables -----------
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
   
    // Quaternion Angles
    private Quaternion initialAngle;
    private Quaternion rotationAngle;
    
    private Quaternion baseInitialAngle;
    private Quaternion baseRotationAngle;

    float rotationAmount;
    public float rotationSpeed;
    public float rotationMax;
    
    void Start()
    {
        pavilionTransform = pavilion.transform;
        pavilionBaseTransform = pavilionBase.transform;
        currentSliderValue = slider.value;
    }

    void Update()
    {
        AssemblySequence();
        //RotateOnSwipe();
    }

    void AssemblySequence()
    {
        if (slider.value != currentSliderValue)
        {
            float index = Remap(slider.value, 0, pavilionTransform.childCount, slider.maxValue, 0);

            for (int i = 0; i < pavilionTransform.childCount; i++)
            {
                if(i >= index)
                    pavilionTransform.GetChild(i).gameObject.SetActive(true);
                else
                    pavilionTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void RotateOnSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
                initialAngle = pavilionTransform.rotation;
                baseInitialAngle = pavilionBaseTransform.rotation;
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                rotationAmount = Remap(currentSwipe.x, 0, 0, 1, rotationMax);

                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    pavilionTransform.rotation =
                        Quaternion.Euler(pavilionTransform.rotation.eulerAngles.x,
                            pavilionTransform.rotation.eulerAngles.y - rotationAmount, 0);
                    pavilionBaseTransform.rotation =
                        Quaternion.Euler(pavilionBaseTransform.rotation.eulerAngles.x,
                            pavilionBaseTransform.rotation.eulerAngles.y - rotationAmount,
                            pavilionBaseTransform.rotation.eulerAngles.z);
                    Debug.Log("left swipe");
                }

                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    pavilionTransform.rotation =
                        Quaternion.Euler(pavilionTransform.rotation.eulerAngles.x,
                            pavilionTransform.rotation.eulerAngles.y + rotationAmount, 0);
                    pavilionBaseTransform.rotation =
                        Quaternion.Euler(pavilionBaseTransform.rotation.eulerAngles.x,
                            pavilionBaseTransform.rotation.eulerAngles.y + rotationAmount,
                            pavilionBaseTransform.rotation.eulerAngles.z);
                    Debug.Log("right swipe");
                }
            }
        }
    }

    // ----------- Helping Functions -----------
    float Remap(float val, float min1, float min2, float max1, float max2)
    {
        return (float) (val - min1) / (max1 - min1) * (max2 - min2) + min2;;
    }
}
