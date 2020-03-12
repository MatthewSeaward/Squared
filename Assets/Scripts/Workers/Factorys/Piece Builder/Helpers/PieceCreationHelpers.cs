using Assets.Scripts.Constants;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Factorys.Helpers
{
    public static class PieceCreationHelpers
    {
        public static (Sprite sprite, Colour colour) GetRandomSprite()
        {
            var permittedValues = new List<Colour>();
            if (LevelManager.Instance?.SelectedLevel?.Colours == null)
            {
                permittedValues.AddRange((Colour[])Enum.GetValues(typeof(Colour)));
                permittedValues.Remove(Colour.None);
            }
            else
            {
                permittedValues.AddRange((Colour[])LevelManager.Instance.SelectedLevel.Colours.ToArray());

                if (UnityEngine.Random.Range(0, 100) < GameSettings.ChanceToUseBannedPiece)
                {
                    var bannedSprite = LevelManager.Instance.SelectedLevel.BannedPiece();
                    if (bannedSprite >= 0)
                    {
                        permittedValues.Remove((Colour)bannedSprite);
                    }
                }
            }

            int selectedColour = (int)permittedValues[UnityEngine.Random.Range(0, permittedValues.Count)];

            return (GameResources.PieceSprites[selectedColour.ToString()], (Colour)selectedColour);
        }
    }
}
