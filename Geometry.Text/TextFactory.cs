using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry.Text
{
    /// <summary>
    /// Singleton abstraction of application text. In real life, the text would be stored in a DB/caching system, resource files, etc
    /// </summary>
    public class TextFactory : IDisposable
    {
        private static TextFactory instance = null;
        private Dictionary<string, Dictionary<string, TextDO>> textCache = new Dictionary<string, Dictionary<string, TextDO>>();

         #region Constructor
        private TextFactory()
        {
           
        }
        #endregion

        public static TextFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextFactory();
                    InitCache();
                    
                }
                return instance;
            }
        }

        private static void InitCache()
        {
            instance.textCache.Add("eng", BuildEngCache());
        }

        private static Dictionary<string, TextDO> BuildEngCache()
        {
            string lang = "eng";
            Dictionary<string, TextDO> langCache = new Dictionary<string, TextDO>();

            langCache.Add("MSG_HEADER", new TextDO("Social Life of Rectangles", string.Empty, lang));
            langCache.Add("MSG_ERR_HEADER", new TextDO("We've got issues", string.Empty, lang));
            langCache.Add("MSG_ERR_RECT_COMPARE", new TextDO("Cannot compare rectangles", string.Empty, lang));
            langCache.Add("MSG_ERR_LOGIC_COMPARE", new TextDO("Current implementation cannot provide the result given your input parameters", string.Empty, lang));
            langCache.Add("MSG_ERR_INVALID RECT", new TextDO("The rectangle is invalid. Width and Height must be greater than 0", string.Empty, lang));
            langCache.Add("MSG_INSTRUCT", new TextDO("Drag rectangles around and see how they interact. Watching them can be fun!", string.Empty, lang));

            #region Intersection Messages
            langCache.Add("0", new TextDO("Separated", "We are completely independent", lang));
            langCache.Add("1", new TextDO("Intersection", "We are happily intersecting", lang));
            langCache.Add("2", new TextDO("Containment", "Hey, I am comletely within the bounds of my larger buddy", lang));
            langCache.Add("3", new TextDO("Adjacency", "Snuggly rubbing each other's elbows", lang));
            langCache.Add("4", new TextDO("Full Overlap", "We are equal!", lang));
            langCache.Add("5", new TextDO("Touching", "We are touching each other, but it does not count as Adjacency", lang));
            #endregion

            return langCache;
        }

        public TextDO GetTextObject(string code, string lang)
        {
            TextDO textObj = null;
            Dictionary<string, TextDO> langCache = textCache[lang];
            if (langCache != null)
            {
                textObj = langCache[code];
            }
            return textObj;
        }

        public  string GetText(string code, string lang)
        {
            string text = string.Empty;
            TextDO textObj = GetTextObject(code, lang);
            if (textObj != null)
            {
                text = textObj.Text;
            }
            return text;
        }

        public string GetLongText(string code, string lang)
        {
            string text = string.Empty;
            TextDO textObj = GetTextObject(code, lang);
            if (textObj != null)
            {
                text = textObj.Longtext;
            }
            return text;
        }

        public void Dispose()
        {
            textCache.Clear();
            textCache = null;
        }
    }
}
