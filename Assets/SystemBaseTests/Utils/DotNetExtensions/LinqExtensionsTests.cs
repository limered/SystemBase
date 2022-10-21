using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SystemBase.Utils.DotNet;

namespace SystemBaseTests.Utils.DotNetExtensions
{
    public class LinqExtensionsTests
    {
        private readonly int[] _notRandom = { 1, 2, 3, 4 };
        
        [Test]
        public void Randomize_ReturnsARandomArrayForArray()
        {
            var baseArr = (int[])_notRandom.Clone();
            var randomArray = baseArr.Randomize();

            Assert.AreNotEqual(baseArr, randomArray);
            Assert.IsTrue(randomArray
                .Any(i => Array.IndexOf(randomArray, i) != Array.IndexOf(baseArr, i)));
            Assert.IsFalse(randomArray.All(i => i == 0));
        }

        [Test]
        public void Randomize_ReturnsARandomListForList()
        {
            var baseList = new List<int>(_notRandom);
            var randomList = baseList.Randomize();

            Assert.AreNotEqual(baseList, randomList);
            Assert.IsTrue(randomList
                .Any(i => randomList.IndexOf(i) != baseList.IndexOf(i)));
            Assert.IsFalse(randomList.All(i => i == 0));
        }
        
        [Test]
        public void RandomizeInPlace_ReturnsTheArrayRandomized()
        {
            var baseArr = (int[])_notRandom.Clone();
            var randomArray = baseArr.RandomizeInPlace();

            Assert.AreEqual(baseArr, randomArray);
            Assert.IsTrue(randomArray
                .Any(i => Array.IndexOf(randomArray, i) != Array.IndexOf(_notRandom, i)));
            Assert.IsFalse(randomArray.All(i => i == 0));
        }

        [Test]
        public void RandomizeInPlace_ReturnsTheListRandomized()
        {
            var baseList = new List<int>(_notRandom);
            var randomList = baseList.RandomizeInPlace();

            Assert.AreEqual(baseList, randomList);
            Assert.IsTrue(randomList
                .Any(i => randomList.IndexOf(i) != Array.IndexOf(_notRandom, i)));
            Assert.IsFalse(randomList.All(i => i == 0));
        }

        [Test]
        public void RandomElement_RetrievesAElement()
        {
            var element = _notRandom.RandomElement();
            
            Assert.Contains(element, _notRandom);
        }
    }
}