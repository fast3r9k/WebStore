using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{

    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRoute : TagHelper
    {

        private const string AttributeName = "is-active-route";
        private const string IgnoreAction = "ws-ignore-action";

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        [ViewContext,HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignore_action = output.Attributes.ContainsName(IgnoreAction);

            if (IsActive(ignore_action))
                MakeActive(output);

            output.Attributes.RemoveAll(AttributeName); 
        }

        private bool IsActive(bool IsIgnoreAction)
        {
            var routeValues = ViewContext.RouteData.Values;


            var currentController = routeValues["controller"]?.ToString();
            var currentAction = routeValues["Action"]?.ToString();

            const StringComparison strComp= StringComparison.OrdinalIgnoreCase;

            if (!string.IsNullOrEmpty(Controller) && !string.Equals(currentController, Controller, strComp))
                return false;

            if (!IsIgnoreAction && !string.IsNullOrEmpty(Action) && !string.Equals(currentAction, Action, strComp))
                return false;

            foreach (var (key,value) in RouteValues)
                if (!routeValues.ContainsKey(key) || routeValues[key]?.ToString() != value)
                    return false;

            return true;
        }

        private static void MakeActive(TagHelperOutput output)
        {
            var classAttribute = output.Attributes.FirstOrDefault(attr => attr.Name == "class");

            if(classAttribute is null)
                output.Attributes.Add("class","active");
            else
            {
                if(classAttribute.Value.ToString()?.Contains("active") ?? false)
                    return;
                output.Attributes.SetAttribute("class",classAttribute.Value + "active");

            }
        }
    }
}
