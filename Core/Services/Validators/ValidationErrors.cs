namespace CatchLightning.Core.Services.Validators
{
    public enum AchievementError
    {
        NameRequired,
        NameDuplicated,
        GoalNotFound,
        CategoryNotFound
    }

    public enum CategoryError
    {
        NameRequired,
        NameDuplicated,
    }

    public enum GoalError
    {
        NameRequired,
        NameDuplicated,
    }
}
