using System;

namespace mrcarson.content
{
    public class Content
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string Link { get; set; }

        public DateTime PublishedAt { get; set; }

        public string PublishedId { get; set; }

        public bool IsPublished {
            get { return !string.IsNullOrEmpty(PublishedId); }
        }

        protected bool Equals(Content other) {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Content)obj);
        }

        public override int GetHashCode() {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}