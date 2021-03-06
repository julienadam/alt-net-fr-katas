﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling.Iteration4
{
    public class Frame
    {
        public int? ThrowOne { get; set; }
        public int? ThrowTwo { get; set; }
        public int? Score { get; set; }
        public bool IsOpen
        {
            get { return !ThrowTwo.HasValue; }
        }
        public bool IsSpare
        {
            get { return ThrowTwo.HasValue && 10 == (ThrowOne + ThrowTwo); }
        }
        public string Display
        {
            get { 
                if (IsOpen)
                    return ThrowOne + "|_";
                if (IsSpare)
                    return ThrowOne + "|/";
                return ThrowOne + "|" + ThrowTwo;
            }

        }
    }

    public class Bowler
    {
        private int _frame = 0;
        private List<Frame> _frames = new List<Frame>(10); 

        private Frame CurrentFrame()
        {
            if (_frames.Count <= _frame)
            {
                _frames.Add(new Frame());
            }
            return _frames[_frame];
        }

        public void Throw(int pinsSet)
        {
            var currentFrame = CurrentFrame();
            if (FrameNo > 1 && this[FrameNo - 1].IsSpare && !currentFrame.ThrowOne.HasValue)
            {
                this[FrameNo - 1].Score = 10 + pinsSet;
            }
            if (currentFrame.ThrowOne.HasValue)
            {
                currentFrame.ThrowTwo = pinsSet;
                if(!currentFrame.IsSpare)
                    currentFrame.Score = currentFrame.ThrowOne + currentFrame.ThrowTwo;
                _frame++;
            }
            else
                currentFrame.ThrowOne = pinsSet;
        }

        public int? Score
        {
            get
            {
                int? score = null;
                foreach (var frame in _frames)
                {
                    score = frame.Score + (score??0);
                }
                return score;
            }
        }
        public int FrameNo
        {
            get { return _frame+1; }
        }
        
        public string Display
        {
            get { return string.Join(" ", _frames.Select(f => f.Display)); }
        }

        public Frame this[int frame]
        {
            get { return _frames[frame - 1]; }
        }
    }
}
