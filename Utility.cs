using System;
using System.Threading;

namespace Blackjack
{
    public class Utility
    {
        //Centers the text, outputs the string passed to it
        public static void CenterText(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth / 2) - (text.Length / 2), Console.CursorTop);
            Console.WriteLine(text);
        }
        //Makes the cursor visible and returns the input
        public static string UserInput()
        {
            CenterCursor();
            Console.CursorVisible = true;
            string input = Console.ReadLine();
            Console.CursorVisible = false;
            return input;
        }
        //Centers the cursor
        public static void CenterCursor()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 7);
        }
        //Animation during "deck shuffling"
        public static void ShuffleAnimation(int cycles, int shuffleTime)
        {
            for (int i = 0; i < cycles; i++)
            {
                GameScreen("Shuffling \\");
                Thread.Sleep(shuffleTime);
                GameScreen("Shuffling |");
                Thread.Sleep(shuffleTime);
                GameScreen("Shuffling /");
                Thread.Sleep(shuffleTime);
                GameScreen("Shuffling --");
                Thread.Sleep(shuffleTime);
            }
        }
        //Displays the game screen with a single given string
        public static void GameScreen(string title)
        {
            TopBorder();
            Console.SetCursorPosition((Console.WindowWidth / 2) - (title.Length / 2), (Console.WindowHeight / 2));
            Console.WriteLine(title);
            BottomBorder();
        }
        //Displays the game screen with two given strings
        public static void GameScreen(string title, string subtitle)
        {
            TopBorder();
            Console.SetCursorPosition((Console.WindowWidth / 2) - (title.Length / 2), (Console.WindowHeight / 2));
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            CenterText(subtitle);
            Console.ResetColor();
            BottomBorder();
        }
        //Displays the game screen with player/dealer info
        public static void GameScreen(string playerHand, string dealerHand, string message)
        {
            TopBorder();
            PlayerInfo(playerHand);
            DealerInfo(dealerHand);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (message.Length / 2), (Console.WindowHeight / 2) - 5);
            Console.Write(message);
            BottomBorder();
        }
        //Displays the game screen with player/dealer info and an instruction
        public static void GameScreen(string playerHand, string dealerHand, string message, string subMessage)
        {
            TopBorder();
            PlayerInfo(playerHand);
            DealerInfo(dealerHand);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (message.Length / 2), (Console.WindowHeight / 2) - 5);
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - (subMessage.Length / 2), (Console.WindowHeight / 2) + 5);
            Console.Write(subMessage);
            BottomBorder();
        }
        //Creates an outer border of card suits
        private static string SuitBorderOuter(int width)
        {
            // u2660 SPADE
            // u2665 HEART
            // u2666 DIAMOND
            // u2663 CLUB
            string[] suits = new string[] { " \u2660 ", " \u2665 ", " \u2666 ", " \u2663 " };
            string border = "";
            string borderRevised;

            while (border.Length < width)
            {
                for (int i = 0; i < suits.Length; i++)
                {
                    border += suits[i];
                }
            }
            int extra = (border.Length - width);
            if (extra > 0)
            {
                borderRevised = border.Remove(border.Length - extra);
            }
            else
            {
                borderRevised = border;
            }
            return borderRevised;
        }
        //Creates an inner border of card suits
        private static string SuitBorderInner(int width)
        {
            string[] suits = new string[] { " \u2660 ", " \u2665 ", " \u2666 ", " \u2663 " };
            string border = "";
            string borderRevised;

            while (border.Length < width)
            {
                for (int i = 0; i < suits.Length; i++)
                {
                    border += suits[i];
                }
            }
            int extra = ((border.Length - width) + 3);
            if (extra > 0)
            {
                borderRevised = border.Remove(border.Length - extra);
            }
            else
            {
                borderRevised = border;
            }
            return borderRevised;
        }
        private static void TopBorder()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(SuitBorderOuter(Console.WindowWidth));
            Console.ResetColor();
            Console.SetCursorPosition(1, 2);
            Console.WriteLine(SuitBorderInner(Console.WindowWidth).PadLeft(3));
        }
        private static void BottomBorder()
        {
            Console.ResetColor();
            Console.SetCursorPosition(1, Console.WindowHeight - 2);
            Console.WriteLine(SuitBorderInner(Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(SuitBorderOuter(Console.WindowWidth));
            Console.ResetColor();
        }
        private static void PlayerInfo(string playerHand)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 20, (Console.WindowHeight / 2) - 1);
            Console.Write("Player");
            Console.ResetColor();
            Console.SetCursorPosition((Console.WindowWidth / 2) - 20, (Console.WindowHeight / 2));
            Console.Write("Hand: {0}", playerHand);
        }
        private static void DealerInfo(string dealerHand)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition((Console.WindowWidth / 2) + 10, (Console.WindowHeight / 2) - 1);
            Console.Write("Dealer");
            Console.ResetColor();
            Console.SetCursorPosition((Console.WindowWidth / 2) + 10, (Console.WindowHeight / 2));
            Console.Write("Hand: {0}", dealerHand);
        }
    }
}
