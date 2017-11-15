using System;
using System.Collections.Generic;
using System.Text;

namespace MadreApp.Helpers
{
    class MessagingCenterChoice
    {
        /// <summary>
        /// Init this instance.
        /// </summary>
        public static void Init()
        {
            var time = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance cancel/OK text.
        /// </summary>
        /// <value><c>true</c> if this instance cancel; otherwise, <c>false</c>.</value>
        public string Cancel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance destruction text.
        /// </summary>
        /// <value>Destruction text</value>
        public string Destruction { get; set; }

        /// <summary>
        /// Gets or sets a list of items text.
        /// </summary>
        /// <value>the list of items</value>
        public string[] Items { get; set; }


        /// <summary>
        /// Gets or sets the OnCompleted Action.
        /// </summary>
        /// <value>The OnCompleted Action.</value>
        public Action<string> OnCompleted { get; set; }
    }
}
