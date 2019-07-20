
using System;
using System.Collections.Generic;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای نگهداری اطلاعات مختلف (تگ ها، ریشه کلمه و ...) درباره عبارات (کلمات) است
    /// </summary>
    /// <seealso cref="Phrase" />
    public class Phrase
    {
        ///// <summary>
        ///// Initializes a new instance of the <see cref="Phrase" /> class.
        ///// </summary>
        //public Phrase()
        //{
        //    RootWords = new List<string>();
        //    Tags = new Dictionary<string, Tuple<string, bool>>();
        //}


        //private string _word = string.Empty;
        /// <summary>
        /// کلمه یا متن عبارت
        /// </summary>
        /// <value>The word.</value>
        public string Word { get; set; }

        /// <summary>
        /// توضیحات اضافه در مورد این عبارت
        /// </summary>
        public string WordComment = string.Empty;
        /// <summary>
        /// حدس اولیه در مورد نقش کلمه
        /// </summary>
        public string SimplePos = string.Empty;
        /// <summary>
        /// ریشه های کلمه یا عبارت فعلی
        /// </summary>
        public List<string> RootWords;
        /// <summary>
        /// اگر کلمه از نوع فعل باشد اطلاعات صرفی فعل را نگهداری میکند و درغیر اینصورت تهی است
        /// </summary>
        public object VerbInformation;
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

        /*/// <summary>
        /// The tags
        /// </summary>
        private readonly Dictionary<string, Tuple<string, bool>> _tags;*/

        /// <summary>
        /// فهرست تگ های عبارت
        /// Dictionary[نام تگ, Tuple[مقدار تگ, آیا این تگ کلمه بعدی را هم شامل میشود]]
        /// Dictionary[Key or Tag_Type, Tuple[Tag_Value, IsContinueInNextPhrase]]
        /// </summary>
        /// <value>The tags.</value>
        public Dictionary<string, Tuple<string, bool>> Tags { get; set; }//=> _tags;      




        /// <summary>
        /// اولین ریشه کلمه
        /// </summary>
        /// <value>The first root.</value>
        public string FirstRoot;

        /// <summary>
        /// تعداد کلمات عبارت
        /// </summary>
        /// <value>The word count.</value>
        public int WordCount;

        /// <summary>
        /// طول (تعداد کاراکترهای) عبارت
        /// </summary>
        /// <value>The length.</value>
        public int Length;

        /// <summary>
        /// آیا عبارت فعل است یا غیرفعل
        /// </summary>
        /// <value><c>true</c> if this instance is verb; otherwise, <c>false</c>.</value>
        public bool IsVerb;

        /// <summary>
        /// آیا عبارت متشکل از علائم و جداساز است
        /// </summary>
        /// <value><c>true</c> if this instance is punc; otherwise, <c>false</c>.</value>
        public bool IsPunc;


    }
}
