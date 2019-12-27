using Assets.Scripts.Workers.Helpers;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Helpers
{
    [Category("Helpers")]
    class EnumHelperTests
    {
        enum Colours { Red = 'a', Blue = 'b', Green = 'c' };

        [Test]
        public void ValidValue()
        {
            Assert.IsTrue(EnumHelpers.IsValue<Colours>('a'));
        }

        [Test]
        public void InvalidValue()
        {
            Assert.IsFalse(EnumHelpers.IsValue<Colours>('d'));
        }
    }
}
