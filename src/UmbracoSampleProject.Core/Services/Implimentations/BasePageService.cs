using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoSampleProject.Models;
using UmbracoSampleProject.Models.Models.ViewModels;

namespace UmbracoSampleProject.Core.Services.Implimentations
{
    public class BasePageService : IBasePageService
    {
        private readonly ILogger<BasePageService> _logger;

        public BasePageService(ILogger<BasePageService> logger)
        {
            _logger = logger;
        }

        public BasePageViewModel GetModel(IPublishedContent content)
        {
            var homePage=content.Root() as Home;
            var culture = Thread.CurrentThread.CurrentCulture.Name;
            var baseModel = new BasePageViewModel(content)
            {
                Culture = culture,
                HomePage=homePage
            };
            return baseModel;
        }
    }
}
