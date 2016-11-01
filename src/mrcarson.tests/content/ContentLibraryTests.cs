using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using mrcarson.content;
using NUnit.Framework;

namespace mrcarson.tests.content
{
    [TestFixture]
    public class ContentLibraryTests
    {
        [SetUp]
        public void Setup() {
            var assemblyLocation = GetType().GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(assemblyLocation);
            Directory.SetCurrentDirectory(path);
        }

        [Test]
        public void CSV_file_is_read_correct() {
            var result = ContentLibrary.ImportFromCsvFile("content\\Tweets.csv");
            result.Should().Contain(c => c.Message == "äöü" && c.Link == "http://das.ist.toll");
            result.Should().Contain(c => c.Message == "Hello World" && c.Link == "https://link.de");
        }

        [Test]
        public void Importing_from_CSV_file_sets_the_ids_on_each_content() {
            var result = ContentLibrary.ImportFromCsvFile("content\\Tweets.csv");
            result.Should().NotContain(c => string.IsNullOrEmpty(c.Id));
        }

        [Test]
        public void Ids_are_unique() {
            var result = ContentLibrary.ImportFromCsvFile("content\\Tweets.csv");
            result.Select(c => c.Id).Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Should_all_be_unpublished() {
            var result = ContentLibrary.ImportFromCsvFile("content\\Tweets.csv");
            result.Should().NotContain(c => c.IsPublished);
        }

        [Test]
        public void Should_not_have_published_ids() {
            var result = ContentLibrary.ImportFromCsvFile("content\\Tweets.csv");
            result.Should().NotContain(c => !string.IsNullOrEmpty(c.PublishedId));
        }

        [Test]
        public void Should_not_have_published_timestamps() {
            var result = ContentLibrary.ImportFromCsvFile("content\\Tweets.csv");
            result.Should().NotContain(c => c.PublishedAt != new DateTime());
        }
    }
}