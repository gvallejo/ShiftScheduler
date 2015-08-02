using Scheduler.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public class RandomGenerator
    {
        protected Random m_rand;

        public RandomGenerator()
        {
            this.m_rand = new Random();
        }

        virtual public int GetNextInt(int min, int max)
        {
            return this.m_rand.Next(min, max);
        }

        virtual public Stack<int> GetRandomQueue(int qty, int max)
        {
            int[] aux = GetRandomSequence(qty, 1, max);

            for (int i = 0; i < aux.Length; i++)
            {
                LogSession.Main.LogMessage(string.Format("aux[{0}] = {1}", i, aux[i]));
            }

            return new Stack<int>(aux);
        }

        virtual public int[] GetRandomSequence(int qty, int minVal, int maxVal)
        {
            int[] rndSeq;

            rndSeq = GetFrameworkRandomSequence(qty, minVal, maxVal);
            //rndSeq = GetRangeRandomSequence(qty, minVal, maxVal);
            return rndSeq;
        }

        virtual public int[] GetFrameworkRandomSequence(int qty, int minVal, int maxVal)
        {
            List<int> InitList = new List<int>(Math.Abs(maxVal - minVal) + 1);
            List<int> rndSeq = new List<int>(qty);
            int polledNumber;
            int UpperLimit;


            for (int i = minVal; i < maxVal + 1; i++)
            {
                InitList.Add(i);
            }


            UpperLimit = Math.Abs(maxVal - minVal) + 1;
            for (int i = 0; i < qty; i++)
            {
                // Generate random integers from 0 to UpperLimit-1.
                polledNumber = this.m_rand.Next(UpperLimit);
                rndSeq.Add(InitList[polledNumber]);
                InitList.RemoveAt(polledNumber);
                UpperLimit--;
            }

            return rndSeq.ToArray();
        }

        virtual public int[] GetRangeRandomSequence(int qty, int minVal, int maxVal)
        {
            int[] rndSeq;
            var sequence = Enumerable.Range(minVal, qty).OrderBy(n => Guid.NewGuid());
            return rndSeq = sequence.ToArray();
        }


        
    }
}
