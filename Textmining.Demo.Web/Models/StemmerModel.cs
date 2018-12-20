using System.Collections.Generic;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای تنظیم پارامترهای مورد نیاز تابع ریشه یابی طراحی شده است
    /// </summary>
    public class StemmerModel
    {
        /// <summary>
        /// ورودی به شکل لیست عبارات
        /// </summary>
        /// <value>The phrases.</value>
        public List<PhraseModel> Phrases { get; set; }
        /// <summary>
        /// ورودی به شکل متن
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// آیا در حین ریشه یابی کلمات محاوره ای تحلیل و به شکل رسمی تبدیل شوند
        /// </summary>
        /// <value><c>true</c> if [check slang]; otherwise, <c>false</c>.</value>
        public bool CheckSlang { get; set; }
        /// <summary>
        /// آیا بخشهای جملات مرکب غیرتودرتو جداسازی شوند
        /// </summary>
        /// <value><c>true</c> if [complex sentence]; otherwise, <c>false</c>.</value>
        public bool ComplexSentence { get; set; }
        /// <summary>
        /// سطح ریشه یابی
        /// </summary>
        /// <value>The level.</value>
        public LevelOfLemmatizer Level { get; set; }
    }
}