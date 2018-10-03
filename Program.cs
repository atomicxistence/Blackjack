using System;
using System.Collections.Generic;
using System.Threading;

namespace Blackjack
{
    class Game
    {
        static void Main(string[] args)
        {
            //Intialize class objects
            Player player = new Player();
            Player dealer = new Player();
            Deck deck = new Deck();
            //Intitialize variables
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
            //Start the game
            Utility.GameScreen("BLACKJACK by atomic-games", "Press ENTER to continue");
            Utility.UserInput();
            //Main Game Engine
            while (isPlaying && player.chips > 0)
            {
                //Initialize Game Engine variables
                string hiddenCard;
                string shownCard;
                bool hit = true;
                bool gameReset = false;
                bool isAnswering = true;
                int bet = 0;
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
                //ask for bet
                do
                {
                    bet = GameLogic.ValidNumber(player.hand, player.chips, dealer.hand, "Place your bet!");
                } while (!GameLogic.IsBetValid(player.hand, player.chips, dealer.hand, bet));
                player.chips -= bet;
                //Player First Card
                player.hand += deck.Draw(currentDeck);
                player.numAces += GameLogic.AceCheck(deck.Draw(currentDeck));
                player.handValue += GameLogic.CardValue(deck.Draw(currentDeck));
                currentDeck = deck.RemoveCard(currentDeck);
                Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealing cards...");
                Thread.Sleep(sleepTime);
                //Double down - double the intitial bet and draw a single card
                if (player.handValue >= 9 && player.handValue <= 11 && player.chips >= bet)
                {
                    bool doubleDown = GameLogic.IsDoubleDown(player.hand, player.chips, dealer.hand, bet);
                    if (doubleDown)
                    {
                        player.chips -= bet;
                        bet += bet;
                        hit = false;
                    }
                }
                //Player Second Card
                player.hand += deck.Draw(currentDeck);
                player.numAces += GameLogic.AceCheck(deck.Draw(currentDeck));
                player.handValue += GameLogic.CardValue(deck.Draw(currentDeck));
                currentDeck = deck.RemoveCard(currentDeck);
                Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealing cards...");
                Thread.Sleep(sleepTime);
                //deal dealer 2 cards, one is unseen
                hiddenCard = deck.Draw(currentDeck);
                dealer.handValue += GameLogic.CardValue(hiddenCard);
                dealer.numAces += GameLogic.AceCheck(deck.Draw(currentDeck));
                currentDeck = deck.RemoveCard(currentDeck);
                dealer.hand += "?? ";
                Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealing cards...");
                Thread.Sleep(sleepTime);
                shownCard = deck.Draw(currentDeck);
                dealer.hand += deck.Draw(currentDeck);
                dealer.handValue += GameLogic.CardValue(deck.Draw(currentDeck));
                dealer.numAces += GameLogic.AceCheck(deck.Draw(currentDeck));
                currentDeck = deck.RemoveCard(currentDeck);
                Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealing cards...");
                Thread.Sleep(sleepTime);
                //BLACKJACK!
                if (player.handValue == blackjack)
                {
                    gamesWon += 1;
                    player.chips += (bet * 2);
                    Utility.GameScreen(player.hand, player.chips, dealer.hand, "BLACKJACK!", "Press ENTER to continue");
                    Utility.UserInput();
                    gameReset = true;
                }
                //player's turn
                while (hit && !gameReset)
                {
                    Utility.GameScreen(player.hand, player.chips, dealer.hand, "HIT or STAY?");
                    string response = Utility.UserInput();
                    switch (response.ToLower())
                    {
                        case "hit":
                            player.handValue += GameLogic.CardValue(deck.Draw(currentDeck));
                            player.hand += deck.Draw(currentDeck);
                            player.numAces += GameLogic.AceCheck(deck.Draw(currentDeck));
                            currentDeck = deck.RemoveCard(currentDeck);
                            break;
                        case "stay":
                            hit = false;
                            break;
                        default:
                            Utility.GameScreen(player.hand, player.chips, dealer.hand, "That's not one of the options", "Press ENTER to try again.");
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
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "Player bust!", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                }
                hit = true;
                //dealer's turn
                if (!gameReset)
                {
                    Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealer's Turn");
                    Thread.Sleep(sleepTime);
                    dealer.hand = hiddenCard + shownCard;
                    Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealer's Turn");
                    Thread.Sleep(sleepTime);
                    if (dealer.handValue >= dealerMin && dealer.handValue <= blackjack)
                    {
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealer STAYS");
                        Thread.Sleep(longSleepTime);
                        hit = false;
                    }
                }
                while (!gameReset)
                {
                    if (hit)
                    {
                        dealer.handValue += GameLogic.CardValue(deck.Draw(currentDeck));
                        dealer.hand += deck.Draw(currentDeck);
                        dealer.numAces += GameLogic.AceCheck(deck.Draw(currentDeck));
                        currentDeck = deck.RemoveCard(currentDeck);
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealing cards...");
                        Thread.Sleep(sleepTime);
                    }
                    if (dealer.handValue > blackjack && dealer.numAces > 0)
                    {
                        dealer.handValue -= 10;
                        dealer.numAces--;
                    }
                    if (dealer.handValue > blackjack && dealer.numAces == 0)
                    {
                        gamesWon += 1;
                        player.chips += (bet * 2);
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealer bust!", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                    if (dealer.handValue >= dealerMin && dealer.handValue <= blackjack)
                    {
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "Dealer STAYS");
                        Thread.Sleep(longSleepTime);
                        hit = false;
                    }
                    if (player.handValue > dealer.handValue && !hit)
                    {
                        gamesWon += 1;
                        player.chips += (bet * 2);
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "You won!", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                    if (player.handValue == dealer.handValue && !hit)
                    {
                        player.chips += bet;
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "Tie game", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                    if (player.handValue < dealer.handValue && !hit)
                    {
                        Utility.GameScreen(player.hand, player.chips, dealer.hand, "The house won...", "Press ENTER to continue");
                        Utility.UserInput();
                        gameReset = true;
                    }
                }
                //Game Reset
                isAnswering = true;
                numGames += 1;
                while (gameReset && isAnswering)
                {
                    string winRateAndChips = "You've won " + gamesWon + " of " + numGames + " games. You have " + player.chips + " chips.";
                    Utility.GameScreen("Would you like to play again? (y/n)", winRateAndChips);
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
            //Message if the player is broke and is still trying to play
            if (player.chips <= 0 && isPlaying)
            {
                Utility.GameScreen("You're broke!", "Better luck next time.");
                Thread.Sleep(longSleepTime);
            }
            Utility.GameScreen("GAME OVER", "Press ENTER to exit");
            Utility.UserInput();
            Environment.Exit(0);
        }
    }
}
