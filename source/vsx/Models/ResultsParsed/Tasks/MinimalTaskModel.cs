using System;

namespace vsx.Models
{
    [Serializable()]
    public class MinimalTaskModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public string Author { get; set; }
    }
}
