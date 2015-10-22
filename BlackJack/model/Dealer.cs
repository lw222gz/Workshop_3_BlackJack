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
        private rules.ItieStrategy m_tieRule;

        private List<IGameobserver> m_observer;
        

        
        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_observer = new List<IGameobserver>();
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetSoft17Rule();
            //This is our rule. None shall disobey!
            m_tieRule = a_rulesFactory.GetTieRule();
        }

        public void AddSub(IGameobserver sub)
        {
            m_observer.Add(sub);
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
                //    //This is our own rule:
                //    //When the player and dealer ends up equal we made it so that the highest card 
                //    //on their hand decides the winner. If both have the same card value, let's say both got Ace as
                //    //highest then the card color decides the winner in order: spade > heart > diamon > club
                return m_tieRule.CalcWinner(a_player, this);
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

        public void DealPlayerCard(bool hiddenCard, Player player)
        {
            Card c = m_deck.GetCard();
            c.Show(hiddenCard);
            player.DealCard(c);
            foreach (IGameobserver o in m_observer)
            {
                o.Playerhasacard();
            }
        }
    }
}
