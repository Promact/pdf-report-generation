using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RenderRazorPageToHtml.Helpers
{
    public class RazorViewToStringRenderer
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        public RazorViewToStringRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderViewToStringAsync(ActionContext actionContext, string viewName, object model, bool partial = false)
        {
            var viewEngineResult = _viewEngine.GetView(null, viewName, !partial);

            if (!viewEngineResult.Success)
            {
                viewEngineResult = _viewEngine.FindView(actionContext, viewName, !partial);
            }

            if (!viewEngineResult.Success)
            {
                // Log the locations searched for the view
                var searchedLocations = string.Join(Environment.NewLine, viewEngineResult.SearchedLocations);
                throw new FileNotFoundException($"View '{viewName}' not found. Searched locations: {searchedLocations}");
            }

            var view = viewEngineResult.View;

            using (var sw = new StringWriter())
            {
                var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var tempDataDictionary = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);

                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewDataDictionary,
                    tempDataDictionary,
                    sw,
                    new HtmlHelperOptions()
                );

                await view.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
