namespace Elmah
{
    #region Imports

    using System;
    using Microsoft.Security.Application;
    using LibOwin;

    #endregion

    class WebTemplateBase : RazorTemplateBase
    {
        public IOwinContext Context { get; set; }
        public IOwinResponse Response { get { return Context.Response; } }
        public IOwinRequest Request { get { return Context.Request; } }

        public IHtmlString Html(string html)
        {
            return new HtmlString(html);
        }

        public string AttributeEncode(string text)
        {
            return string.IsNullOrEmpty(text)
                 ? string.Empty
                 : Encoder.HtmlAttributeEncode(text);
        }

        public string Encode(string text)
        {
            return string.IsNullOrEmpty(text) 
                 ? string.Empty 
                 : Elmah.Html.Encode(text).ToHtmlString();
        }

        public override void Write(object value)
        {
            if (value == null)
                return;
            base.Write(Elmah.Html.Encode(value).ToHtmlString());
        }

        public override object RenderBody()
        {
            return new HtmlString(base.RenderBody().ToString());
        }

        public override string TransformText()
        {
            if (Context == null)
                throw new InvalidOperationException("The Context property has not been initialzed with an instance.");
            return base.TransformText();
        }
    }
}