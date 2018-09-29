using System;
using System.Collections.Generic;

namespace Blackjack
{
    public static class GameLogic
    {
        //Converts the card string to a number value
        public static int CardValue(string card)
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
        //Checks to see if an Ace is drawn
        public static int AceCheck(string card)
        {
            int aceNum = 0;
            if (card[0].ToString() == "A")
            {
                aceNum = 1;
            }
            return aceNum;
        }
        //Evaluates if input is a number
        public static int ValidNumber(string playerHand, int playerChips, string dealerHand, string message)
        {
            int number;
            bool valid = false;
            do
            {
                Utility.GameScreen(playerHand, playerChips, dealerHand, message);
                string numInput = Utility.UserInput();
                if (Int32.TryParse(numInput,out number))
                {
                    valid = true;
                }
                else
                {
                    Utility.GameScreen(playerHand, playerChips, dealerHand, "Please enter a valid number", "Press ENTER to try again");
                    Utility.UserInput();
                }
            } while (!valid);
            return number;
        }
        //Evaluates if a player can legally bet the amount they have entered
        public static bool IsBetValid(string playerHand, int playerChips, string dealerHand, int bet)
        {
            bool valid = false;
            if (bet <= 0)
            {
                Utility.GameScreen(playerHand, playerChips, dealerHand, "Please enter a bet greater than 0", "Press ENTER to try again");
                Utility.UserInput();
            }
            if (bet > playerChips)
            {
                Utility.GameScreen(playerHand, playerChips, dealerHand, "You do not have enough chips for that bet", "Press ENTER to try again");
                Utility.UserInput();
            }
            valid |= (bet > 0 && bet <= playerChips);
            return valid;
        }
    }
}
