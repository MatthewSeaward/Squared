﻿using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void PowerupCountChanged(IPowerup powerup);
    public delegate void PieceCollectionComplete(Colour type);

    public class UserPowerupManager
    {
        public static PowerupCountChanged PowerupCountChanged;
        public static PieceCollectionComplete PieceCollectionComplete;

        public IPowerup[] EquippedPowerups = new IPowerup[] { new ExtraLife(), new SpecialPieces(), new PerformBestMove() };

        private static UserPowerupManager _instance;

        public static UserPowerupManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserPowerupManager();
                }
                return _instance;
            }
        }

        public List<PowerupCollection> Powerups = new List<PowerupCollection>();

        private UserPowerupManager()
        {
            PieceCollectionManager.PiecesCollectedEvent += CheckForCompletion;
        }
        
        ~UserPowerupManager()
        {
            PieceCollectionManager.PiecesCollectedEvent -= CheckForCompletion;

        }

        public void AddNewPowerup(IPowerup powerup)
        {
            var match = Powerups.FirstOrDefault(x => x.Powerup.GetType() == powerup.GetType());
            if (match == null)
            {
                Powerups.Add(new PowerupCollection() { Powerup = powerup, Count = 1 });
            }
            else
            {
                match.Count++;
            }
            UserIO.Instance.SavePowerupInfo();
            PowerupCountChanged?.Invoke(powerup);
        }

        public void UsePowerup(IPowerup powerup)
        {
            var match = Powerups.FirstOrDefault(x => x.Powerup.GetType() == powerup.GetType());
            if (match != null)
            {
               match.Count--;
               if (match.Count < 0)
               {
                   match.Count = 0;
               }
            }
            UserIO.Instance.SavePowerupInfo();
            PowerupCountChanged?.Invoke(powerup);
        }

        public int GetUses(IPowerup powerup)
        {
            var match = Powerups.FirstOrDefault(x => x.Powerup.GetType() == powerup.GetType());
            if (match != null)
            {
                return match.Count;
            }
            else
            {
                if (Debug.isDebugBuild)
                {
                    return 99;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal void EquipPowerup(IPowerup powerup, int slot)
        {
            EquippedPowerups[slot] = powerup;
            UserIO.Instance.SaveEquippedPowerupInfo();
        }

        private void CheckForCompletion(Colour PieceColour, int previous, int collected)
        {
            int increment = RemoteConfigHelper.GetCollectionInterval(PieceColour);

            var multiplier = (previous / increment) + 1;

            int powerupsEarned = NumberOfPowerupsEarned(previous, previous + collected, increment);
            for (int i = 0; i < powerupsEarned; i++)
            {
                PieceCollectionComplete?.Invoke(PieceColour);
                AddNewPowerup(PowerupFactory.GetPowerup(PieceColour));
            }
        }

        public bool PowerupEquipped(IPowerup powerup)
        {
           return EquippedPowerups.Any(x => x.GetType() == powerup.GetType());
        }

        public static int NumberOfPowerupsEarned(int previous, int totalCollected, int increment)
        {
            var oldAmount = previous / increment;
            var newAmount = totalCollected / increment;

            return newAmount - oldAmount;
        }
    }
}