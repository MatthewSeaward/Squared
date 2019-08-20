using DataEntities;
using LevelLoader.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using static SquarePiece;

namespace LevelLoader
{
    class DummyLevelLoader : ILevelLoader
    {
        public Dictionary<string, Level[]> GetLevels()
        {
            var levelList = new List<Level>();


            levelList.Add(new Level()
            {
                colours = new Colour[] { Colour.Red, Colour.Green, Colour.LightBlue, Colour.Pink },
                Pattern = new string[] {
                                        "-xx---",
                                        "-xxx--",
                                        "xxxxxx",
                                        "xxxxxx",
                                        "xxxxxx",
                                        "-xxx--",
                                        "-xxx--",
                                        "-xxx--",
                                        "--xx--" }
            });

            levelList.Add(new Level()
            {
                colours = new Colour[] { Colour.Red, Colour.Grey, Colour.LightBlue, Colour.Pink },
                Pattern = new string[] {
                                        "-----",
                                        "xxxxx",
                                        "xxxxx",
                                        "xx-xx",
                                        "xx-xx",
                                        "xxxxx",
                                        "-----" }
            });

            levelList.Add(new Level()
            {
                colours = new Colour[] { Colour.Orange, Colour.Green },
                Pattern = new string[] {
                                        "--x--",
                                        "-xxx-",
                                        "xxxxx",
                                        "xx-xx",
                                        "xxxxx",
                                        "-xxx-",
                                        "--x--" }
            });

            var test = JsonUtility.ToJson(levelList.ToArray());

            var dict = new Dictionary<string, Level[]>();
            dict.Add("Demo", levelList.ToArray());
            return dict;
        }
    }
}
