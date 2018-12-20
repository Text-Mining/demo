namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای تنظیم پارامترهای مورد نیاز توابع جداسازی جملات یا توکن ها طراحی شده است
    /// </summary>
    public class SentenceSplitterModel
    {
        /// <summary>
        /// متن ورودی
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// آیا تبدیل محاوره ای به رسمی انجام شود
        /// </summary>
        /// <value><c>true</c> if [check slang]; otherwise, <c>false</c>.</value>
        public bool CheckSlang { get; set; }

        /// <summary>
        /// آیا نرمالسازی متن نیز انجام شود
        /// </summary>
        /// <value><c>true</c> if normalize; otherwise, <c>false</c>.</value>
        public bool Normalize { get; set; }

        /// <summary>
        /// پارامترهای نرمالسازی متن ورودی
        /// <remarks>این پارامتر درصورتی استفاده میشود که پارامتر <c>Normalize</c> فعال باشد</remarks>
        /// </summary>
        /// <value>The normalizer parameters.</value>
        public NormalizerModel NormalizerParams { get; set; }

        /// <summary>
        /// آیا علائم (جداساز کلمات مثل نقطه، ویرگول، گیومه و ...) در خروجی حفظ شوند
        /// </summary>
        /// <value><c>true</c> if [keep separator] separator keep; otherwise, <c>false</c> those were removed.</value>
        public bool KeepSeparator { get; set; }

        /// <summary>
        /// آیا بخشهای جملات مرکب غیرتودرتو نیز جدا شوند
        /// </summary>
        /// <value><c>true</c> if [complex sentence]; otherwise, <c>false</c>.</value>
        public bool ComplexSentence { get; set; }
    }
}