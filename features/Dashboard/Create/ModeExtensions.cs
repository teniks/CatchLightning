using System;
using System.Collections.Generic;
using System.Text;
using static CatchLightning.features.Dashboard.Create.EditingPageViewModel;

namespace CatchLightning.features.Dashboard.Create
{
    public static class ModeExtenions
    {
        public static string ToDisplay(this EntityPage page, Mode mode) => mode switch
        {
            Mode.Create => page switch
            {
                EntityPage.Category => "Создание категории",
                EntityPage.Goal => "Создание задачи",
                EntityPage.Achievement => "Создание достижения",
            },
            Mode.Edit => page switch
            {
                EntityPage.Category => "Редактирование категории",
                EntityPage.Goal => "Редактирование задачи",
                EntityPage.Achievement => "Редактирование достижения",
            },
            _ => mode.ToString()
        };
    }
}
