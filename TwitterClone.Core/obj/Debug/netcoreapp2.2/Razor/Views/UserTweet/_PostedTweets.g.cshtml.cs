#pragma checksum "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6cafbf2df2271d479ca256bac1ebe37a8a35e0ed"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserTweet__PostedTweets), @"mvc.1.0.view", @"/Views/UserTweet/_PostedTweets.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/UserTweet/_PostedTweets.cshtml", typeof(AspNetCore.Views_UserTweet__PostedTweets))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\_ViewImports.cshtml"
using TwitterClone.Core;

#line default
#line hidden
#line 2 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\_ViewImports.cshtml"
using TwitterClone.Core.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6cafbf2df2271d479ca256bac1ebe37a8a35e0ed", @"/Views/UserTweet/_PostedTweets.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0a9a8a8dccf8856e72cfe1a5c4ff4824f3adc942", @"/Views/_ViewImports.cshtml")]
    public class Views_UserTweet__PostedTweets : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TwitterClone.Core.Models.ViewModels.TweetViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/TwitterS.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:30px;height:30px; float:left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(72, 36, true);
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n<div class=\"card-group\">\r\n");
            EndContext();
#line 8 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
            BeginContext(149, 327, true);
            WriteLiteral(@"        <div class=""Grid-cell panel panel-primary"" style=""margin-top:5px; margin-left:5px; width:400px"">

            <div class=""card border-success mb-3"">
                <div class=""card-header border-primary postedTweetHeader"">
                    <div style=""display:flex;align-items:center"">
                        ");
            EndContext();
            BeginContext(476, 78, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6cafbf2df2271d479ca256bac1ebe37a8a35e0ed4871", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(554, 174, true);
            WriteLiteral("\r\n                        <h4 style=\"font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif; font-style:italic;font-size:12px\">First Posted&nbsp;by&nbsp;<strong>");
            EndContext();
            BeginContext(729, 13, false);
#line 16 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                                                                                                                                                                       Write(item.UserName);

#line default
#line hidden
            EndContext();
            BeginContext(742, 23, true);
            WriteLiteral("</strong>&nbsp;on&nbsp;");
            EndContext();
            BeginContext(766, 22, false);
#line 16 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                                                                                                                                                                                                            Write(item.DisplayCreateDate);

#line default
#line hidden
            EndContext();
            BeginContext(788, 155, true);
            WriteLiteral("</h4>\r\n                    </div>\r\n                </div>\r\n                <div class=\"card-body postedTweet\">\r\n\r\n                    <p class=\"card-text\">");
            EndContext();
            BeginContext(944, 12, false);
#line 21 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                                    Write(item.Content);

#line default
#line hidden
            EndContext();
            BeginContext(956, 34, true);
            WriteLiteral("</p>\r\n\r\n                </div>\r\n\r\n");
            EndContext();
#line 25 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                 if (item.DisplayModifedDate != null)
                {

#line default
#line hidden
            BeginContext(1064, 225, true);
            WriteLiteral("                    <div class=\"modifiedCardHeader card-header\">\r\n                        <h4 style=\"font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif; font-style:italic;font-size:12px\">Modifed&nbsp;on&nbsp;");
            EndContext();
            BeginContext(1290, 23, false);
#line 28 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                                                                                                                                                          Write(item.DisplayModifedDate);

#line default
#line hidden
            EndContext();
            BeginContext(1313, 35, true);
            WriteLiteral("</h4>\r\n                    </div>\r\n");
            EndContext();
#line 30 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                }

#line default
#line hidden
            BeginContext(1367, 43, true);
            WriteLiteral("                <div class=\"card-footer\">\r\n");
            EndContext();
#line 32 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                     if (item.OwnTweet)
                    {
                    

#line default
#line hidden
            BeginContext(1667, 79, true);
            WriteLiteral("                    <a class=\"EdgeButton EdgeButton--primary EdgeButton--small\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1746, "\"", 1783, 3);
            WriteAttributeValue("", 1756, "FunctionEditTweet(", 1756, 18, true);
#line 36 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
WriteAttributeValue("", 1774, item.Id, 1774, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 1782, ")", 1782, 1, true);
            EndWriteAttribute();
            BeginContext(1784, 37, true);
            WriteLiteral("><i class=\"fa fa-edit\"></i>Edit</a>\r\n");
            EndContext();
            BeginContext(1823, 159, true);
            WriteLiteral("                    <button type=\"button\" class=\"EdgeButton EdgeButton--danger EdgeButton--small\" data-toggle=\"modal\" data-target=\"#deleteModal\" data-whatever=");
            EndContext();
            BeginContext(1983, 7, false);
#line 38 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                                                                                                                                                          Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1990, 45, true);
            WriteLiteral("><i class=\"fa fa-trash\"></i>Delete</button>\r\n");
            EndContext();
#line 39 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"
                    }

#line default
#line hidden
            BeginContext(2058, 60, true);
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 43 "C:\Users\sbhatia\Desktop\desktop items\1.FSE\Assignment 20-Twitter\TwitterClone.Core\Views\UserTweet\_PostedTweets.cshtml"

    }

#line default
#line hidden
            BeginContext(2127, 34, true);
            WriteLiteral("</div>\r\n\r\n<script>\r\n   \r\n</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TwitterClone.Core.Models.ViewModels.TweetViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591