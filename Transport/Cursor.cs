using System;
using System.Collections.Generic;

namespace Transport
{
    /// <summary>
    /// Provides a navigable cursor over a collection of items,
    /// supporting forward and backward traversal.
    /// </summary>
    /// <typeparam name="T">The type of items in the cursor.</typeparam>
    public class Cursor<T>
    {
        private readonly IList<T> _items;
        private int _position;

        /// <summary>
        /// Initializes a new <see cref="Cursor{T}"/> over the given collection.
        /// </summary>
        /// <param name="items">The collection to cursor over.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="items"/> is null.</exception>
        public Cursor(IList<T> items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
            _position = -1;
        }

        /// <summary>
        /// Gets the current item at the cursor position.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the cursor is before the first item or after the last item.
        /// </exception>
        public T Current
        {
            get
            {
                if (_position < 0 || _position >= _items.Count)
                    throw new InvalidOperationException("Cursor is not positioned on a valid item.");
                return _items[_position];
            }
        }

        /// <summary>
        /// Gets the zero-based index of the current cursor position,
        /// or -1 if the cursor has not been advanced.
        /// </summary>
        public int Position => _position;

        /// <summary>
        /// Gets the total number of items in the cursor.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Advances the cursor to the next item.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the cursor was successfully advanced;
        /// <c>false</c> if the cursor has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            if (_position < _items.Count - 1)
            {
                _position++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves the cursor to the previous item.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the cursor was successfully moved back;
        /// <c>false</c> if the cursor is already before the first item.
        /// </returns>
        public bool MovePrevious()
        {
            if (_position > 0)
            {
                _position--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Resets the cursor to before the first item.
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }

        /// <summary>
        /// Moves the cursor to the first item.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the collection has at least one item; otherwise <c>false</c>.
        /// </returns>
        public bool MoveFirst()
        {
            if (_items.Count == 0)
                return false;
            _position = 0;
            return true;
        }

        /// <summary>
        /// Moves the cursor to the last item.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the collection has at least one item; otherwise <c>false</c>.
        /// </returns>
        public bool MoveLast()
        {
            if (_items.Count == 0)
                return false;
            _position = _items.Count - 1;
            return true;
        }
    }
}
