using System;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// انواع روشهای محاسبه شباهت رشته ای
    /// </summary>
    [Flags]
    public enum WordComparisonOptions
    {
        HammingDistance = 1,
        JaccardDistance = 2,
        JaroDistance = 4,
        JaroWinklerDistance = 8,
        LevenshteinDistance = 16,
        LongestCommonSubsequence = 32,
        LongestCommonSubstring = 64,
        NormalizedLevenshteinDistance = 128,
        OverlapCoefficient = 256,
        RatcliffObershelpSimilarity = 512,
        SorensenDiceDistance = 1024,
        TanimotoCoefficient = 2048
    }

    /// <summary>
    /// سطوح مختلف نمایش ریشه ها
    /// </summary>
    public enum LevelOfLemmatizer
    {
        All,
        First,
        Last,
        None
    };

    /// <summary>
    /// انواع افعال
    /// </summary>
    public enum TenseType
    {
        Past,
        Present,
        Future,
        Imperative,
        General,
        All
    }

    [Flags]
    public enum AttachedPronounType
    {
        AttachedPronoun_NONE = 1,
        FIRST_PERSON_SINGULAR = 2,
        SECOND_PERSON_SINGULAR = 4,
        THIRD_PERSON_SINGULAR = 8,
        FIRST_PERSON_PLURAL = 16,
        SECOND_PERSON_PLURAL = 32,
        THIRD_PERSON_PLURAL = 64
    }

    [Flags]
    public enum PersonType
    {
        PERSON_NONE = 1,
        FIRST_PERSON_SINGULAR = 2,
        SECOND_PERSON_SINGULAR = 4,
        THIRD_PERSON_SINGULAR = 8,
        FIRST_PERSON_PLURAL = 16,
        SECOND_PERSON_PLURAL = 32,
        THIRD_PERSON_PLURAL = 64
    }

    [Flags]
    public enum TenseFormationType
    {
        TenseFormationType_NONE = 0,
        /// <summary>
        /// می خورم
        /// </summary>
        HAAL_SAADEH_EKHBARI = 1,
        /// <summary>
        /// بخورم
        /// </summary>
        HAAL_ELTEZAMI = 2,
        /// <summary>
        /// خورم
        /// </summary>
        HAAL_SAADEH = 4,
        /// <summary>
        /// بخور
        /// </summary>
        AMR = 8,
        /// <summary>
        /// خورد
        /// </summary>
        GOZASHTEH_SADEH = 32,
        /// <summary>
        /// می خوردم
        /// </summary>
        GOZASHTEH_ESTEMRAARI = 64,
        /// <summary>
        /// خورده ام
        /// </summary>
        GOZASHTEH_NAGHLI_SADEH = 128,
        /// <summary>
        /// می خورده ام
        /// </summary>
        GOZASHTEH_NAGHLI_ESTEMRAARI = 256,
        /// <summary>
        /// خورده بودم
        /// </summary>
        GOZASHTEH_BAEED = 512,
        /// <summary>
        /// خورده باشم
        /// </summary>
        GOZASHTEH_ELTEZAMI = 1024,
        /// <summary>
        /// گذشته نقلی ساده - سوم شخص مفرد (بن گذشته فعل+"ه") : خورده
        /// </summary>
        PAYEH_MAFOOLI = 2048,
        /// <summary>
        /// خواهم خورد
        /// </summary>
        AAYANDEH = 4096,
        /// <summary>
        /// خورده بودم
        /// </summary>
        GOZASHTEH_ABAD = 8192,
        /// <summary>
        /// دارم می خورم
        /// </summary>
        HAAL_MOSTAMAR = 16384,
        /// <summary>
        /// داشتم می خوردم
        /// </summary>
        GOZASHTEH_MOSTAMAR = 32768,
        /// <summary>
        /// خوردن
        /// </summary>
        MASDAR = 65536
    }

    [Flags]
    public enum TensePositivity
    {
        POSITIVE = 1,
        NEGATIVE = 2
    }

    [Flags]
    public enum TensePassivity
    {
        ACTIVE = 1,
        PASSIVE = 2
    }

    [Flags]
    public enum VerbType
    {
        SADEH = 1,
        PISHVANDI = 2,
        MORAKKAB = 4,
        MORAKKABPISHVANDI = 8,
        MORAKKABHARFE_EZAFEH = 16,
        EBAARATFELI = 32,
        LAZEM_TAKFELI = 64,
        AYANDEH_PISHVANDI = 128
    }

    [Flags]
    public enum VerbTransitivity
    {
        Transitive = 1,
        InTransitive = 2,
        BiTransitive = 4
    }
}
