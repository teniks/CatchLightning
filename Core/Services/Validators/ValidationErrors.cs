namespace CatchLightning.Core.Services.Validators
{
    public enum AchievementError
    {
        NameRequired,
        NameDuplicated,
        GoalNotFound,
        CategoryNotFound,
        NotExist
    }

    public enum CategoryError
    {
        NameRequired,
        NameDuplicated,
        NotExist
    }

    public enum GoalError
    {
        NameRequired,
        NameDuplicated,
        NotExist
    }
}
