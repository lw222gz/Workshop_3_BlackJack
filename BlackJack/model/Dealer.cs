using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;


        private Card highestCard;


        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetSoft17Rule();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(m_deck, this, a_player);   
            }
            return false;
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                DealPlayerCard(true, a_player);
                // Checks if player score is equal to or bigger than 21 and if it is, then the player automaticly
                //Stands
                if (a_player.CalcScore() >= 21)
                {
                    Stand();
                }
                return true;
            }
            return false;
        }

        public void Stand()
        {
            if (m_deck != null)
            {
                ShowHand();

                while (m_hitRule.DoHit(this))
                {
                    DealPlayerCard(true, this);
                }
            } 
        }

        public bool IsDealerWinner(Player a_player)
        {
            if (a_player.CalcScore() > g_maxScore)
            {
                return true;
            }
            else if (CalcScore() > g_maxScore)
            {
                return false;
            }
            else if (a_player.CalcScore() == CalcScore())
            {
                //This is our own rule:
                //When the player and dealer ends up equal we made it so that the highest card 
                //on their hand decides the winner. If both have the same card value, let's say both got Ace as
                //highest then the card color decides the winner in order: spade > heart > diamon > club
                return HighestCard(a_player);
            }
            return  CalcScore() >= a_player.CalcScore();
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }

        public bool HighestCard(Player a_player)
        {
            int[] cardScores = getCardScoreArray();
            IEnumerable<Card> pHand = a_player.GetHand();
            IEnumerable<Card> dHand = GetHand();


            Card PlayerHighest = getHighestCardInHand(pHand);
            Card DealerHighest = getHighestCardInHand(dHand);

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

        public Card getHighestCardInHand(IEnumerable<Card> hand){
            int[] cardScores = getCardScoreArray();

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

        public void DealPlayerCard(bool hiddenCard, Player player)
        {
            Card c = m_deck.GetCard();
            c.Show(hiddenCard);
            player.DealCard(c);
        }
    }
}
