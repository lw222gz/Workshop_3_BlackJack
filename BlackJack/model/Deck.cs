using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Deck
    {
        List<Card> m_cards;

        public Deck()
        {
            m_cards = new List<Card>();

            for (int colorIx = 0; colorIx < (int)Card.Color.Count; colorIx++)
            {
                for (int valueIx = 0; valueIx < (int)Card.Value.Count; valueIx++)
                {
                    Card c = new Card((Card.Color)colorIx, (Card.Value)valueIx);
                    AddCard(c);
                }
            }

            Shuffle();
        }

        public Card GetCard()
        {
            Card c = m_cards.First();
            m_cards.RemoveAt(0);
            return c;
        }

        public void AddCard(Card a_c)
        {
            m_cards.Add(a_c);
        }

        public IEnumerable<Card> GetCards()
        {
            return m_cards.Cast<Card>();
        }

        private void Shuffle()
        {
            //m_cards.RemoveAt(3);

            //test for our own rule when dealerscore == playerscore
            /*m_cards = new List<Card> { new Card(Card.Color.Hearts, Card.Value.King), new Card(Card.Color.Clubs, Card.Value.Ace), 
                                        new Card(Card.Color.Spades, Card.Value.Ace), new Card(Card.Color.Diamonds, Card.Value.King), 
                                        new Card(Card.Color.Clubs, Card.Value.Two)};*/

            //Test for soft 17
            /*m_cards.RemoveRange(0, 3);
            m_cards.RemoveRange(3, 6);
            m_cards.RemoveRange(4, 12);*/


            Random rnd = new Random();
             

            for (int i = 0; i < 1017; i++)
            {
                int index = rnd.Next() % m_cards.Count;
                Card c = m_cards.ElementAt(index);
                m_cards.RemoveAt(index);
                m_cards.Add(c); 
            }
        }
    }
}
