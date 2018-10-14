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
                if (Int32.TryParse(numInput, out number))
                {
                    valid = true;
                }
                else
                {
                    Utility.GameScreen(playerHand, playerChips, dealerHand, "Please enter a valid number", "Press ENTER to try again");
                    Console.ReadLine();
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
                Console.ReadLine();
            }
            if (bet > playerChips)
            {
                Utility.GameScreen(playerHand, playerChips, dealerHand, "You do not have enough chips for that bet", "Press ENTER to try again");
                Console.ReadLine();
            }
            valid |= (bet > 0 && bet <= playerChips);
            return valid;
        }
        //Asks if a player wants to double down
        public static bool IsDoubleDown(string playerHand, int playerChips, string dealerHand, int bet)
        {
            bool isAnswering = true;
            bool doubleDown = false;
            string instructions = "You will double your bet of " + bet + " and draw only one more card.";
            while (isAnswering)
            {
                Utility.GameScreen(playerHand, playerChips, dealerHand, "Do you want to double down? (y/n)", instructions);
                string response = Utility.UserInput();
                switch (response.ToLower())
                {
                    case "y":
                        doubleDown = true;
                        isAnswering = false;
                        break;
                    case "n":
                        isAnswering = false;
                        break;
                    default:
                        Utility.GameScreen(playerHand, playerChips, dealerHand, "That's not one of the options.", "Press ENTER to try again");
                        Console.ReadLine();
                        break;
                }
            }
            return doubleDown;
        }
    }
}
