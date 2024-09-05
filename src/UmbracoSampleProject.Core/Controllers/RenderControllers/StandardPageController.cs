using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoSampleProject.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoSampleProject.Core.Services;

namespace UmbracoSampleProject.Core.Controllers.RenderControllers
{
    public class StandardPageController : RenderController
    {
        private readonly IBasePageService _basePageService;
        private readonly IPublishedValueFallback _publishedValueFallback;
        public StandardPageController(IBasePageService basePageService, IPublishedValueFallback publishedValueFallback, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _basePageService = basePageService;
            _publishedValueFallback = publishedValueFallback;
        }
        public IActionResult Index()
        {
            var baseModel = _basePageService.GetModel(CurrentPage);
            var pageModel = new StandardPage(CurrentPage,_publishedValueFallback);
            baseModel.PageModel = pageModel;
            return CurrentTemplate(baseModel);
        }
    }
}
