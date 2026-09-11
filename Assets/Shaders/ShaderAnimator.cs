using UnityEngine;

public class ShaderAnimator : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;

    private float accumulatedTime = 0;

    void Update()
    {
        accumulatedTime += Time.deltaTime;

        if (targetMaterial != null)
        {
            targetMaterial.SetFloat("_DeltaTime", accumulatedTime);
        }
    }
}
