using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class AmericanNewGameStrategy : INewGameStrategy
    {
        public bool NewGame(Deck a_deck, Dealer a_dealer, Player a_player)
        {

            bool hiddenCard = true;
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    a_dealer.DealPlayerCard(hiddenCard, a_player);
                }
                else
                {
                    if (i == 3)
                    {
                        hiddenCard = false;
                    }
                    a_dealer.DealPlayerCard(hiddenCard, a_dealer);
                }
            }
            return true;
        }
    }
}
