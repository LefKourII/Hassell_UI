using UnityEngine;
using UnityEngine.UI;

public class DiagramChanger : MonoBehaviour
{
    public Image parallelKerfing;
    public Image angularKerfing;
    public Image radialKerfing;

    public void ChangeImage(int index)
    {
        switch (index)
        {
            case 0:
                parallelKerfing.gameObject.SetActive(true);
                angularKerfing.gameObject.SetActive(false);
                radialKerfing.gameObject.SetActive(false);
                break;
            case 1:
                parallelKerfing.gameObject.SetActive(false);
                angularKerfing.gameObject.SetActive(true);
                radialKerfing.gameObject.SetActive(false);
                break;
            case 2:
                parallelKerfing.gameObject.SetActive(false);
                angularKerfing.gameObject.SetActive(false);
                radialKerfing.gameObject.SetActive(true);
                break;
        }
    }
}
