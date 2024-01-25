using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCProje.Extensions
{
    [HtmlTargetElement("custom-tag-button")]
    public class CustomTagHelper : TagHelper
    {
        public string Type { get; set; }
        public string ButtonClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("type", Type);
            output.Attributes.SetAttribute("class", ButtonClass);

            var content = context.AllAttributes["content"]?.Value;

            if (content != null)
            {
                output.Content.SetContent(content.ToString());
            }
        }
    }
}
