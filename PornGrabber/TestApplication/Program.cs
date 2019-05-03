using PornGrabber;
using System;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            NSFWSite grabber = new NSFWSite();

            var test1 = grabber.getCategories("http://www.hdpornpictures.com/");
            var test2 = grabber.getRandomCategorie(test1);
            var test3 = grabber.getRandomImage(test2);
            Console.WriteLine(test3);
            Console.ReadLine();
        }
    }
}
