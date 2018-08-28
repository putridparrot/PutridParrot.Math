using System;
using System.Collections;
using System.Collections.Generic;

namespace PutridParrot.Math
{
    public class Vector : IEnumerable<double>
    {
        private double[] _vector;

        public Vector()
        {            
        }

        public Vector(Vector vector)
        {
            Copy(vector._vector);
        }

        public Vector(double[] vector)
        {
            Copy(vector);
        }

        public void Copy(double[] vector)
        {
            _vector = new double[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                _vector[i] = vector[i];
            }
        }

        public int Length => _vector?.Length ?? 0;

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            return (_vector is IEnumerable<double> enumerable ? enumerable.GetEnumerator() : null) ??
                   throw new InvalidOperationException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_vector is IEnumerable enumerable ? enumerable.GetEnumerator() : null) ??
                   throw new InvalidOperationException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Vector);

        }

        public double this[int index]
        {
            get => _vector[index];
            set => _vector[index] = value;
        }

        public bool IsEmpty => _vector == null || _vector.Length == 0;

        public bool Equals(Vector vector)
        {
            if (vector == null)
                return false;

            if (Object.ReferenceEquals(this, vector))
                return true;

            if (Length != vector.Length)
                return false;

            for (var i = 0; i < Length; i++)
            {
                if (this[i] != vector[i])
                        return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return _vector != null ? _vector.GetHashCode() : base.GetHashCode();
        }
    }
}
