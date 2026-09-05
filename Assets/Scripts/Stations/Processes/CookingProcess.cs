using System;

public sealed class CookingProcess
{
    private enum CookingStage { Cooking, Overtime, Finished }

    public event Action<float> CookingProgressChanged;

    public event Action ItemCooked;
    public event Action ItemBurned;

    public KitchenItemDefinition Cooked => recipe.Cooked;
    public KitchenItemDefinition Burned => recipe.Burned;

    public bool IsComplete => currentStage != CookingStage.Cooking;

    private readonly CookableItemDefinition recipe;
    private CookingStage currentStage;
    private float elapsedTime;

    public CookingProcess(CookableItemDefinition recipe)
    {
        this.recipe = recipe;
        currentStage = CookingStage.Cooking;
        elapsedTime = 0f;
    }

    public void AdvanceTime(float deltaTime)
    {
        elapsedTime += deltaTime;

        switch (currentStage)
        {
            case CookingStage.Cooking:
                CookingProgressChanged?.Invoke(elapsedTime / recipe.CookingTime);
                if (elapsedTime >= recipe.CookingTime)
                {
                    currentStage = CookingStage.Overtime;
                    ItemCooked?.Invoke();
                    elapsedTime = 0f;   // Reset elapsed time for overtime tracking
                }
                break;

            case CookingStage.Overtime:
                CookingProgressChanged?.Invoke(elapsedTime / recipe.Overtime);
                if (elapsedTime >= recipe.Overtime)
                {
                    currentStage = CookingStage.Finished;
                    ItemBurned?.Invoke();
                }
                break;

            case CookingStage.Finished:
                break;
        }
    }
}