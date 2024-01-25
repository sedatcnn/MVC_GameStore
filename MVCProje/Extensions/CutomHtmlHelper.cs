using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq.Expressions;
using System.Text;

namespace MVCProje.Extensions
{
    public static class CutomHtmlHelper
    {
        public static IHtmlContent MyTextBoxFor<TModel, TValue>(
             this IHtmlHelper<TModel> helper,
             Expression<Func<TModel, TValue>> expression,
             string labelText,
             object value,
             string backgroundColor,
             object htmlAttributes)
        {
            var labelTagBuilder = new TagBuilder("label");
            labelTagBuilder.AddCssClass("col-md-4 form-label");
            labelTagBuilder.Attributes.Add("for", helper.IdFor(expression));
            labelTagBuilder.InnerHtml.Append(labelText);

            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("form-group");
            mainDiv.InnerHtml.AppendHtml(labelTagBuilder);

            var colDivTagBuilder = new TagBuilder("div");
            colDivTagBuilder.AddCssClass("col-md-8");

            var inputTagBuilder = new TagBuilder("input");
            inputTagBuilder.AddCssClass("form-control");
            inputTagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            inputTagBuilder.MergeAttribute("id", helper.IdFor(expression));
            inputTagBuilder.MergeAttribute("name", helper.NameFor(expression));
            inputTagBuilder.MergeAttribute("value", value?.ToString());

            if (!string.IsNullOrEmpty(backgroundColor))
            {
                inputTagBuilder.MergeAttribute("style", $"background-color: {backgroundColor};");
            }

            colDivTagBuilder.InnerHtml.AppendHtml(inputTagBuilder);

            mainDiv.InnerHtml.AppendHtml(colDivTagBuilder);

            return mainDiv;
        }
    }
}
