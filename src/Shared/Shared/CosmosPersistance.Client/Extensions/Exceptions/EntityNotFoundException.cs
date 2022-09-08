using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Extensions.Exceptions
{
    // <summary>
    /// Custom exception to handle an entity not being found in the database
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Id of the entity that was not found
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// A new default exception
        /// </summary>
        public EntityNotFoundException() { }

        /// <summary>
        /// A new exception with a message
        /// </summary>
        /// <param name="message">Exception message</param>
        public EntityNotFoundException(string message)
            : base(message) { }

        /// <summary>
        /// A new exception with an Inner Exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// A new exception with the Id of the entity
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="id">Id of the entity</param>
        public EntityNotFoundException(string message, string id)
            : this(message)
            => Id = id;
    }
}
