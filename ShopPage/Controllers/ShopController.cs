using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.Controllers
{
    public class ShopController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //List<Item> items;
        static List<Item> selectedItems;
        static int startInd, itemCountReturn;

        // GET: Shop
        public ActionResult Index()
        {
            //context = new ApplicationDbContext();
            //items = context.Items.ToList();
            selectedItems = context.Items.ToList();
            if (selectedItems.Count > 0)
            {
                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                    //selectedItems = items.GetRange(startInd, endInd);
                    //return View(selectedItems.GetRange(startInd, itemCount));
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                    //selectedItems = items.GetRange(startInd, endInd);
                    //return View(selectedItems.GetRange(startInd, itemCount));

                    //int rownum = 1;
                    //rownum = n / thN;
                    //int rem = n % thN;
                    //Thread[] th = new Thread[thN];
                    //for (int i = 0; i < thN; i++)
                    //{
                    //    int start = i * rownum + (i < rem ? i : rem);
                    //    int end = start + rownum + (i < rem ? 1 : 0);
                    //    th[i] = new Thread(() => { matMul(start, end); });
                    //    th[i].Start();
                    //}
                }

                return View(selectedItems.GetRange(startInd, itemCountReturn));
            }
            else
                return PartialView("ItemsPartialView", null);
        }

        public ActionResult LoadMore()
        {
            int rem = selectedItems.Count - (startInd + itemCountReturn);
            if (rem > 0)
            {
                startInd += itemCountReturn;
                if (rem > 1)
                {
                    itemCountReturn = 1;
                    //selectedItems = items.GetRange(startInd, endInd);
                }
                else
                {
                    itemCountReturn = rem;
                    //selectedItems = items.GetRange(startInd, endInd);
                }
                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }

            ViewBag.Message = "There Are No Items More to be Loaded!";
            return PartialView("ItemsPartialView", null);
        }

        public ActionResult GetByCategory(string categoryName)
        {
            int count = context.Items.Where(i => i.Product.Category.Name.ToLower() == categoryName.ToLower()).ToList().Count;
            
            if(count > 0)
            {
                selectedItems = context.Items.Where(i => i.Product.Category.Name.ToLower() == categoryName.ToLower()).ToList();
                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                    //selectedItems = items.GetRange(startInd, endInd);
                    //return PartialView("ItemsPartialView", selectedItems);
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                    //selectedItems = items.GetRange(startInd, endInd);
                    //return PartialView("ItemsPartialView", selectedItems);
                }

                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }
            else
            {
                //ViewBag.Message = $"There Are No Items Related to '{categoryName}' Category to be Loaded!";
                return PartialView("ItemsPartialView", null);
            }
        }

        public ActionResult GetByBrand(string brandNames)
        {
            if(brandNames != "")
            {
                var arrNames = brandNames.Split(',');

                int count = 0;

                foreach (var item in arrNames)
                {
                    count += context.Items.Where(i => i.Brand.ToLower() == item.ToLower()).ToList().Count;
                }

                if (count > 0)
                {
                    selectedItems.Clear();

                    foreach (var item in arrNames)
                    {
                        selectedItems.AddRange(context.Items.Where(i => i.Brand.ToLower() == item.ToLower()).ToList());
                    }

                    startInd = 0;
                    if (selectedItems.Count > 2)
                    {
                        itemCountReturn = 2;
                    }
                    else
                    {
                        itemCountReturn = selectedItems.Count;
                    }

                    return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
                }
                else
                {
                    //string msg = "";
                    //foreach (var item in arrNames)
                    //{
                    //    msg += $"{item}, ";
                    //}
                    //ViewBag.Message = $"There Are No Items Related to '{msg}'Brand(s) to be Loaded!\nPlease, Remove the select to see Result!";
                    return PartialView("ItemsPartialView", null);
                }
            }
            else
            {
                selectedItems = context.Items.ToList();

                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                }

                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }
        }

        public ActionResult GetByPrice(string priceRange)
        {
            double priceValue = double.Parse(priceRange);

            int count = context.Items.Where(i => i.Price <= priceValue).ToList().Count;
            
            if (count > 0)
            {
                selectedItems = context.Items.Where(i => i.Price <= priceValue).ToList();
                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                }

                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }
            else
            {
                //ViewBag.Message = $"There Are No Items in Range 10 $ and {priceRange} $ to be Loaded!";
                return PartialView("ItemsPartialView", null);
            }
        }

        public ActionResult GetByColor(string color)
        {
            int count = context.Items.Where(i => i.Color.ToLower() == color.ToLower()).ToList().Count;

            if (count > 0)
            {
                selectedItems = context.Items.Where(i => i.Color.ToLower() == color.ToLower()).ToList();
                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                }

                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }
            else
            {
                //ViewBag.Message = $"There Are No Items Related to '{color}' Color to be Loaded!\nPlease, Choose another Color or Anything else.";
                return PartialView("ItemsPartialView", null);
            }
        }

        public ActionResult GetBySize(string itemSize)
        {
            int size = 0;
            switch (itemSize)
            {
                case "s":
                    size = 40;
                    break;
                case "m":
                    size = 41;
                    break;
                case "l":
                    size = 42;
                    break;
                case "xs":
                    size = 39;
                    break;
            }

            int count = context.Items.Where(i => i.Size == size).ToList().Count;

            if (count > 0)
            {
                selectedItems = context.Items.Where(i => i.Size == size).ToList();
                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                }

                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }
            else
            {
                //ViewBag.Message = $"There Are No Items Related to '{color}' Color to be Loaded!\nPlease, Choose another Color or Anything else.";
                return PartialView("ItemsPartialView", null);
            }
        }

        public ActionResult GetByTagName(string tagName)
        {
            int count = context.Items.Where(i => i.Product.Name.ToLower() == tagName.ToLower()).ToList().Count;

            if (count > 0)
            {
                selectedItems = context.Items.Where(i => i.Product.Name.ToLower() == tagName.ToLower()).ToList();
                startInd = 0;
                if (selectedItems.Count > 2)
                {
                    itemCountReturn = 2;
                }
                else
                {
                    itemCountReturn = selectedItems.Count;
                }

                return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCountReturn));
            }
            else
            {
                //ViewBag.Message = $"There Are No Items Related to '{color}' Color to be Loaded!\nPlease, Choose another Color or Anything else.";
                return PartialView("ItemsPartialView", null);
            }
        }

        public ActionResult GetItemDetails(int id)
        {
            Item itemResult = context.Items.Where(i => i.ID == id).FirstOrDefault();

            return PartialView("ItemDetailsPartialView", itemResult);
        }

        //public ActionResult GetDefault()
        //{
        //    selectedItems = context.Items.ToList();
        //    startInd = 0;
        //    if (selectedItems.Count > 2)
        //    {
        //        itemCount = 2;
        //    }
        //    else
        //    {
        //        itemCount = selectedItems.Count;
        //    }

        //    return PartialView("ItemsPartialView", selectedItems.GetRange(startInd, itemCount));
        //}
    }
}