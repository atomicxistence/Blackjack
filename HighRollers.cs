using System;
using System.Collections.Generic;

namespace Blackjack
{
    [Serializable()]
    public class HighRollers
    {
        public int ChipTotal { get; set; }
        public string Name { get; set; }
        
        //Sort high rollers list to the top 5, get rid of the rest
        public static List<HighRollers> TopFive(List<HighRollers> highRollers)
        {
            SortList(highRollers);
            List<HighRollers> topFive = RemoveBottom(highRollers);
            return topFive;
        }

        //Sort a list from highest to lowest
        private static List<HighRollers> SortList(List<HighRollers> highRollers)
        {
            highRollers.Sort((x, y) => y.ChipTotal.CompareTo(x.ChipTotal));
            return highRollers;
        }

        //Remove bottom object of list
        private static List<HighRollers> RemoveBottom(List<HighRollers> highrollers)
        {
            while (highrollers.Count > 5)
            {
                highrollers.RemoveAt(highrollers.Count - 1);
            }
            return highrollers;
        }

        //Returns the value of the lowest high roller
        public static int ChipTotalToBeat(List<HighRollers> highRollers)
        { 
            var bottomHighRoller = highRollers[highRollers.Count - 1];
            int bottomHighRollerValue = bottomHighRoller.ChipTotal;
            return bottomHighRollerValue;
        }

        //Create a default list of High Rollers
        public static List<HighRollers> DefaultHighRollers()
        {
            HighRollers default1 = new HighRollers { ChipTotal = 5000, Name = "atomicxistence" };
            HighRollers default2 = new HighRollers { ChipTotal = 1000, Name = "God" };
            HighRollers default3 = new HighRollers { ChipTotal = 3000, Name = "Peewee Herman" };
            HighRollers default4 = new HighRollers { ChipTotal = 4000, Name = "David S Pumpkins" };
            HighRollers default5 = new HighRollers { ChipTotal = 2000, Name = "John Cena" };
            List<HighRollers> defaultHighRollers = new List<HighRollers>
            {
                default1,
                default2,
                default3,
                default4,
                default5
            };
            return SortList(defaultHighRollers);
        }
    }
}
