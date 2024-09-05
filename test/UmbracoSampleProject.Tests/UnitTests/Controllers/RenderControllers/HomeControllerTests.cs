using AutoFixture.NUnit3;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoSampleProject.Core.Controllers.RenderControllers;
using UmbracoSampleProject.Core.Services;
using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Tests.Common;
using Umbraco.Cms.Web.Common.Routing;
using UmbracoSampleProject.Tests.UnitTests.Helpers;
using Microsoft.AspNetCore.Routing;
using UmbracoSampleProject.Models.Models.ViewModels;

namespace UmbracoSampleProject.Tests.UnitTests.Controllers.RenderControllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IBasePageService> _basePageService;
        private Mock<IPublishedValueFallback> _publishedValueFallback;
        private HomeController _controller;
        private IUmbracoContextAccessor _umbracoContextAccessor;
        private Mock<IPublishedContent> _publishedContent;
        [SetUp]
        public void SetUp()
        {
            _umbracoContextAccessor = new TestUmbracoContextAccessor();
            _basePageService = new Mock<IBasePageService>();
            _publishedValueFallback = new Mock<IPublishedValueFallback>();

            var globalSettings = new GlobalSettings();
            var backofficeSecurityAccessor = Mock.Of<IBackOfficeSecurityAccessor>();
            Mock.Get(backofficeSecurityAccessor).Setup(x => x.BackOfficeSecurity).Returns(Mock.Of<IBackOfficeSecurity>());
            var umbracoContextFactory = TestUmbracoContextFactory.Create(globalSettings, _umbracoContextAccessor);

            var umbracoContextReference = umbracoContextFactory.EnsureUmbracoContext();
            var umbracoContext = umbracoContextReference.UmbracoContext;

            var umbracoContextAccessor = new TestUmbracoContextAccessor(umbracoContext);

            _publishedContent = new Mock<IPublishedContent>();
            _publishedContent.Setup(x => x.Id).Returns(1234);
            _publishedContent.Setup(x => x.ContentType.Alias).Returns("home");
            var builder = new PublishedRequestBuilder(umbracoContext.CleanedUmbracoUrl, Mock.Of<IFileService>());
            builder.SetPublishedContent(_publishedContent.Object);
            var publishedRequest = builder.Build();

            var routeDefinition = new UmbracoRouteValues(publishedRequest, null);

            var httpContext = new DefaultHttpContext();
            httpContext.Features.Set(routeDefinition);

            _controller = new HomeController(_basePageService.Object, _publishedValueFallback.Object,Mock.Of<ILogger<RenderController>>(), Mock.Of<ICompositeViewEngine>(), _umbracoContextAccessor)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext, RouteData = new RouteData() },
            };
        }

        [Test, AutoData]
        public void When_PageAction_ThenResultIsIsAssignableFromContentResult()
        {
            //arrange
            var basePageViewMode = new BasePageViewModel(_publishedContent.Object);
            _basePageService.Setup(x=>x.GetModel(It.IsAny<IPublishedContent>())).Returns(basePageViewMode);
            //act
            var result = _controller.Index();
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
