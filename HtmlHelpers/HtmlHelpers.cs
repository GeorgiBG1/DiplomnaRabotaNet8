using Microsoft.AspNetCore.Html;

namespace HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent RenderStars(int stars)
        {
            var builder = new HtmlContentBuilder();
            for (int i = 1; i <= 5; i++)
            {
                if (i <= stars)
                {
                    builder.AppendHtml("<i class='fas fa-star'></i>");
                }
                else
                {
                    builder.AppendHtml("<i class='far fa-star'></i>");
                }
            }
            return builder;
        }
    }
}
