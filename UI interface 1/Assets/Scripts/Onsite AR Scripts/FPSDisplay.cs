using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = this.gameObject.GetComponent<TMP_Text>();
        InvokeRepeating("FPSUpdate", 0, 1.0f);
    }

    void FPSUpdate()
    {
        text.text = "FPS: " + ((int) (1 / Time.deltaTime)).ToString();
    }
}
