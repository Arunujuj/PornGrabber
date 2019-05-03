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

        private string GetHTML(string url)
        {
            this.url = url;

            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }
        }

        public string[] getCategories(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(GetHTML(url));

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
            doc.LoadHtml(GetHTML(url + "/" + categorie));
            var nodes = doc.GetElementbyId("container");
            List<HtmlNode> pureImages = nodes.ChildNodes.Where(x => x.HasClass("giselle")).ToList();

            string[] images = new string[pureImages.Count()];
            int indx = 0;
            foreach(var imageContainer in pureImages)
            {
                foreach(var actualImage in imageContainer.ChildNodes)
                {
                    images[indx] = GetSRC(actualImage.InnerHtml);
                }
                
                indx++;
            }

            return images[random.Next(0, images.Count())];
        }

        private string GetSRC(string innerHtml)
        {
            if(innerHtml != "")
            {
                string result = "";

                int startIndex = innerHtml.IndexOf("src='") + 5;
                result = innerHtml.Remove(0, startIndex);
                int endIndex = result.IndexOf("' ");
                result = result.Remove(endIndex, result.Length - endIndex);


                return result;
            }
            return "";
        }
    }
}
