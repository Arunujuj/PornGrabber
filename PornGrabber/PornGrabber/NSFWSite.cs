using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PornGrabber
{
    public class NSFWSite : IWebsite
    {
        string url = "http://www.hdpornpictures.com/";
        Random random = new Random();

        public string[] getCategories(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Tools.GetHTML(url));

            var nodes = doc.GetElementbyId("container");

            var cateNodes = nodes.ChildNodes.Where(x => x.HasClass("giselle"));

            string[] categories = new string[cateNodes.Count()];
            int indx = 0;
            foreach (var node in cateNodes)
            {
                foreach(var item in node.ChildNodes)
                {
                    string categorie = item.GetAttributeValue("href", "");
                    if(categorie != "")
                    {
                        categories[indx] = categorie.Replace("/", string.Empty);
                        indx++;
                    }
                }
                
            }
            return categories;
        }

        public string getRandomCategorie(string[] categories)
        {
            int categorieIndex = random.Next(0, categories.Count());
            string categorieurl = categories[categorieIndex];
            return categorieurl;
        }

        public string getRandomImage(string categorie)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Tools.GetHTML(url + "/" + categorie));
            var nodes = doc.GetElementbyId("container");
            List<HtmlNode> pureImages = nodes.ChildNodes.Where(x => x.HasClass("giselle")).ToList();

            string[] images = new string[pureImages.Count()];
            int indx = 0;
            foreach(var imageContainer in pureImages)
            {
                foreach(var actualImage in imageContainer.ChildNodes)
                {
                    images[indx] = Tools.GetSRCTag(actualImage.InnerHtml);
                }
                
                indx++;
            }

            return images[random.Next(0, images.Count())];
        }

        
    }
}
