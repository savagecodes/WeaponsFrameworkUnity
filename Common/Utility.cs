using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class Utility 
    {
        public static int SetBit(int flag, int bitPos) 
        {
    
            return flag | bitPos;
            //return flag |= 1 << bit;
        }
        public static int ClearBit(int flag, int bitPos)
        {
  
            return flag & (~bitPos);
            // return flag &= ~(1 << bit);
        }

        public static bool TestBit(int flag,int bitPos)
        {
            return (flag & bitPos) == bitPos;
            // return (flag & bit) != 0;
        }

    }
}
