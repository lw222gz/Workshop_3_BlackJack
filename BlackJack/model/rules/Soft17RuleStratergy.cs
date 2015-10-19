using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class Soft17RuleStratergy : IHitStrategy
    {
        private int total;
        public bool DoHit(model.Player a_dealer)
        {
            int[] cardScores = a_dealer.getCardScoreArray();
            total = 0;

            if (a_dealer.CalcScore() < 17)
            {
                return true;
            }

            if (a_dealer.CalcScore() == 17){

                foreach (Card c in a_dealer.GetHand())
                {                   
                    if (c.GetValue() != Card.Value.Ace)
                    {
                        total += cardScores[(int)c.GetValue()];
                    }                      
                }
                //if this is true a soft 17 is active.
                if (total <= 6)
                {
                    return true;
                }             
            }

            return false;
        }
    }
}
