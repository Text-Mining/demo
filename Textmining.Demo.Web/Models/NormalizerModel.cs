using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای تنظیم پارامترهای مورد نیاز تابع پیش پردازش و نرمالسازی متون طراحی شده است
    /// </summary>
    public class NormalizerModel
    {
        public NormalizerModel()
        {
            ReplaceWildChar = true;
            ReplaceDigit = true;
            RefineSeparatedAffix = true;
            RefineQuotationPunc = false;
        }

        /// <summary>
        /// متن ورودی
        /// </summary>
        /// <value>The text.</value>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// آیا کاراکترها و علائم خاص با نسخه استاندارد آن جایگزین شوند
        /// </summary>
        /// <value><c>true</c> if [replace wild character]; otherwise, <c>false</c>.</value>
        [DefaultValue(true)]
        public bool ReplaceWildChar { get; set; }

        /// <summary>
        /// آیا ارقام (اعداد) عربی و انگلیسی با ارقام استاندارد فارسی جایگزین شوند
        /// </summary>
        /// <value><c>true</c> if [replace digit]; otherwise, <c>false</c>.</value>
        [DefaultValue(true)]
        public bool ReplaceDigit { get; set; }

        /// <summary>
        /// آیا نیم فاصله بین پسوند و پیشوند کلمات اصلاح شود
        /// </summary>
        /// <value><c>true</c> if [refine separated affix]; otherwise, <c>false</c>.</value>
        [DefaultValue(true)]
        public bool RefineSeparatedAffix { get; set; }

        /// <summary>
        /// آیا فاصله گذاری استاندارد بین علائم و عبارت نقل قول اعمال شود
        /// </summary>
        /// <value><c>true</c> if [refine quotation punc]; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool RefineQuotationPunc { get; set; }
    }
}
