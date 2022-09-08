using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Extensions.Exceptions
{
    /// <summary>
    /// Custom exception to handle an entity already existing in the database
    /// </summary>
    public class EntityAlreadyExistsException : Exception
    {
        /// <summary>
        /// Id of the entity that already exists
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// A new default exception
        /// </summary>
        public EntityAlreadyExistsException() { }

        /// <summary>
        /// A new exception with a message
        /// </summary>
        /// <param name="message">Exception message</param>
        public EntityAlreadyExistsException(string message)
            : base(message) { }

        /// <summary>
        /// A new exception with an Inner Exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public EntityAlreadyExistsException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// A new exception with the Id of the entity
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="id">Id of the entity</param>
        public EntityAlreadyExistsException(string message, string id)
            : this(message)
            => Id = id;
    }
}
