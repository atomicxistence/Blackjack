using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Deck
    {
        //Creates a new deck of cards
        public List<string> New()
        {
            List<string> deck = new List<string>();
            string[] suits = new string[] { "\u2660 ", "\u2665 ", "\u2666 ", "\u2663 " };
            string[] faces = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K", "A" };
            string card = "";
            for (int x = 0; x < suits.Length; x++)
            {
                for (int i = 0; i < faces.Length; i++)
                {

                    card = faces[i] + suits[x];
                    deck.Add(card);
                }
            }
            return deck;
        }
        //Shuffle a given list and return it
        public List<string> Shuffle(List<string> deck)
        {
            Random shuffler = new Random();
            string temp;
            int rng;
            int numCards = deck.Count;
            for (int i = numCards - 1; i > 0; i--)
            {
                rng = shuffler.Next(0, i + 1);
                temp = deck[i];
                deck[i] = deck[rng];
                deck[rng] = temp;
            }
            return deck;
        }
        //Draw the top card from the deck
        public string Draw(List<string> deck)
        {
            string topCard = deck[0];
            return topCard;
        }
        //Removes the top card from the deck
        public List<string> RemoveCard(List<string> deck)
        {
            deck.RemoveAt(0);
            return deck;
        }
        //Checks to see if an Ace is drawn
        public int aceCheck(string card)
        {
            int aceNum;
            if (card[0].ToString() == "A")
            {
                aceNum = 1;
            }else
            {
                aceNum = 0;
            }
            return aceNum;
        }
    }
}
