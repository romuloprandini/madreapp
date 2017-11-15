using System;

namespace MadreApp.Helpers
{
    public class MessagingCenterQuestion
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
        /// Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance positive text.
        /// </summary>
        /// <value>The positive text</value>
        public string Positive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance negative text.
        /// </summary>
        /// <value>The negative text</value>
        public string Negative { get; set; }

        /// <summary>
        /// Gets or sets the OnCompleted Action.
        /// </summary>
        /// <value>The OnCompleted Action.</value>
        public Action<bool> OnCompleted { get; set; }
    }
}