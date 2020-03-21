using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Assets.Scripts.Editor.Workers.Daily_Challenge
{
    [Category("Daily Challenge")]
    public class RandomTest
    {

        [Test]
        public void CompareTwoRandoms_FirstNumbers()
        {
            var firstRnd = GetRandom();
            var secondRnd = GetRandom();

            var firstNum = firstRnd.Next();
            var secondNum = secondRnd.Next();

            Assert.AreEqual(firstNum, secondNum);
        }
        
        [Test]
        public void CompareTwoRandoms_MulitpleNumbers()
        {
            var firstRnd = GetRandom();
            var secondRnd = GetRandom();

            for (int i = 0; i < 10; i++)
            {            
                var firstNum = firstRnd.Next();
                var secondNum = secondRnd.Next();

                Assert.AreEqual(firstNum, secondNum);
            }
        }

        [Test]
        public void MultipleRandoms()
        {
            var rndList = new List<Random>();
            for (int i = 0; i < 10; i++)
            {
                rndList.Add(GetRandom());
            }

            var lastNumber = rndList[0].Next();
            for (int i = 1; i < 10; i++)
            {
                var thisNum = rndList[i].Next();

                Assert.AreEqual(lastNumber, thisNum);

                lastNumber = thisNum;
            }
        }

        [Test]
        public void CompareTwoRandoms_TimeDelay()
        {
            var firstRnd = GetRandom();

            Thread.Sleep(3000);

            var secondRnd = GetRandom();

            var firstNum = firstRnd.Next();
            var secondNum = secondRnd.Next();

            Assert.AreEqual(firstNum, secondNum);
        }

        private Random GetRandom()
        {
            string date = DateTime.Now.Day + "" + DateTime.Now.Month + "" + DateTime.Now.Year;
            return new Random(Convert.ToInt32(date));
        }
    }
}
