using System;
using System.Collections.Generic;
using System.Linq;
using mrcarson.content;

namespace mrcarson
{
    public class ContentFinder
    {
        private IList<Content> contents;
        private readonly Random random;

        public ContentFinder(IEnumerable<Content> contents)
            : this(contents, new Random(DateTime.Now.Millisecond)) {
        }

        internal ContentFinder(IEnumerable<Content> contents, Random random) {
            this.contents = contents.ToList();
            this.random = random;
        }

        public Content GetNextContent() {
            contents = contents.OrderBy(c => c.PublishedId).ToList();
            var index = GetRandomIndex();
            var nextContent = contents.ElementAt(index);
            contents.RemoveAt(index);
            return nextContent;
        }

        private int GetRandomIndex() {
            var count = contents.Count(c => !c.IsPublished);
            var index = random.Next(0, count - 1);
            return index;
        }
    }
}