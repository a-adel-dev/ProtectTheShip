

using UnityEngine;

namespace com.ARTillery.Core
{
    public class Points
    {
        private int _maxPoints;
        private int _currentPoints;

        public int MaxPoints { get => _maxPoints; set => _maxPoints = value; }
        public int CurrentPoints { get => _currentPoints; set => _currentPoints = value; }

        public Points(int maxPoints)
        {
            MaxPoints = maxPoints;
            CurrentPoints = maxPoints;
        }

        public void ReducePoints(int value)
        {
            CurrentPoints = Mathf.Min( CurrentPoints - value , 0);
        }

        public void ResetPoints()
        {
            CurrentPoints = MaxPoints;
        }

        public void AddPoints(int value)
        {
            CurrentPoints = Mathf.Max(CurrentPoints + value, MaxPoints);
        }

        public int GetCurrentPoints()
        {
            return CurrentPoints;
        }

        public bool IsOutOfPoints()
        {
            return _currentPoints <= 0;
        }

    }
}
