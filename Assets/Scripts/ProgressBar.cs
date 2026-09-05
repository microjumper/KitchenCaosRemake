using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Color primaryColor = Color.yellow;
    [SerializeField] private Color secondaryColor = Color.red;

    private void OnEnable()
    {
        barImage.color = primaryColor;
        
        SetProgress(0f);
    }

    public void SetProgress(float progress)
    {
        if (progress >= 0 && progress <= 1)
        {
            barImage.fillAmount = progress;
        }
    }

    public void UseSecondaryColor()
    {
        barImage.color = secondaryColor;
    }
}
