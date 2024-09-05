using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSampleProject.Models.Models.ViewModels
{
    public class BasePageViewModel : ContentModel
    {
        public BasePageViewModel(IPublishedContent? content) : base(content)
        {
        }

        public string Culture { get; set; }
        public string BodyClass { get; set; }
        public Base PageModel { get; set; }
        public Home HomePage { get; set; }

    }
}
