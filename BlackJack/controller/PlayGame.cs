using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame : model.IGameobserver
    {
        private model.Game a_game;
        private view.IView a_view;
        public PlayGame(model.Game a_Game, view.IView a_View)
        {
            a_game = a_Game;
            a_view = a_View;
            a_game.AddSub(this);

        }
        public bool Play()
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


        public void Playerhasacard(model.Card c)
        {
            System.Threading.Thread.Sleep(500);


            a_view.DisplayCard(c);
        }
    }
}
