using KomplexeEingaben.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;    // CultureInfo

namespace KomplexeEingaben.Controllers
{
    public class HomeController : Controller
    {
        //private KomplexIn _kI = new() { Name = "Anna"};

        public IActionResult Index()
        {
            // Exkurs
            var de = "123,456";
            var en = "345.689";

            var deTrue = decimal.TryParse(de, out decimal deP);
            var enTrue = decimal.TryParse(en, CultureInfo.InvariantCulture, out decimal enP);
            Console.WriteLine($"{deTrue}: {deP}  -  {enP} :{enTrue}");

            Console.WriteLine("Index GET");
            var komplexIn = new KomplexIn();
            komplexIn.Name = "Max Musterling";
            komplexIn.Password = "admin";
            komplexIn.Number = 456.78m;
            komplexIn.IsSubscribed = true;
            komplexIn.IsSubscribedInt = 1;
            komplexIn.CategoryId = 1;
            komplexIn.BirthDate = DateTime.Now;
            komplexIn.SelectedIds = [1, 2, 3];

            Console.WriteLine("Index POST   " + komplexIn.Name.ToUpper());
            Console.WriteLine("\tIsSubscribed: " + komplexIn.IsSubscribed);
            Console.WriteLine("\tIsSubscribedInt: " + komplexIn.IsSubscribedInt);
            foreach (var item in komplexIn.SelectedIds) Console.Write($"{item}, ");
            Console.WriteLine();
            //_kI = komplexIn;

            //Console.WriteLine("######### " + _kI.Name);
            return View(komplexIn);
        }

        [HttpPost]
        public IActionResult Index(KomplexIn komplexInNew)
        {
            Console.WriteLine("Index POST   " + komplexInNew.Name.ToUpper());
            Console.WriteLine("\tIsSubscribed: " +komplexInNew.IsSubscribed);
            Console.WriteLine("\tIsSubscribedInt: " + komplexInNew.IsSubscribedInt);
            
            if (komplexInNew?.SelectedIds != null)
                foreach (var item in komplexInNew?.SelectedIds) Console.WriteLine($"{item}, ");
            else komplexInNew.SelectedIds = [];

            komplexInNew.ImageInput = komplexInNew.FileInput is null ? @"/img/DSC_9129_sm.png" : @"/img/" + komplexInNew.FileInput;

            komplexInNew.Name = komplexInNew.Name.ToUpper();

            //_kI = komplexInNew;
            return View(komplexInNew);
        }



        public IActionResult TagHelper()
        {
            //Console.WriteLine("Index GET");
            var komplexIn = new KomplexIn();
            komplexIn.Name = "Max Musterling";
            komplexIn.Password = "admin";
            komplexIn.Number = 456.78m;
            komplexIn.IsSubscribed = true;
            komplexIn.IsSubscribedInt = 1;
            komplexIn.CategoryId = 1;
            komplexIn.BirthDate = DateTime.Now;
            komplexIn.SelectedIds = [1, 2, 3];
            komplexIn.CheckboxField.Add(new MultiCheck() {Wert=1, IsChecked=true, Name="Option 1" });
            komplexIn.CheckboxField.Add(new MultiCheck() {Wert=2, IsChecked=false, Name="Option 2" });
            komplexIn.CheckboxField.Add(new MultiCheck() {Wert=3, IsChecked=true, Name="Option 3" });

            //Console.WriteLine("Index POST   " + komplexIn.Name.ToUpper());
            //Console.WriteLine("\tIsSubscribed: " + komplexIn.IsSubscribed);
            //Console.WriteLine("\tIsSubscribedInt: " + komplexIn.IsSubscribedInt);
            //foreach (var item in komplexIn.SelectedIds) Console.Write($"{item}, ");
            //Console.WriteLine();
            //Console.WriteLine("+++++++ " + _kI.Name);
            //_kI = komplexIn;
            return View("TagHelper", komplexIn);
        }

        [HttpPost]
        public IActionResult TagHelper(KomplexIn komplexInNew)
        {
            Console.WriteLine("Index POST   " + komplexInNew.Name.ToUpper());
            Console.WriteLine("\tIsSubscribed: " + komplexInNew.IsSubscribed);
            Console.WriteLine("\tIsSubscribedInt: " + komplexInNew.IsSubscribedInt);

            if (komplexInNew?.SelectedIds != null)
                foreach (var item in komplexInNew?.SelectedIds) Console.WriteLine($"{item}, ");
            else komplexInNew.SelectedIds = [];

            Console.WriteLine("CheckboxField Count: " + komplexInNew.CheckboxField.Count);

            komplexInNew.CheckboxField.Add(new MultiCheck() { Wert = 1, IsChecked = true, Name = "Option 1" });
            komplexInNew.CheckboxField.Add(new MultiCheck() { Wert = 2, IsChecked = false, Name = "Option 2" });
            komplexInNew.CheckboxField.Add(new MultiCheck() { Wert = 3, IsChecked = true, Name = "Option 3" });

            foreach (var item in komplexInNew.CheckboxField)
            {
                if (komplexInNew.SelectedIds.Contains(item.Wert)) item.IsChecked = true;
                else item.IsChecked = false;
            }

            komplexInNew.ImageInput = komplexInNew.FileInput is null ? @"/img/DSC_9129_sm.png" : @"/img/" + komplexInNew.FileInput;

            komplexInNew.Name = komplexInNew.Name.ToLower();

            //_kI = komplexInNew;
            return View("TagHelper", komplexInNew);
        }
    }
}
