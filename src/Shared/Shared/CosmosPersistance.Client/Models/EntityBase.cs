using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Models
{
    /// <summary>
    /// The base entity for every object in the database
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// The Id of the document
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The unix stamp of when the document was created
        /// </summary>
        [JsonProperty(PropertyName = "documentCreatedUnix")]
        public long DocumentCreatedUnix { get; set; }

        /// <summary>
        /// The unix stamp of when the document was last modified
        /// </summary>
        [JsonProperty(PropertyName = "documentModifiedUnix")]
        public long DocumentModifiedUnix { get; set; }
    }
}
