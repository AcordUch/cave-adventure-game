using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cave_Adventure
{
    public class Game
    {
        private static Game _instance;
        public GameScreen CurrentScreen { get; private set; } = GameScreen.MainMenu;
        public event Action<GameScreen> ScreenChanged;
        public event Action<string> ChangedOnCertainArena;
        //public event Action ConfigureArenaPanelOnManyLevels;
        //public event Action ConfigureArenaPanelOneLevel;
        
        private Game(){}
        
        public static Game Instance => _instance ??= new Game();

        public void SwitchOnArenas(object sender, EventArgs e)
        {
            //ConfigureArenaPanelOnManyLevels?.Invoke();
            ArenaPanel.Instance.AdjustForLevelsCampaign();
            ChangeStage(GameScreen.Arenas);
        }

        public void SwitchOnArenas(string arena)
        {
            //ConfigureArenaPanelOneLevel?.Invoke();
            ArenaPanel.Instance.AdjustCustomLevel();
            ChangeStage(GameScreen.Arenas);
            ChangedOnCertainArena?.Invoke(arena);
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
        
        public void SwitchOnEndGame(object sender, EventArgs e)
        {
            SwitchOnEndGame();
        }
        
        public void SwitchOnEndGame()
        {
            ChangeStage(GameScreen.EndGame);
        }
        
        public void SwitchOnArenaGeneratorMenu(object sender, EventArgs e)
        {
            ChangeStage(GameScreen.ArenaGeneratorMenu);
        }

        private void ChangeStage(GameScreen stage)
        {
            CurrentScreen = stage;
            ScreenChanged?.Invoke(stage);
        }
    }
}