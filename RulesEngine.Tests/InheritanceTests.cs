using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SpearSoft.RulesEngine;
using SpearSoft.RulesEngine.Fluent;
using System.Collections.ObjectModel;

namespace SpearSoft.RulesEngine.Tests
{
    [TestFixture]
    public class InheritanceTests
    {
        private class ClassA
        {
            public int ParamA { get; set; }
        }
        private class ClassB : ClassA
        {
            public int ParamB { get; set; }
        }
        private class ClassC : ClassB
        {
            public int ParamC { get; set; }
        }

        IEngine _re;

        [SetUp]
        public void TestInitialize()
        {
            var builder = new Fluent.FluentBuilder();

            builder.For<ClassA>()
                .Setup(a => a.ParamA)
                    .MustBeLessThan(1000);
            builder.For<ClassB>()
                .Setup(b => b.ParamB)
                    .MustBeLessThan(1000);
            builder.For<ClassC>()
                    .Setup(c => c.ParamC)
                        .MustBeLessThan(1000)
                    .Setup(c => c.ParamA)
                        .MustBeLessThan(250);

            _re = builder.Build();
        }

        [Test]
        public void ShouldInheritRules()
        {
            ClassB b = new ClassB() { ParamA = 2000, ParamB = 999};
            Assert.IsFalse(_re.Validate(b), "Expected 'b' to be invalid because inherited ParamA is not valid.");
        }

        [Test]
        public void ShouldInheritRules_2()
        {
            ClassC c = new ClassC() { ParamA = 999, ParamB = 999, ParamC=999 };
            Assert.IsFalse(_re.Validate(c), "Expected 'c' to be invalid because inherited ParamA is not valid.");
        }
    }
}
