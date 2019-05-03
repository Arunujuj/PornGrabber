namespace PornGrabber
{
    interface IWebsite
    {
        string[] getCategories(string url);

        string getRandomCategorie(string[] categories);

        string getRandomImage(string categorie);


    }
}
