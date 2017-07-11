using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Surveys.BO;
using Surveys.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Web.Controllers
{
    public class SurveyController : Controller
    {
        SurveyService _service;
        WebSettings _settings;

        public SurveyController(SurveyService service, IOptions<WebSettings> settings)
        {
            _service = service;
            _settings = settings.Value;
        }

        private string ResolveSurveyId()
        {
            var valueProvider = CompositeValueProvider.CreateAsync(ControllerContext).Result;

            var idValue = valueProvider.GetValue("id");
            if (idValue == null)
                return null;

            var idValueStr = idValue.ConvertTo(typeof(string)) as string;
            if (idValueStr == null)
                return null;

            return idValueStr;
        }

        private SurveyBO ResolveSurvey()
        {
            var idValueStr = ResolveSurveyId();

            if (idValueStr == null)
                return null;

            var result = _service.GetSurveyByUrl(idValueStr);
            if (result == null)
                return null;

            _service.FetchAnswersForClient(result, ResolveClientId());

            return result;
        }

        public ActionResult Index()
        {
            var survey = ResolveSurvey();
            if (survey == null)
                return NotFound();

            var model = new SurveyIndexViewModel(survey);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(object dummy)
        {
            var survey = ResolveSurvey();
            if (survey == null || survey.IsClosed)
                return NotFound();

            var model = new SurveyIndexViewModel(survey);
            TryUpdateModelAsync(model.Answers, "Answers").Wait();

            if (!ViewData.ModelState.IsValid)
            {
                return View(model);
            }

            model.UpdateBO();

            var clientIdentity = ResolveClientId();

            _service.SaveAnswers(survey, clientIdentity);

            return RedirectToAction("SendSurveyResult", new { id = survey.Url });
        }

        private ClientIdentityBO ResolveClientId()
        {
            return new BO.ClientIdentityBO()
            {
                Id = (Guid)HttpContext.Items[ClientIdMiddleware.ContextClientId]
            };
        }

        public ActionResult SendSurveyResult()
        {
            var survey = ResolveSurvey();
            if (survey == null || survey.IsClosed)
                return NotFound();

            var model = new SurveyIndexViewModel(survey);
            return View(model);
        }

        public ActionResult Report(string key)
        {
            if(!string.Equals(key, _settings.ReportPassword, StringComparison.OrdinalIgnoreCase))
            {
                return NotFound();
            }

            var survey = ResolveSurvey();
            if (survey == null)
                return NotFound();

            var report = _service.GenerateReport(survey);
            return View(report);
        }
    }
}
