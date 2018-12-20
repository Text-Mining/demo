using System;
using System.Collections.Generic;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای نگهداری اطلاعات مختلف (تگ ها، ریشه کلمه و ...) درباره عبارات (کلمات) است
    /// </summary>
    public class PhraseModel
    {
        /// <summary>
        /// کلمه یا متن عبارت
        /// </summary>
        /// <value>The word.</value>
        public string Word { get; set; }

        /// <summary>
        /// توضیحات اضافه در مورد این عبارت
        /// </summary>
        public string WordComment { get; set; }

        /// <summary>
        /// حدس اولیه در مورد نقش کلمه
        /// </summary>
        public string SimplePos { get; set; }

        /// <summary>
        /// ریشه های کلمه یا عبارت فعلی
        /// </summary>
        public List<string> RootWords { get; set; }

        /// <summary>
        /// اگر کلمه از نوع فعل باشد اطلاعات صرفی فعل را نگهداری میکند و درغیر اینصورت تهی است
        /// </summary>
        public VerbInflectionModel VerbInformation;

        /// <summary>
        /// شماره جمله ای که این عبارت در آن قرار دارد
        /// </summary>
        public int SentenceNumber;

        /// <summary>
        /// شماره (اندیس جایگاه) کلمه در جمله
        /// </summary>
        public int WordNumberInSentence;
        /// <summary>
        /// شماره اندیس کاراکتر شروع عبارت در متن
        /// </summary>
        public int StartCharIndex;

        /// <summary>
        /// فهرست تگ های عبارت
        /// Dictionary[نام تگ, Tuple[مقدار تگ, آیا این تگ کلمه بعدی را هم شامل میشود]]
        /// Dictionary[Key or Tag_Type, Tuple[Tag_Value, IsContinueInNextPhrase]]
        /// </summary>
        /// <value>The tags.</value>
        public Dictionary<string, Tuple<string, bool>> Tags { get; set; }

        /// <summary>
        /// تعداد کلمات عبارت
        /// </summary>
        /// <value>The word count.</value>
        public int WordCount { get; set; }

        /// <summary>
        /// طول (تعداد کاراکترهای) عبارت
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; set; }

        /// <summary>
        /// آیا عبارت فعل است یا غیرفعل
        /// </summary>
        /// <value><c>true</c> if this instance is verb; otherwise, <c>false</c>.</value>
        public bool IsVerb { get; set; }

        /// <summary>
        /// آیا عبارت متشکل از علائم و جداساز است
        /// </summary>
        /// <value><c>true</c> if this instance is punc; otherwise, <c>false</c>.</value>
        public bool IsPunc { get; set; }

        /// <summary>
        /// تبدیل اطلاعات عبارت فعلی به رشته متنی
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Word;
        }
    }
}
