using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cave_Adventure
{
    public class Game
    {
        public GameScreen Screen { get; private set; } = GameScreen.MainMenu;
        public event Action<GameScreen> ScreenChanged;

        public void SwitchOnArenas(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.Arenas);
        }

        public void SwitchOnMainMenu(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.MainMenu);
        }

        public void SwitchOnLevelSelectionMenu(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.LevelSelectionMenu);
        }

        public void SwitchOnStoryIntroPanel(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.StoryIntro);
        }
        
        public void SwitchOnTutorial1(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.TutorialMenu1);
        }
        
        public void SwitchOnTutorial2(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.TutorialMenu2);
        }

        private void ChangeStage(GameScreen stage)
        {
            Screen = stage;
            ScreenChanged?.Invoke(stage);
        }
    }
}