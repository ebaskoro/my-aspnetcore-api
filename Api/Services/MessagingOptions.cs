namespace Api.Services
{

    /// <summary>
    /// Messaging options.
    /// </summary>
    public class MessagingOptions
    {

        /// <summary>
        /// Gets or sets the messaging server's URL.
        /// </summary>
        public string Url
        {
            get;

            set;
        }


        /// <summary>
        /// Checks whether the URL is specified or not.
        /// </summary>
        public bool HasUrl
        {
            get
            {
                return (false == string.IsNullOrEmpty(Url));
            }
        }

    }

}
