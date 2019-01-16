using BikeStore.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BikeStore.TagHelpers {
  // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
  
  public class PageLinkTagHelper : TagHelper {
    private IUrlHelperFactory mUrlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory xUrlHelperFactory)
    {
      mUrlHelperFactory = xUrlHelperFactory;

    }
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }
    public PagingInfo PageModel { get; set; }
    public string PageAction { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      IUrlHelper UrlHelper = mUrlHelperFactory.GetUrlHelper(ViewContext);
      TagBuilder Result = new TagBuilder("div");
      for (int i = 0; i < PageModel.TotalPage; i++)
      {
        TagBuilder tag = new TagBuilder("a");
        tag.Attributes["href"] = UrlHelper.Action(PageAction, new { productPage = i });
        tag.InnerHtml.Append(i.ToString());
        Result.InnerHtml.AppendHtml(tag);
      }
      output.Content.AppendHtml(Result.InnerHtml);
    }
  }
} 
