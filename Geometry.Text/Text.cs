using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.Text
{
    /// <summary>
    /// Encapsulates a text segment for th epurposes of UI
    /// </summary>
    public class TextDO
    {
        private string text = string.Empty;
        private string lang = string.Empty;
        private string longtext = string.Empty;

        public TextDO()
        {

        }

        public TextDO(string text, string longtext, string lang)
        {
            this.text = text;
            this.lang = lang;
            this.longtext = longtext;
        }

        /// <summary>
        /// optional long version to be used in tooltips, etc
        /// </summary>
        public string Longtext
        {
            get { return longtext; }
            set { longtext = value; }
        }

        /// <summary>
        /// Language
        /// </summary>
        public string Lang
        {
            get { return lang; }
            set { lang = value; }
        }

        /// <summary>
        /// Actual text string
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
