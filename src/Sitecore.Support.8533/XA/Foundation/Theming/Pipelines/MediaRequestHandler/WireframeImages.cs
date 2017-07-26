namespace Sitecore.Support.XA.Foundation.Theming.Pipelines.MediaRequestHandler
{
    using System;
    using System.Drawing;
    using System.Xml.Linq;

    public class WireframeImages : Sitecore.XA.Foundation.Theming.Pipelines.MediaRequestHandler.WireframeImages
    {
        protected override Image GetWireframeImage(Sitecore.Resources.Media.Media media)
        {
            #region Added code
            if (media.Extension.Equals("svg", StringComparison.OrdinalIgnoreCase)) // try to get dimensions from SVG file contents
            {
                int height = 100; // fallback values
                int width = 100;
                XElement root = XDocument.Load(media.GetStream().Stream).Root;
                if (root != null && root.Attribute("height") != null && root.Attribute("width") != null)
                {
                    if (!root.Attribute("height").Value.Contains("%") && !root.Attribute("width").Value.Contains("%"))
                    {
                        Int32.TryParse(root.Attribute("height").Value.Replace("px", "").Split('.')[0], out height);
                        Int32.TryParse(root.Attribute("width").Value.Replace("px", "").Split('.')[0], out width);
                    }
                }
                return this.GetWireframeImageInternal(width, height);
            } 
            #endregion
            Image image = Image.FromStream(media.GetStream().Stream);
            return this.GetWireframeImageInternal(image.Width, image.Height);
        }
    }
}