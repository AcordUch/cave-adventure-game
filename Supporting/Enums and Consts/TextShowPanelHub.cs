using System;
using Cave_Adventure.Properties;
using Cave_Adventure.Views.Screens;

namespace Cave_Adventure
{
    public static class TextShowPanelHub
    {
        public static TextShowPanel CreateStoryIntroPanel()
        {
            var result = new TextShowPanel()
            {
                Title = {Visible = false},
                InnerTextLabel = {Text = Resources.BeginningOfStory},
                FirstButton = { Text = "Погодите, хочу назад", Visible = true},
                ThirdButton = { Text = "И глубже в тьму", Visible = true}
            };
            result.FirstButton.Click += Game.Instance.SwitchOnMainMenu;
            result.ThirdButton.Click += Game.Instance.SwitchOnTutorial1;
            return result;
        }
        
        public static TextShowPanel CreateEndGamePanel()
        {
            var result = new TextShowPanel()
            {
                Title = {Visible = false},
                InnerTextLabel = {Text = Resources.EndOfStory},
                ThirdButton = { Text = "Удачи, путник!", Visible = true}
            };
            result.ThirdButton.Click += Game.Instance.SwitchOnMainMenu;
            return result;
        }
        
        public static TextShowPanel CreateTutorial1Panel()
        {
            var result = new TextShowPanel()
            {
                Title = {Text = "Обучение"},
                InnerTextLabel = {Text = Resources.Tutorial1},
                FirstButton = { Text = "Погодите, хочу назад", Visible = true},
                SecondButton = {Text = "Хочу уже играть", Visible = true},
                ThirdButton = { Text = "Хмм, понял, давай дальше", Visible = true}
            };
            result.FirstButton.Click += Game.Instance.SwitchOnStoryIntroPanel;
            result.SecondButton.Click += Game.Instance.SwitchOnArenas;
            result.ThirdButton.Click += Game.Instance.SwitchOnTutorial2;
            return result;
        }
        
        public static TextShowPanel CreateTutorial2Panel()
        {
            var result = new TextShowPanel()
            {
                Title = {Text = "Обучение"},
                InnerTextLabel = {Text = Resources.Tutorial2},
                FirstButton = { Text = "Погодите, хочу назад", Visible = true},
                ThirdButton = { Text = "И зачем я на это согласился...", Visible = true}
            };
            result.FirstButton.Click += Game.Instance.SwitchOnTutorial1;
            result.ThirdButton.Click += Game.Instance.SwitchOnArenas;
            return result;
        }
    }
}