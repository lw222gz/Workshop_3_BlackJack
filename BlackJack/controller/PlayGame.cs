using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame
    {
        public bool Play(model.Game a_game, view.IView a_view)
        {
            a_view.DisplayWelcomeMessage();
            
            a_view.DisplayDealerHand(a_game.GetDealerHand(), a_game.GetDealerScore());
            a_view.DisplayPlayerHand(a_game.GetPlayerHand(), a_game.GetPlayerScore());

            if (a_game.IsGameOver())
            {
                a_view.DisplayGameOver(a_game.IsDealerWinner());
            }

           

            switch ((view.Option)a_view.GetInput())
            {
                case view.Option.PlayNewRound:
                        a_game.NewGame();
                    break;

                case view.Option.Hit:
                        a_game.Hit();
                    break;

                case view.Option.Stand:
                        a_game.Stand();
                    break;

                case view.Option.Quit: 
                        return false;
                    
            }
            return true;   
        }
    }
}
