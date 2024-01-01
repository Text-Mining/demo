using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Textmining.Demo.Web.Models;
using NToastNotify;

namespace Textmining.Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor and Index Action

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _urlPath;
        private readonly IConfiguration _config;
        private readonly IToastNotification _toastNotification;
        private static string _feedback;
        private static string _token;
        private readonly string _currentPath;

        public HomeController(IConfiguration config, IWebHostEnvironment env, IToastNotification toastNotification)
        {
            _config = config;
            _currentPath = env.WebRootPath;
            _token = _config.GetValue<string>("TextMiningApiToken");
            _urlPath = _config.GetValue<string>("ApiUrl"); //"https://api.text-mining.ir/api/";
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Virastar"); //SpellCorrector
        }

        #endregion

        #region FormalConverter

        public IActionResult FormalConverter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FormalConverter(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var apiOutput = CallApi($"{_urlPath}TextRefinement/FormalConverter", model.InputText);

                if (apiOutput.Item2)
                {
                    ViewData["Output"] = apiOutput.Item1;
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region SpellCorrector
        public IActionResult SpellCorrector()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SpellCorrector(SpellCheckerModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                var apiOutput = CallApi($"{_urlPath}TextRefinement/SpellCorrector", model);

                if (apiOutput.Item2)
                {
                    ViewData["Output"] = apiOutput.Item1;
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region SwearWordTagger

        public IActionResult SwearWordTagger()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwearWordTagger(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var apiOutput = CallApi($"{_urlPath}TextRefinement/SwearWordTagger", model.InputText);

                if (apiOutput.Item2)
                {
                    var viewModel = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiOutput.Item1);
                    return PartialView("_SwearTaggerOutput", viewModel);
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region SentimentClassifier
        public IActionResult SentimentClassifier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SentimentClassifier(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var apiOutput = CallApi($"{_urlPath}SentimentAnalyzer/SentimentClassifier2", model.InputText);
                if (apiOutput.Item2)
                {
                    var viewModel = JsonConvert.DeserializeObject<int>(apiOutput.Item1);
                    return PartialView("_SentimentClassifierOutput", viewModel);
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region NamedEntityRecognitionDetect
        public IActionResult NamedEntityRecognitionDetect()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NamedEntityRecognitionDetect(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var apiOutput = CallApi($"{_urlPath}NamedEntityRecognition/Detect", model.InputText);

                if (apiOutput.Item2)
                {
                    var viewModel = JsonConvert.DeserializeObject<List<Phrase>>(apiOutput.Item1);
                    return PartialView("_NamedEntityRecognitionDetectOutput", viewModel);
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region LanguageDetectionPredict
        public IActionResult LanguageDetectionPredict()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LanguageDetectionPredict(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var result = CallApi($"{_urlPath}LanguageDetection/Predict", model.InputText);
                if (result.Item2)
                {
                    ViewData["Output"] = result.Item1;
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(result.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region LemmatizeText2Text
        public IActionResult LemmatizeText2Text()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LemmatizeText2Text(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var result = CallApi($"{_urlPath}Stemmer/LemmatizeText2Text", model.InputText);
                if (result.Item2)
                {
                    ViewData["Output"] = result.Item1;
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(result.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region GetMostSimilarWord
        public IActionResult GetMostSimilarWord()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetMostSimilarWord(GetMostSimilarWordModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Word))
            {
                //ViewData["Output"] = CallApi($"{_urlPath}TextSimilarity/GetMostSimilarWord", model);
                //return PartialView("_ApiOutput");
                var result = CallApi($"{_urlPath}TextSimilarity/GetMostSimilarWord", model);
                if (result.Item2)
                {
                    var viewModel = JsonConvert.DeserializeObject<string[]>(result.Item1);
                    return PartialView("_GetMostSimilarWordOutput", viewModel);
                }
                else
                {
                    ShowError(result.Item1);
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region KeywordExtraction
        public IActionResult KeywordExtraction()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KeywordExtraction(KeywordExtractionModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                model.MinWordLength = 3;
                //model.MaxWordCount = 3;
                model.MinKeywordFrequency = 1;
                //model.Method = "TFIDF";
                model.MaxWordCount = Math.Min(5, Math.Max(1, model.MaxWordCount));

                var apiOutput = CallApi($"{_urlPath}InformationRetrieval/KeywordExtraction", model);

                if (apiOutput.Item2)
                {
                    var viewModel = JsonConvert.DeserializeObject<Dictionary<string, double>>(apiOutput.Item1);
                    return PartialView("_KeywordExtractionOutput", viewModel);
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region Summarization
        public IActionResult Summarization()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summarization(SummarizationModel model)
        {
            model.SummaryWordCount = Math.Min(1000, Math.Max(10, model.SummaryWordCount));

            var apiOutput = CallApi($"{_urlPath}InformationRetrieval/Summarize", model);

            if (apiOutput.Item2)
            {
                ViewData["Output"] = apiOutput.Item1;
                return PartialView("_ApiOutput");
            }
            else
            {
                ShowError(apiOutput.Item1);
                return new EmptyResult();
            }
        }

        #endregion

        #region Topic Modeling
        public IActionResult TopicModeling()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TopicModeling(TopicModelingModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                model.TopicsCount = Math.Min(10, Math.Max(2, model.TopicsCount));
                model.WordsPerTopicCount = 10;
                model.Documents = model.Text.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

                var apiOutput = CallApi($"{_urlPath}InformationRetrieval/TopicModeling", model);

                if (apiOutput.Item2)
                {
                    var result = (List<Dictionary<string, double>>) JsonConvert.DeserializeObject(apiOutput.Item1,
                        typeof(List<Dictionary<string, double>>));
                    if (result == null)
                        ViewData["Output"] = apiOutput.Item1;
                    else
                        ViewData["Output"] = JsonConvert.SerializeObject(result.Select(l => l.Keys).ToArray());
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(apiOutput.Item1);
                }
            }
            return new EmptyResult();
        }

        #endregion

        #region Virastar
        public IActionResult Virastar()
        {
            CheckFeedbackNotif();

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Virastar(VirastarModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                model.SpellCheckerCandidateCount = Math.Min(5, Math.Max(1, model.SpellCheckerCandidateCount));

                var result = CallApi($"{_urlPath}Virastar/ScanText", new
                {
                    model.Text,
                    ReturnOnlyChangedTokens = false,
                    SpellConfiguration = new
                    {
                        LexicalSpellCheckSuggestionCount = model.SpellCheckerCandidateCount,
                        RealWordAlternativeSuggestionCount = 0,
                        LexicalSpellCheckHighSensitive = model.SpellCheckHighSensitive,
                        ContextSpellCheckHighSensitive = model.SpellCheckHighSensitive
                    }
                });

                if (result.Item2)
                {
                    var viewModel = JsonConvert.DeserializeObject<List<TokenInfo>>(result.Item1);
                    if (viewModel != null)
                        foreach (TokenInfo tokenInfo in viewModel)
                            tokenInfo.EditList.Reverse();

                    return PartialView("_VirastarOutput", viewModel);
                }
                else
                {
                    ShowError(result.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region PunctuationRestoration
        public IActionResult PunctuationRestoration()
        {
            //CheckFeedbackNotif();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PunctuationRestoration(ApiViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var result = CallApi($"{_urlPath}Virastar/PunctuationRestoration", new
                {
                    Text = model.InputText
                });

                if (result.Item2)
                {
                    ViewData["Output"] = result.Item1;
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(result.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region Document Similarity
        public IActionResult DocumentSimilarity()
        {
            //CheckFeedbackNotif();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DocumentSimilarity(DocumentSimilarityModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Document1) && !string.IsNullOrWhiteSpace(model.Document2))
            {
                var result = CallApi($"{_urlPath}TextSimilarity/DocumentSimilarity", model);

                if (result.Item2)
                {
                    ViewData["Output"] = $"میزان شباهت دو عبارت فوق =  {Convert.ToDouble(result.Item1) * 100.0}%";
                    return PartialView("_ApiOutput");
                }
                else
                {
                    ShowError(result.Item1);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region WordCloud
        public IActionResult WordCloud()
        {
            CheckFeedbackNotif();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WordCloud(WordCloudModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                try
                {
                    string[] words = model.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    // اگر به جای لیست کلمات متن وارد کرده بود
                    if (words.Length < 20 || words.Average(w => w.Length) > 20)
                    {
                        var model2 = new KeywordExtractionModel
                        {
                            MinWordLength = 3,
                            MaxWordCount = 3,
                            MinKeywordFrequency = 1,
                            ResultKeywordCount = 500,
                            Method = "FNG",
                            Text = model.Text
                        };

                        try
                        {
                            var apiOutput = CallApi($"{_urlPath}InformationRetrieval/KeywordExtraction", model2);
                            if (apiOutput.Item2)
                            {
                                var viewModel = JsonConvert.DeserializeObject<Dictionary<string, double>>(apiOutput.Item1);
                                words = viewModel?.Keys.ToArray() ?? Array.Empty<string>();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex);
                            ShowError("خطا در استخراج کلمات کلیدی");
                            return new EmptyResult();
                        }
                    }

                    string jsonStr = JsonConvert.SerializeObject(new
                        {
                            Words = words, model.Theme, //FontName = "Samim"
                        }
                    );

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJWTToken());
                    var response = client.PostAsync($"{_urlPath}InformationRetrieval/WordCloudGeneration",
                        new StringContent(jsonStr, Encoding.UTF8, "application/json")).Result;
                    var bytesArray = response.Content.ReadAsByteArrayAsync().Result;


                    string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".jpg";
                    string filePath = Path.Combine(_currentPath, "wordcloud", fileName);
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    using (var ms = new MemoryStream(bytesArray))
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                    //Image.FromStream(ms).Save(filePath);
                    return PartialView("_WordCloudOutput", "~/wordcloud/" + fileName);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    ShowError("خطا در ترسیم ابر کلمات");
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region Common Methods   

        private string GetJWTToken()
        {
            if (!string.IsNullOrEmpty(_token))
                return _token;
            
            var textMiningApiKey = _config.GetValue<string>("TextMiningApiKey");
            string jwtToken = string.Empty;
            var client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            var response = client.GetAsync($"{_urlPath}Token/GetToken?apikey={textMiningApiKey}").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                jwtToken = (string)JObject.Parse(res)["token"];
            }

            return jwtToken;
        }

        private (string, bool) CallApi(string apiUrl, object inputModel)
        {
            try
            {
                //ToDo: check model validation
                var client = new HttpClient();
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJWTToken());

                var contentBody = new StringContent(ObjectModelToString(inputModel), Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, contentBody).Result;
                if (!response.IsSuccessStatusCode)
                    _token = null;
                string resp = response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : response.ToString();
                return (resp, response.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {
                //ToDo: better error message
                _logger.Error(ex);
                return ($"error: {ex.Message}", false);
            }
        }

        private string ObjectModelToString(object model)
        {
            string jsonStr = JsonConvert.SerializeObject(model);
            return jsonStr;
        }

        private void ShowError(string msg)
        {
            try
            {
                var viewModel = JObject.Parse(msg);
                
                string errorMessage = "نامشخص";
                if(viewModel.ContainsKey("error") && viewModel["error"] != null)
                    errorMessage = viewModel["error"].Value<string>();
                else if (viewModel.ContainsKey("message") && viewModel["message"] != null)
                    errorMessage = viewModel["message"].Value<string>();
                else if(viewModel.First?.First != null && !string.IsNullOrWhiteSpace(viewModel.First.First[0]?.Value<string>()))
                    errorMessage = viewModel.First.First[0].Value<string>();

                _toastNotification.AddErrorToastMessage(
                    string.IsNullOrWhiteSpace(errorMessage) ? msg : errorMessage,
                    new ToastrOptions
                    {
                        Title = "خطا"
                    });
            }
            catch
            {
                _toastNotification.AddErrorToastMessage(msg,
                    new ToastrOptions()
                    {
                        Title = "خطا"
                    });
            }
        }
        
        #endregion

        #region Report Error 

        HashSet<char> _persianChars = new HashSet<char>(
            "ابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیآيءئأإؤ".ToCharArray());
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ReportError(string errorReportInputText, string errorReportComment)
        {
            if ((string.IsNullOrWhiteSpace(errorReportInputText) || errorReportInputText.Count(c => _persianChars.Contains(c)) < 3) &&
                (string.IsNullOrWhiteSpace(errorReportComment) || errorReportComment.Count(c => _persianChars.Contains(c)) < 3))
            {
                _feedback = "متن ورودی و توضیحات باید بصورت متن فارسی وارد شود.";
            }
            else
            {
                try
                {
                    SmtpClient smtp = new SmtpClient(_config.GetValue<string>("ErrorReport:SmtpHost"))
                    {
                        Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("ErrorReport:SmtpUsername"),
                            _config.GetValue<string>("ErrorReport:SmtpPassword")),
                        //Timeout = 300000
                        //Port = 587,
                        //EnableSsl = true
                    };
                    /*#if DEBUG
                                         return RedirectToAction("Index");
                    #endif*/

                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(_config.GetValue<string>("ErrorReport:SmtpUsername")),
                        IsBodyHtml = true,
                        Subject = "گزارش خطا در دمو ابزارهای فارسی‌یار"
                    };
                    mail.To.Add(_config.GetValue<string>("ErrorReport:ReportEmail"));
                    mail.Body = "<p dir='rtl'>" +
                                $"ورودی: {errorReportInputText} " +
                                "<br/> " +
                                $"توضیحات: {errorReportComment}" +
                                "<br/> " +
                                $"آدرس ابزار: {Request.Headers["Referer"]}" +
                                "</p>";

                    /*smtp.Send(mail);
                    smtp.Dispose();
                    mail.Dispose();*/
                    smtp.SendCompleted += (_, _) => {
                        smtp.Dispose();
                        mail.Dispose();
                    };
                    smtp.SendAsync(mail, null);
                    _feedback = "OK";
                }
                catch (Exception ex)
                {
                    _feedback = ex.Message;
                    if (ex.InnerException != null)
                        _feedback += Environment.NewLine + ex.InnerException.Message;
                }
            }

            return RedirectToAction("Index");
        }

        private void CheckFeedbackNotif()
        {
            if (!string.IsNullOrWhiteSpace(_feedback))
            {
                if (_feedback == "OK")
                    _toastNotification.AddSuccessToastMessage("با تشکر از همکاری شما",
                        new ToastrOptions()
                        {
                            Title = "ارسال موفق گزارش"
                        });
                else if (_feedback.Length > 0)
                    _toastNotification.AddErrorToastMessage("خطایی در ارسال گزارش به وجود آمد: " + _feedback,
                        new ToastrOptions()
                        {
                            Title = "خطا"
                        });
            }

            _feedback = string.Empty;
        }

        #endregion
    }
}
