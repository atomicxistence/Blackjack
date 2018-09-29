using System;
using System.Collections.Generic;
using System.Threading;

namespace Blackjack
{
    class Game
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Player dealer = new Player();
            Deck deck = new Deck();
            int blackjack = 21;
            int dealerMin = 17;
            int lowDeck = 15;
            int numGames = 0;
            int gamesWon = 0;
            int sleepTime = 1000;
            int longSleepTime = 2000;
            int shuffleTime = 100;
            bool isPlaying = true;
            List<string> currentDeck = new List<string>();
            Console.CursorVisible = false;
            Utility.GameScreen("BLACKJACK by atomic-games", "Press ENTER to continue");
            Utility.UserInput();
            while (isPlaying)
            {
                string hiddenCard;
                string shownCard;
                bool hit = true;
                bool gameReset = false;
                player.hand = "";
                dealer.hand = "";
                player.handValue = 0;
                dealer.handValue = 0;
                player.numAces = 0;
                dealer.numAces = 0;
                //Shuffle a new deck or if the deck is getting low
                if (numGames == 0)
                {
                    currentDeck = deck.Shuffle(deck.New());
                    Utility.ShuffleAnimation(5, shuffleTime);
                }
                if (numGames > 0 && currentDeck.Count < lowDeck)
                {
                    currentDeck = deck.Shuffle(deck.New());
                    Utility.ShuffleAnimation(5, shuffleTime);
                }

                //deal player 2 cards
                for (int i = 0; i < 2; i++)
                {
                    player.hand += deck.Draw(currentDeck);
                    player.numAces += deck.aceCheck(deck.Draw(currentDeck));
                    player.handValue += player.CardValue(deck.Draw(currentDeck));
                    currentDeck = deck.RemoveCard(currentDeck);
                    Utility.GameScreen(player.hand, dealer.hand, "Dealing cards...");
                    Thread.Sleep(sleepTime);
                }
                //deal dealer 2 cards, one is unseen
                hiddenCard = deck.Draw(currentDeck);
                dealer.handValue += dealer.CardValue(hiddenCard);
                dealer.numAces += deck.aceCheck(deck.Draw(currentDeck));
                currentDeck = deck.RemoveCard(currentDeck);
                dealer.hand += "?? ";
                Utility.GameScreen(player.hand, dealer.hand, "Dealing cards...");
                Thread.Sleep(sleepTime);
                shownCard = deck.Draw(currentDeck);
                dealer.hand += deck.Draw(currentDeck);
                dealer.handValue += dealer.CardValue(deck.Draw(currentDeck));
                dealer.numAces += deck.aceCheck(deck.Draw(currentDeck));
                currentDeck = deck.RemoveCard(currentDeck);
                Utility.GameScreen(player.hand, dealer.hand, "Dealing cards...");
                Thread.Sleep(sleepTime);
                if (player.handValue == blackjack)
                {
                    gamesWon += 1;
                    Utility.GameScreen(player.hand, dealer.hand, "BLACKJACK!", "Press ENTER to continue");
                    Utility.UserInput();
                    gameReset = true;
                }
                //player's turn
                while (hit && !gameReset)
                {
                    Utility.GameScreen(player.hand, dealer.hand, "HIT or STAY?");
                    string response = Utility.UserInput();
                    switch (response.ToLower())
                    {
                        case "hit":
                            player.handValue += player.CardValue(deck.Draw(currentDeck));
                            player.hand += deck.Draw(currentDeck);
                            player.numAces += deck.aceCheck(deck.Draw(currentDeck));
                            currentDeck = deck.RemoveCard(currentDeck);
                            break;
                        case "stay":
                            hit = false;
                            break;
                        default:
                            Utility.GameScreen(player.hand, dealer.hand, "That's not one of the options. Press ENTER to try again.");
                            Utility.UserInput();
                            break;
                    }
                    if (player.handValue > blackjack && player.numAces > 0)
                    {
                        player.handValue -= 10;
                        player.numAces--;
                    }
                    if (player.handValue > blackjack && player.numAces == 0)
                    {
                        Utility.GameScreen(player.hand, dealer.hand, "Player bust!", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                }
                if (!gameReset)
                {
                    dealer.hand = hiddenCard + shownCard;
                    Utility.GameScreen(player.hand, dealer.hand, "Dealer's Turn");
                    Thread.Sleep(longSleepTime);
                    if (dealer.handValue == blackjack)
                    {
                        gameReset = true;
                        Utility.GameScreen(player.hand, dealer.hand, "Dealer STAYS");
                        Thread.Sleep(longSleepTime);
                    }
                }
                //dealer's turn
                hit = true;
                while (!gameReset)
                {
                    dealer.handValue += dealer.CardValue(deck.Draw(currentDeck));
                    dealer.hand += deck.Draw(currentDeck);
                    dealer.numAces += deck.aceCheck(deck.Draw(currentDeck));
                    currentDeck = deck.RemoveCard(currentDeck);
                    Utility.GameScreen(player.hand, dealer.hand, "Dealing cards...");
                    Thread.Sleep(sleepTime);
                    if (dealer.handValue >= dealerMin && dealer.handValue <= blackjack)
                    {
                        Utility.GameScreen(player.hand, dealer.hand, "Dealer STAYS");
                        Thread.Sleep(longSleepTime);
                        hit = false;
                    }
                    if (dealer.handValue > blackjack && dealer.numAces > 0)
                    {
                        dealer.handValue -= 10;
                        dealer.numAces--;
                    }
                    if (dealer.handValue > blackjack && dealer.numAces == 0)
                    {
                        gamesWon += 1;
                        Utility.GameScreen(player.hand, dealer.hand, "Dealer bust!", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                    if (player.handValue > dealer.handValue && !hit)
                    {
                        gamesWon += 1;
                        Utility.GameScreen(player.hand, dealer.hand, "You won!", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                    if (player.handValue == dealer.handValue && !hit)
                    {
                        Utility.GameScreen(player.hand, dealer.hand, "Tie game", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                    if (player.handValue < dealer.handValue && !hit)
                    {
                        Utility.GameScreen(player.hand, dealer.hand, "The house won...", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                }
                //Game Reset
                bool isAnswering = true;
                numGames += 1;
                while (gameReset && isAnswering)
                {
                    string winRate = "You've won " + gamesWon + " out of " + numGames + " games.";
                    Utility.GameScreen("Would you like to play again? (y/n)", winRate);
                    switch (Utility.UserInput().ToLower())
                    {
                        case "y":
                            isAnswering = false;
                        break;
                        case "n":
                            isPlaying = false;
                            isAnswering = false;
                        break;
                        default:
                            Utility.GameScreen("That's not one of the options. Press ENTER to try again.");
                            Utility.UserInput();
                            break;
                    }
                }
            }
            Console.Clear();
            Utility.GameScreen("GAME OVER", "Press ENTER to exit");
            Utility.UserInput();
            Environment.Exit(0);
        }
    }
}
