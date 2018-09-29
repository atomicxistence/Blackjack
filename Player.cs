using System;
namespace Blackjack
{
    class Player
    {
        public string hand;
        public int handValue = 0;
        public int numAces = 0;

        public int CardValue(string card)
        {
            int cardValue = 0;
            string cardFace = card[0].ToString();
            switch (cardFace)
            {
                case "J":
                case "Q":
                case "K":
                    cardValue = 10;
                    break;
                case "A":
                    cardValue = 11;
                    break;
                default:
                    cardValue = Int32.Parse(cardFace);
                    break;
            }
            return cardValue;
        }
    }

}
