namespace CatchLightning.Core.Services.Validators
{
    internal enum AchievementError
    {
        NameRequired,
        NameDuplicated,
        GoalNotFound,
        CategoryNotFound
    }

    internal enum CategoryError
    {
        NameRequired,
        NameDuplicated,
    }

    internal enum GoalError
    {
        NameRequired,
        NameDuplicated,
    }
}
