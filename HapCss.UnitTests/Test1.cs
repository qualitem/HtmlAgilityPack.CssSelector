using System;
using System.Linq;
using Xunit;

namespace HapCss.UnitTests
{
    public class Html1
    {
        static HtmlAgilityPack.HtmlDocument doc = LoadHtml();

        [Fact]
        public void IdSelectorMustReturnOnlyFirstElement()
        {
            var elements = doc.QuerySelectorAll("#myDiv");

            Assert.True(elements.Count == 1);
            Assert.True(elements[0].Id == "myDiv");
            Assert.True(elements[0].Attributes["first"].Value == "1");
        }

        [Fact]
        public void GetElementsByAttribute()
        {
            var elements = doc.QuerySelectorAll("*[id=myDiv]");

            Assert.True(elements.Distinct().Count() == 2 && elements.Count == 2);
            for (int i = 0; i < elements.Count; i++)
                Assert.True(elements[i].Id == "myDiv");
        }

        [Fact]
        public void GetElementsByClassName1()
        {
            var elements1 = doc.QuerySelectorAll(".cls-a");
            var elements2 = doc.QuerySelectorAll(".clsb");

            Assert.True(elements1.Count == 1);
            for (int i = 0; i < elements1.Count; i++)
                Assert.True(elements1[i] == elements2[i]);
        }

        [Fact]
        public void GetElementsByClassName_MultiClasses()
        {
            var elements = doc.QuerySelectorAll(".cls-a, .cls-b");

            Assert.True(elements.Count == 2);
            Assert.True(elements[0].Id == "spanA");
            Assert.True(elements[1].Id == "spanB");
        }

        [Fact]
        public void GetElementsByClassName_WithUnderscore()
        {
            var elements = doc.QuerySelectorAll(".underscore_class");

            Assert.True(elements.Count == 1);
            Assert.True(elements[0].Id == "spanB");
        }

        [Fact]
        public void GetElementsByPseudoNot_WithUnderscore()
        {
            var elements = doc.QuerySelectorAll("div>*:not(span)");

            Assert.Equal(1, elements.Count);
            Assert.True(elements[0].InnerText == "P1");
        }

        [Fact]
        public void GetElementsByPseudoNot2_WithUnderscore()
        {
            var elements = doc.QuerySelectorAll("*:not(table), *:not(table) *");

            Assert.Equal(11, elements.Count);
        }



        private static HtmlAgilityPack.HtmlDocument LoadHtml()
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(Resource.GetString("Test1.html"));

            return doc;
        }

    }
}
