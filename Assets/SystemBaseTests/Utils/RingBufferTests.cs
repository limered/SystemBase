using NUnit.Framework;
using SystemBase.Utils;

namespace SystemBaseTests.Utils
{
    public class RingBufferTests
    {
        private RingBuffer<int> _cut;


        private const int BufferCapacity = 5;
        [SetUp]
        public void SetUp()
        {
            _cut = new RingBuffer<int>(BufferCapacity);
        }

        [TestFixture]
        public class Capacity : RingBufferTests
        {
            [Test]
            public void ReturnTheCapacity()
            {
                Assert.AreEqual(BufferCapacity, _cut.Capacity);
            }    
        }

        [TestFixture]
        public class Add : RingBufferTests
        {
            [Test]
            public void IfBufferIsEmpty_AddsAtTheFront()
            {
                const int addedItem = 1;
                
                _cut.Add(addedItem);
                
                Assert.AreEqual(addedItem, _cut[0]);
            }

            [Test]
            public void IfBufferIsNotFull_AddsAfterLastAdding()
            {
                const int addedItem = 1;
                
                _cut.Add(123);
                _cut.Add(addedItem);
                
                Assert.AreEqual(addedItem, _cut[1]);
            }

            [Test]
            public void IfBufferIsFull_OverwritesFromTheBeginning()
            {
                const int addedItem = 300;
                
                for(var i = 0; i < BufferCapacity; i++)
                    _cut.Add(i);
                
                _cut.Add(addedItem);
                
                Assert.AreEqual(addedItem, _cut[0]);
            }

            [Test]
            public void CanNotAddDirectlyWithOverflow()
            {
                const int addedItem = 300;
                
                _cut[BufferCapacity * 2] = addedItem;
                
                Assert.AreEqual(addedItem, _cut[0]);
            }
        }
    }
}
