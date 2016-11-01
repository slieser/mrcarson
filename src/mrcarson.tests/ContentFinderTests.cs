using System;
using System.Collections.Generic;
using FluentAssertions;
using mrcarson.content;
using NUnit.Framework;

namespace mrcarson.tests
{
    [TestFixture]
    public class ContentFinderTests
    {
        private List<Content> theContent;
        private ContentFinder sut;

        [SetUp]
        public void Setup() {
            theContent = new List<Content> {
                new Content { Id = "1", Message = "m1" },
                new Content { Id = "2", Message = "m2" },
                new Content { Id = "3", Message = "m3" },
                new Content { Id = "4", Message = "m4" },
                new Content { Id = "5", Message = "m5" }
            };
            sut = new ContentFinder(theContent, new Random(42));
        }

        [Test]
        public void Content_is_found_by_random_index() {
            var result = sut.GetNextContent();
            result.Message.Should().Be("m3");

            result = sut.GetNextContent();
            result.Message.Should().Be("m1");

            result = sut.GetNextContent();
            result.Message.Should().Be("m2");

            result = sut.GetNextContent();
            result.Message.Should().Be("m4");

            result = sut.GetNextContent();
            result.Message.Should().Be("m5");
        }

        [Test]
        public void Published_content_is_not_found() {
            theContent[0].PublishedId = "#1";
            theContent[4].PublishedId = "#5";

            var result = sut.GetNextContent();
            result.Message.Should().Be("m3");

            result = sut.GetNextContent();
            result.Message.Should().Be("m2");

            result = sut.GetNextContent();
            result.Message.Should().Be("m4");
        }
    }
}