using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private static int _feedback;
        private static string _token;
        private readonly string _currentPath;

        public HomeController(IConfiguration config, IHostingEnvironment env, IToastNotification toastNotification)
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
            var apiOutput = CallApi($"{_urlPath}TextRefinement/FormalConverter", model.InputText);

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

        #region SpellCorrector
        public IActionResult SpellCorrector()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SpellCorrector(SpellCheckerModel model)
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
                return new EmptyResult();
            }
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
            var apiOutput = CallApi($"{_urlPath}TextRefinement/SwearWordTagger", model.InputText);

            if (apiOutput.Item2)
            {
                var viewModel = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiOutput.Item1);
                return PartialView("_SwearTaggerOutput", viewModel);
            }
            else
            {
                ShowError(apiOutput.Item1);
                return new EmptyResult();
            }
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
            var apiOutput = CallApi($"{_urlPath}SentimentAnalyzer/SentimentClassifier", model.InputText);
            if (apiOutput.Item2)
            {
                var viewModel = JsonConvert.DeserializeObject<int>(apiOutput.Item1);
                return PartialView("_SentimentClassifierOutput", viewModel);
            }
            else
            {
                ShowError(apiOutput.Item1);
                return new EmptyResult();
            }
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
            var apiOutput = CallApi($"{_urlPath}NamedEntityRecognition/Detect", model.InputText);

            if (apiOutput.Item2)
            {
                var viewModel = JsonConvert.DeserializeObject<List<Phrase>>(apiOutput.Item1);
                return PartialView("_NamedEntityRecognitionDetectOutput", viewModel);
            }
            else
            {
                ShowError(apiOutput.Item1);
                return new EmptyResult();
            }
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
            var result = CallApi($"{_urlPath}LanguageDetection/Predict", model.InputText);
            if (result.Item2)
            {
                ViewData["Output"] = result.Item1;
                return PartialView("_ApiOutput");
            }
            else
            {
                ShowError(result.Item1);
                return new EmptyResult();
            }
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
            var result = CallApi($"{_urlPath}Stemmer/LemmatizeText2Text", model.InputText);
            if (result.Item2)
            {
                ViewData["Output"] = result.Item1;
                return PartialView("_ApiOutput");
            }
            else
            {
                ShowError(result.Item1);
                return new EmptyResult();
            }
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
                return new EmptyResult();
            }
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
                return new EmptyResult();
            }
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
            model.TopicsCount = Math.Min(10, Math.Max(2, model.TopicsCount));
            model.WordsPerTopicCount = 10;
            model.Documents = model.Text.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            var apiOutput = CallApi($"{_urlPath}InformationRetrieval/TopicModeling", model);

            if (apiOutput.Item2)
            {
                var result = (List<Dictionary<string, double>>) JsonConvert.DeserializeObject(apiOutput.Item1,
                    typeof(List<Dictionary<string, double>>));
                if(result==null)
                    ViewData["Output"] = apiOutput.Item1;
                else 
                    ViewData["Output"] = JsonConvert.SerializeObject(result.Select(l => l.Keys).ToArray());
                return PartialView("_ApiOutput");
            }
            else
            {
                ShowError(apiOutput.Item1);
                return new EmptyResult();
            }
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
            model.SpellCheckerCandidateCount = Math.Min(5, Math.Max(1, model.SpellCheckerCandidateCount));

            var result = CallApi($"{_urlPath}Virastar/ScanText", model);

            if (result.Item2)
            {
                var viewModel = JsonConvert.DeserializeObject<List<TokenInfo>>(result.Item1);
                foreach (TokenInfo tokenInfo in viewModel)
                    tokenInfo.EditList.Reverse();

                return PartialView("_VirastarOutput", viewModel);
            }
            else
            {
                ShowError(result.Item1);
                return new EmptyResult();
            }
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
            try
            {
                string jsonStr = JsonConvert.SerializeObject(new
                    {
                        Words = model.Text.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries),
                        Theme = model.Theme,
                        //FontName = "Samim"
                    }
                );

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetJWTToken());
                var response = client.PostAsync($"{_urlPath}InformationRetrieval/WordCloudGeneration",
                    new StringContent(jsonStr, Encoding.UTF8, "application/json")).Result;
                var bytesArray = response.Content.ReadAsByteArrayAsync().Result;
                using (var ms = new MemoryStream(bytesArray))
                {
                    Image image = Image.FromStream(ms);
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".jpg";
                    image.Save(Path.Combine(_currentPath, "wordcloud", fileName));
                    return PartialView("_WordCloudOutput", "~/wordcloud/" + fileName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                ShowError("خطا در ترسیم ابر کلمات");
                return new EmptyResult();
            }
        }
        #endregion

        #region Common Methods   

        private string GetJWTToken()
        {
            if (!string.IsNullOrEmpty(_token))
                return _token;
            
            var textMiningApiKey = _config.GetValue<string>("TextMiningApiKey");
            string jwtToken = string.Empty;
            var client = new RestClient($"{_urlPath}Token/GetToken?apikey={textMiningApiKey}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _token = jwtToken = (string)JObject.Parse(response.Content)["token"];
            }

            return jwtToken;
        }

        private (string, bool) CallApi(string apiUrl, object inputModel)
        {
            try
            {
                //ToDo: check model validation
                var client = new RestClient(apiUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Authorization", $"Bearer {GetJWTToken()}");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("input", ObjectModelToString(inputModel), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (!response.IsSuccessful)
                    _token = null;
                return (response.Content, response.IsSuccessful);
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
                
                string errorMessage;
                if(viewModel.ContainsKey("error"))
                    errorMessage = viewModel["error"].Value<string>();
                else if (viewModel.ContainsKey("message"))
                    errorMessage = viewModel["message"].Value<string>();
                else errorMessage = viewModel.First.First[0].Value<string>();

                _toastNotification.AddErrorToastMessage(errorMessage,
                    new ToastrOptions()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ReportError(string errorReportInputText, string errorReportComment)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(_config.GetValue<string>("ErrorReport:SmtpHost"))
                {
                    Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("ErrorReport:SmtpUsername"),
                        _config.GetValue<string>("ErrorReport:SmtpPassword"))
                    //,Timeout = 30000
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
                mail.Body = $"<p dir='rtl'>" +
                            $"ورودی: {errorReportInputText} " +
                            $"<br/> " +
                            $"توضیحات:{errorReportComment}" +
                            $"<br/> " +
                            $"آدرس ابزار:{Request.Headers["Referer"].ToString()}" +
                            $"</p>";

                //smtp.Send(mail);
                smtp.SendCompleted += (s, e) => {
                    smtp.Dispose();
                    mail.Dispose();
                };
                smtp.SendAsync(mail, null);
                _feedback = 1;
            }
            catch //(Exception ex)
            {
                _feedback = 2;
            }

            return RedirectToAction("Index");
        }

        private void CheckFeedbackNotif()
        {
            if(_feedback == 1)
                _toastNotification.AddSuccessToastMessage("با تشکر از همکاری شما",
                    new ToastrOptions()
                    {
                        Title = "ارسال موفق گزارش"
                    });
            else if(_feedback == 2)
                _toastNotification.AddErrorToastMessage("خطایی در ارسال گزارش به وجود آمد",
                    new ToastrOptions()
                    {
                        Title = "خطا"
                    });

            _feedback = 0;
        }

        #endregion
    }
}
