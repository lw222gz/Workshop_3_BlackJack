using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class Highestcardrule : ItieStrategy
    {
        private Card highestCard;

        public bool CalcWinner(Player a_player, Dealer a_dealer)
        {
            int[] cardScores = a_dealer.getCardScoreArray();
            IEnumerable<Card> pHand = a_player.GetHand();
            IEnumerable<Card> dHand = a_dealer.GetHand();


            Card PlayerHighest = getHighestCardInHand(pHand, a_dealer);
            Card DealerHighest = getHighestCardInHand(dHand, a_dealer);

            if (cardScores[(int)PlayerHighest.GetValue()] == cardScores[(int)DealerHighest.GetValue()])
            {
                //if the second parameter is higher value then false is returned, aka player win.
                return isHigherCardColorValue(DealerHighest.GetColor(), PlayerHighest.GetColor());
            }

            if (cardScores[(int)PlayerHighest.GetValue()] > cardScores[(int)DealerHighest.GetValue()])
            {
                //player won
                return false;
            }
            else
            {
                //dealer won.
                return true;
            }
        }

        public Card getHighestCardInHand(IEnumerable<Card> hand, Dealer a_dealer)
        {
            int[] cardScores = a_dealer.getCardScoreArray();

            int Highest = 0;
            highestCard = null;

            foreach (Card c in hand)
            {
                if (cardScores[(int)c.GetValue()] >= Highest)
                {
                    Highest = cardScores[(int)c.GetValue()];
                    if (highestCard == null)
                    {
                        highestCard = c;
                    }
                    else if (cardScores[(int)c.GetValue()] > cardScores[(int)highestCard.GetValue()])
                    {
                        highestCard = c;
                    }
                    else if (isHigherCardColorValue(c.GetColor(), highestCard.GetColor()))
                    {
                        highestCard = c;
                    }

                }
            }
            return highestCard;
        }

        //Returns @boolean to check if argument card color is higher than 2nd argument color.
        public bool isHigherCardColorValue(Card.Color FirstColor, Card.Color SecondColor)
        {
            switch (SecondColor)
            {
                case Card.Color.Spades:
                    //spades is highest so the new color cant be higher.
                    return false;


                case Card.Color.Hearts:
                    //if NewColor is spades then it's higher, no other colors can be higher tho.
                    if (FirstColor == Card.Color.Spades)
                    {
                        return true;
                    }
                    break;

                case Card.Color.Diamonds:
                    if (FirstColor == Card.Color.Spades || FirstColor == Card.Color.Hearts)
                    {
                        return true;
                    }
                    break;

                case Card.Color.Clubs:
                    //if highest is clubs then the new color is automaticly higher.
                    return true;

            }

            return false;
        }
    }
}
