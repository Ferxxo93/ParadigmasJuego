using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum gameStatus
    {
        menu, game, win, lose
    }

    public class GameManager
    {
        private static GameManager instance;
        private gameStatus gameStage = gameStatus.menu;    // 0-Menu     1-Game     2-Win    3-Lose
     
        private Image mainMenu = Engine.LoadImage("assets/MainMenu.png");
        private Image loseScreen = Engine.LoadImage("assets/Lose.png");
        private Image winScreen = Engine.LoadImage("assets/Win.png");
        private LevelController levelController;

        public LevelController LevelController => levelController;

        public static GameManager Instance
        {
            get { 
            
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            levelController = new LevelController();
        }
        public void Update()
        {
            switch(gameStage)
            {
                case gameStatus.menu:
                    if (Engine.GetKey(Engine.KEY_ESP))
                    {
                        gameStage = gameStatus.game;
                    }
                    break;
                case gameStatus.game:
                    levelController.Update();
                    break;
                case gameStatus.win:
                    if (Engine.GetKey(Engine.KEY_1))
                    {
                        gameStage = gameStatus.menu;
                        levelController = new LevelController();
                    }
                    if (Engine.GetKey(Engine.KEY_2))
                    {
                        gameStage = gameStatus.game;
                        levelController = new LevelController();
                    }
                    break;
                case gameStatus.lose:
                    if(Engine.GetKey(Engine.KEY_1))
                    {
                        gameStage = gameStatus.menu;
                        levelController = new LevelController();
                    }
                    if (Engine.GetKey(Engine.KEY_2))
                    {
                        gameStage = gameStatus.game;
                        levelController = new LevelController();
                    }

                    break;
            }
        }

        public void Render()
        {
            switch (gameStage)
            {
                case gameStatus.menu:
                    Engine.Clear();
                    Engine.Draw(mainMenu,0,0);
                    Engine.Show();
                    break;
                case gameStatus.game:
                    levelController.Render();
                    break;
                case gameStatus.win:
                    Engine.Clear();
                    Engine.Draw(winScreen, 0, 0);
                    Engine.Show();
                    break;
                case gameStatus.lose:
                    Engine.Clear();
                    Engine.Draw(loseScreen, 0, 0);
                    Engine.Show();
                    break;
            }
        }

        public void ChangeGameStatus(gameStatus status)
        {
            gameStage = status;
        }
    }
}
