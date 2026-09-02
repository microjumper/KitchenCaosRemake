using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barImage;

    private void OnEnable() => SetProgress(0);

    public void SetProgress(float progress)
    {
        if (progress >= 0 && progress <= 1)
        {
            barImage.fillAmount = progress;
        }
    }
}
