﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public class GameManager : IGameManager
    {
        public int CurrentLevel { get; set; }
        public static List<ScoreData> ScoreData { get; set; }

        private static GameManager _instance;
        private static ICommand _saveCommand;

        public GameManager()
        {
            ScoreData = new List<ScoreData>();
            CurrentLevel = 0;
        }

        public static GameManager Instance
        {
            get
            {
                if (_instance is null) //lazy init
                {
                    _instance = new GameManager();

                    //if (_saveCommand is null)
                    //{
                    //    _saveCommand = new SaveGameCommand(_instance);
                    //}
                }


                return _instance;
            }
        }
        public IGameManagerMemento Save()
        {
            return new GameManagerMemento(CurrentLevel, ScoreData);
        }

        public void Restore(IGameManagerMemento memento)
        {
            if (!(memento is GameManagerMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            CurrentLevel = memento.GetLevel();
            ScoreData = memento.GetScoreData();
        }


        public void ShowScores()
        {
            //throw new NotImplementedException();
            //return _ranking;
            foreach(var data in ScoreData)
            {
                Console.WriteLine(data.Score);
            }
        }
    }
}
