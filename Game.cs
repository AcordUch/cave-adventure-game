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
        
        private void ChangeStage(GameScreen stage)
        {
            Screen = stage;
            ScreenChanged?.Invoke(stage);
        }
    }
}