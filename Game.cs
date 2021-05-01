using System;

namespace Cave_Adventure
{
    public class Game
    {
        public GameScreen Screen { get; private set; } = GameScreen.MainMenu;
        public event Action<GameScreen> ScreenChanged;

        public void SwitchOnArenas()
        {
            ChangeStage(GameScreen.Arenas);
        }

        public void SwitchOnMainMenu()
        {
            ChangeStage(GameScreen.MainMenu);
        }
        
        private void ChangeStage(GameScreen stage)
        {
            Screen = stage;
            ScreenChanged?.Invoke(stage);
        }
    }
}