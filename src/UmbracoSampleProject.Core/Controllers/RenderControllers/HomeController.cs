using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoSampleProject.Core.Services;
using UmbracoSampleProject.Core.Services.Implimentations;
using UmbracoSampleProject.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSampleProject.Core.Controllers.RenderControllers
{
    public class HomeController : RenderController
    {
        private readonly IBasePageService _basePageService;
        private readonly IPublishedValueFallback _publishedValueFallback;
        public HomeController(IBasePageService basePageService, IPublishedValueFallback publishedValueFallback, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor) {
            _basePageService=basePageService;
            _publishedValueFallback=publishedValueFallback;
        }

        public override IActionResult Index()
        {
            var baseModel = _basePageService.GetModel(CurrentPage);
            var pageModel = new Home(CurrentPage,_publishedValueFallback);
            baseModel.PageModel = pageModel;
            return View("~/Views/Home.cshtml", baseModel);
        }
    }
}
