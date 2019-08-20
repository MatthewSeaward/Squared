﻿using System.Collections.Generic;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Entities;
using DataEntities;

namespace Assets.Scripts.Workers
{
    public class DialogueManager
    {
        public Dictionary<string, Barks> Barks;

        private static DialogueManager _instance;

        public static DialogueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DialogueManager();
                }

                return _instance;
            }
        }

        private Barks SelectedBarks => Barks[LevelManager.Instance.SelectedChapter];

        private DialogueManager()
        {
            IBarkLoader BarkIO = new BarkLoader();

            Barks = BarkIO.GetBarks();
        }

        public string[] GetAngryText()
        {
            return new string[] { SelectedBarks.Anger[UnityEngine.Random.Range(0, SelectedBarks.Anger.Length)] };
        }

        public string[] GetLevelText()
        {

            int currentStar = LevelManager.Instance.SelectedLevel.GetCurrentStar().Number;            

            var dialogue = LevelManager.Instance.SelectedLevel.DiaglogueItems;
            if (dialogue != null)
            {
                var text = GetDialogue(currentStar, dialogue);
                if (text != null)
                {
                    return text;
                }
            }                        
            
            var firstLevel = LevelManager.Instance.SelectedLevel.GetCurrentStar().Number <= 1;

            string[] barks;
            if (firstLevel)
            {
                barks = SelectedBarks.LevelStart;
            }
            else
            {
                barks = SelectedBarks.ChallengeIncrease;
            }

            return new string[] { barks[UnityEngine.Random.Range(0, barks.Length)] };            
        }

        private string[] GetDialogue(int currentStar, LevelDialogue dialogue)
        {
            switch (currentStar)
            {
                default:
                case 0:
                case 1:
                    return dialogue.Star1 != null ? dialogue.Star1 : null;
                case 2:
                    return dialogue.Star2 != null ? dialogue.Star2 : null;
                case 3:
                    return dialogue.Star3 != null ? dialogue.Star3 : null;
            }
        }
    }
}
