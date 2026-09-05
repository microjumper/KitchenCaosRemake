using System;

public sealed class CuttingProcess
{
    public event Action<float> CutProgressChanged;

    public KitchenItemDefinition Output => recipe.Output;
    public bool IsInProgress => cutsCompleted > 0 && !IsComplete;
    public bool IsComplete => cutsCompleted >= recipe.CutsRequired;

    private readonly SlicebleItemDefinition recipe;
    private int cutsCompleted = 0;

    public CuttingProcess(SlicebleItemDefinition recipe)
    {
        this.recipe = recipe;
    }

    public void Cut()
    {
        if (IsComplete)
        {
            return;
        }

        cutsCompleted++;

        var progress = (float)cutsCompleted / recipe.CutsRequired;
        CutProgressChanged?.Invoke(progress);
    }
}