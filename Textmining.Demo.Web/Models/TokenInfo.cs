using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace Textmining.Demo.Web.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EditType {None, SpaceCorrection, CharReplace, SpellCorrection, WordSuggestion } //MergeWithPrev, MergeWithNext,
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TokenType { FormalWord, Word, Separator, Number, Phone, Email, Web, SocialId, HashTag, Emoji, Abbreviation, DateTime, English }

    /// <summary>
    /// این کلاس برای نگهداری یک مورد اصلاح پیشنهاد شده توسط ویراستار فارسی‌یار استفاده می‌شود
    /// </summary>
    public class EditItem
    {
        public EditItem() { }

        public EditItem(EditType editType, string suggestedText, string description, int removeTokensCount = 0)
        {
            EditType = editType;
            SuggestedText = suggestedText;
            Description = description;
            RemoveTokensCount = removeTokensCount;
        }

        /// <summary>
        /// نوع اصلاح
        /// </summary>
        /// <value>The type of the edit.</value>
        public EditType EditType { get; set; }

        /// <summary>
        /// عبارت پیشنهادی برای جایگزین شدن
        /// </summary>
        /// <value>The suggested text.</value>
        public string SuggestedText { get; set; }

        /// <summary>
        /// توضیح درباره تغییر پیشنهادی
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// حذف چند عدد از توکن‌های قبل یا بعد
        /// </summary>
        /// <value>The remove tokens.</value>
        public int RemoveTokensCount { get; set; }
    }

    /// <summary>
    /// این کلاس برای نگهداری اطلاعات توکن‌های متن بکار می‌رود
    /// </summary>
    public class TokenInfo
    {
       
        /// <summary>
        /// لیست تغییرات اعمال شده روی این توکن
        /// </summary>
        /// <value>The edit list.</value>
        public List<EditItem> EditList { get; set; }

        /// <summary>
        /// نوع توکن از قبیل: کلمه، عدد، ایمیل، ایموجی، آدرس وب، ایمیل و ...
        /// </summary>
        /// <value>The type of the token.</value>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// شماره اندیس کاراکتری توکن فعلی در متن اصلی
        /// </summary>
        /// <value>The index of the character.</value>
        public int CharIndex { get; set; }

        /// <summary>
        /// شکل خام یا اولیه توکن
        /// </summary>
        /// <value>The original text.</value>
        public string OriginalText { get; set; }

        /// <summary>
        /// شکل جدید (اصلاح شده) توکن براساس تغییرات پیشنهاد شده
        /// </summary>
        /// <value>The new text.</value>
        public string NewText => EditList == null || EditList.Count == 0
            //|| EditList[EditList.Count - 1].Item2 == null
            ? OriginalText
            : EditList[EditList.Count - 1].SuggestedText; // CandidateTexts[0];

        /// <summary>
        /// طول (تعداد کاراکترهای) شکل اصلی توکن
        /// </summary>
        /// <value>The old length.</value>
        public int OldLength => OriginalText?.Length ?? 0;

        /// <summary>
        /// طول (تعداد کاراکترهای) توکن بعد از اعمال تغییرات پیشنهاد شده
        /// </summary>
        /// <value>The new length.</value>
        public int NewLength => NewText?.Length ?? 0;

        /// <summary>
        /// نشان می‌دهد که آیا تغییراتی برای این توکن پیشنهاد شده است یا خیر؟
        /// </summary>
        /// <value><c>true</c> if this instance is changed; otherwise, <c>false</c>.</value>
        public bool IsChanged => EditList.Count > 0 && EditList.Any(ed=>ed!=null && !ed.SuggestedText.Equals(OriginalText));

        /// <summary>
        /// تبدیل نمونه از کلاس به متن قابل چاپ
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"«{OriginalText}»({OldLength}) => «{NewText}»({NewLength})";
    }
}
