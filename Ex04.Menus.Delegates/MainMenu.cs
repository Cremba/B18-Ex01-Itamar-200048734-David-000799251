﻿using System.Collections.Generic;
using System;

namespace Ex04.Menus.Delegates
{
    public class MainMenu : Delegates
    {
        private static int m_Level = 0;
        public string Title { get => Label ; set => Label = value; }
        private List<Delegates> m_ListOfMainMenu = new List<Delegates>();
        private int m_index = 1;

        public MainMenu(string i_Title, int i_Level)
        {
            Title = i_Title;
            m_Level = i_Level;
        }

        public MainMenu newLevelMenu(string i_Title)
        {
            MainMenu mainMenu = new MainMenu(i_Title, m_Level++);
            m_ListOfMainMenu.Add(mainMenu);
            return mainMenu;
        }

        public Item NewItem(string i_ItemLabel)
        {
            Item item = new Item(i_ItemLabel);
            m_ListOfMainMenu.Add(item);
            return item;
        }

        private int getNumberFromUser()
        {
            int choiceFromUserAsNumber = 0;
            bool inputIsValid = false;
            while (!inputIsValid)
            {
                string inputFromUserAsString = Console.ReadLine();
                if(!int.TryParse(inputFromUserAsString, out choiceFromUserAsNumber))
                {
                    Console.WriteLine("Input needs to be a number ");
                }
                else if (choiceFromUserAsNumber == 0)
                {
                    if(m_Level == 1)
                    {
                        exit();
                    }
                    else
                    {

                        Console.WriteLine("You went back");
                    }
                }
                else if( choiceFromUserAsNumber < 1 || choiceFromUserAsNumber > m_ListOfMainMenu.Count)
                {
                    Console.WriteLine("Value needs to be between 1 and {0}", m_ListOfMainMenu.Count + 1);
                }
                else
                {
                    inputIsValid = true;
                }
            }
            return choiceFromUserAsNumber;
        }

        public override void Show()
        {
            bool inputIsRight = true;
            while (inputIsRight)
            {
                printMenu();
                int choice = getNumberFromUser();
                if (choice == 0)
                {
                    inputIsRight = false;
                    exit();
                }
                else
                {
                    m_ListOfMainMenu[choice - 1].Show();
                }
            }
        }

        private void exit()
        {
            Console.WriteLine("Thank you, have a good day");
            Console.ReadLine();
            Environment.Exit(200);
        }

        private void printMenu()
        {
            if(m_Level == 1)
            {
                Console.WriteLine("{0} :",Title);
            }
            else
            {
                Console.WriteLine("{0}. {1}", m_Level, Title);
            }

            m_index = 1;

            foreach (Delegates item in m_ListOfMainMenu)
            {
                Console.WriteLine("{0}. {1}", m_index, item.Label );
                m_index++;
            }
            Console.WriteLine("0. Back");
        }
    }
}