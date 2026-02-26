using System;
using System.Collections.Generic;
using Transport;
using Xunit;

namespace Transport.Tests
{
    public class CursorTests
    {
        private static Cursor<int> CreateCursor(int count = 3)
        {
            var items = new List<int>();
            for (int i = 1; i <= count; i++)
                items.Add(i);
            return new Cursor<int>(items);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenItemsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Cursor<int>(null!));
        }

        [Fact]
        public void InitialPosition_IsMinusOne()
        {
            var cursor = CreateCursor();
            Assert.Equal(-1, cursor.Position);
        }

        [Fact]
        public void Current_ThrowsInvalidOperationException_BeforeFirstMove()
        {
            var cursor = CreateCursor();
            Assert.Throws<InvalidOperationException>(() => cursor.Current);
        }

        [Fact]
        public void MoveNext_ReturnsTrueAndAdvancesPosition()
        {
            var cursor = CreateCursor();
            Assert.True(cursor.MoveNext());
            Assert.Equal(0, cursor.Position);
            Assert.Equal(1, cursor.Current);
        }

        [Fact]
        public void MoveNext_ReturnsFalse_WhenAtEnd()
        {
            var cursor = CreateCursor(1);
            cursor.MoveNext();
            Assert.False(cursor.MoveNext());
        }

        [Fact]
        public void MoveNext_TraversesAllItems()
        {
            var cursor = CreateCursor(3);
            var results = new List<int>();
            while (cursor.MoveNext())
                results.Add(cursor.Current);
            Assert.Equal(new[] { 1, 2, 3 }, results);
        }

        [Fact]
        public void MovePrevious_ReturnsFalse_BeforeFirstItem()
        {
            var cursor = CreateCursor();
            cursor.MoveNext();
            Assert.False(cursor.MovePrevious());
        }

        [Fact]
        public void MovePrevious_MovesBackward()
        {
            var cursor = CreateCursor(3);
            cursor.MoveNext();
            cursor.MoveNext();
            Assert.True(cursor.MovePrevious());
            Assert.Equal(0, cursor.Position);
            Assert.Equal(1, cursor.Current);
        }

        [Fact]
        public void Reset_SetsPositionToMinusOne()
        {
            var cursor = CreateCursor();
            cursor.MoveNext();
            cursor.Reset();
            Assert.Equal(-1, cursor.Position);
        }

        [Fact]
        public void Reset_MakesCurrentThrow()
        {
            var cursor = CreateCursor();
            cursor.MoveNext();
            cursor.Reset();
            Assert.Throws<InvalidOperationException>(() => cursor.Current);
        }

        [Fact]
        public void MoveFirst_ReturnsTrueAndPositionsAtFirst()
        {
            var cursor = CreateCursor(3);
            cursor.MoveLast();
            Assert.True(cursor.MoveFirst());
            Assert.Equal(0, cursor.Position);
            Assert.Equal(1, cursor.Current);
        }

        [Fact]
        public void MoveFirst_ReturnsFalse_WhenEmpty()
        {
            var cursor = new Cursor<int>(new List<int>());
            Assert.False(cursor.MoveFirst());
        }

        [Fact]
        public void MoveLast_ReturnsTrueAndPositionsAtLast()
        {
            var cursor = CreateCursor(3);
            Assert.True(cursor.MoveLast());
            Assert.Equal(2, cursor.Position);
            Assert.Equal(3, cursor.Current);
        }

        [Fact]
        public void MoveLast_ReturnsFalse_WhenEmpty()
        {
            var cursor = new Cursor<int>(new List<int>());
            Assert.False(cursor.MoveLast());
        }

        [Fact]
        public void Count_ReturnsNumberOfItems()
        {
            var cursor = CreateCursor(5);
            Assert.Equal(5, cursor.Count);
        }
    }
}
