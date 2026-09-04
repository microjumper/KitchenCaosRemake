using System;

public sealed class CookingProcess
{
    public event Action ItemCooked;
    public event Action ItemBurned;

    public KitchenItemDefinition Cooked => recipe.Cooked;
    public KitchenItemDefinition Burned => recipe.Burned;

    public bool IsComplete => elapsedTime >= recipe.CookingTime;
    private bool IsBurned => elapsedTime >= recipe.CookingTime + recipe.Overtime;

    private readonly CookableItemDefinition recipe;
    private float elapsedTime;

    public CookingProcess(CookableItemDefinition recipe)
    {
        this.recipe = recipe;
        this.elapsedTime = 0f;
    }

    public void AdvanceTime(float deltaTime)
    {
        elapsedTime += deltaTime;
        
        if (IsBurned)
        {
            ItemBurned?.Invoke();
        }

        if (IsComplete)
        {
            ItemCooked?.Invoke();
        }
    }
}