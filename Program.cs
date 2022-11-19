using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCardBilling
{
    

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(findLowestPrice(new List<List<string>> { new List<string> { "10", "d0", "d1" }, new List<string> { "15", "EMPTY", "EMPTY" }, new List<string> { "20", "d1", "EMPTY" } },
            //                                  new List<List<string>> { new List<string> { "d0", "1", "27" }, new List<string> { "d1", "2", "5" } }));
             Console.WriteLine(findLowestPrice(new List<List<string>> { new List<string> { "10", "sale", "january-sale" }, new List<string> { "200", "sale", "EMPTY" } },
                                         new List<List<string>> { new List<string> { "sale", "0", "10" }, new List<string> { "january-sale", "1", "10" } }));
            List<int> inventory1 = new List<int>();
            inventory1.Add(2);
            inventory1.Add(8);
            inventory1.Add(4);
            inventory1.Add(10);
            inventory1.Add(6);
           // long data = maximumProfit(inventory1, 20L);
           // Console.WriteLine(data); //expected:19

            Console.WriteLine(maximumProfit(inventory1, 20));

        }

        private static bool MaxProfitWithKTransactionqs(int[] prices, int v)
        {
            throw new NotImplementedException();
        }

        public static long maximumProfit(List<int> inventory, long order)
        {
            // inventory.Sort(new ComparatorAnonymousInnerClass());
            // inventory.Sort();
            long maximumProfit = 0;
            Console.WriteLine(inventory);

            while (order != 0)
            {

                int numberOfMaximums = 1;
                int difference = 0;
                for (int i = 1; i < inventory.Count; i++)
                {
                    if (inventory[i].Equals(inventory[i - 1]))
                    {
                        numberOfMaximums++;
                    }
                    else
                    {
                        difference = inventory[i - 1] - inventory[i];
                        break;
                    }
                }

                if (numberOfMaximums == inventory.Count)
                {
                    difference = inventory[0];
                }

                if (order < numberOfMaximums * difference)
                {
                    for (int j = 0; j < order / numberOfMaximums; j++)
                    {
                        maximumProfit += numberOfMaximums * (inventory[0] - j);
                    }
                    for (int k = 0; k < numberOfMaximums; k++)
                    {
                        inventory[k] = inventory[k] - ((int)(order / numberOfMaximums));
                    }
                    maximumProfit += order % numberOfMaximums * inventory[0];
                    order = 0;
                }
                else
                {
                    for (int j = 0; j < difference; j++)
                    {
                        maximumProfit += numberOfMaximums * (inventory[0] - j);
                    }
                    for (int k = 0; k < numberOfMaximums; k++)
                    {
                        inventory[k] = inventory[k] - difference;
                    }
                    order -= numberOfMaximums * difference;
                }
            }
            return maximumProfit;
        }



        public static int findLowestPrice(List<List<string>> products, List<List<string>> discounts)
        {

            int[] minCosts = new int[products.Count];

            IDictionary<string, IList<int>> discountMap = createDiscountMap(products, discounts);

            for (int i = 0; i < products.Count; i++)
            {
                IList<string> oneProduct = products[i];
                minCosts[i] = findMinCost(oneProduct, discountMap);
            }

            Console.WriteLine("[" + string.Join(", ", minCosts) + "]");

            return minCosts.Sum();
        }



        private static IDictionary<string, IList<int>> createDiscountMap(List<List<string>> products, List<List<string>> discounts)
        {
            // key: product, value: price
            IDictionary<string, IList<int>> discountMap = new Dictionary<string, IList<int>>();

            for (int i = 0; i < discounts.Count; i++)
            {
                IList<string> d = discounts[i];
                string key = d[0];
                discountMap[key] = new List<int>();
                for (int j = 1; j < d.Count; j++)
                {
                    discountMap[key].Add(int.Parse(d[j]));
                }
            }

            return discountMap;
        }
        private static int findMinCost(IList<string> eachProduct, IDictionary<string, IList<int>> discountMap)
        {
            int originPrice = int.Parse(eachProduct[0]);
            int min = originPrice;

            // - Type 0: discounted price, the item is sold for a given price.
            // - Type 1: percentage discount, the customer is given a fixed percentage discount from the retail price.
            // - Type 2: fixed discount, the customer is given a fixed amount off from the retail price.
            int discountType;
            int amount;


            for (int i = 1; i < eachProduct.Count; i++)
            {
                string tag = eachProduct[i];

                if (tag.Equals("EMPTY"))
                {
                    continue;
                }

                discountType = discountMap[tag][0];
                amount = discountMap[tag][1];

                switch (discountType)
                {
                    case 0:
                        min = Math.Min(min, amount);
                        break;
                    case 1:
                        min = Math.Min(min, (int)Math.Round(originPrice * ((float)(100 - amount) / 100), MidpointRounding.AwayFromZero));
                        break;
                    case 2:
                        min = Math.Min(min, originPrice - amount);
                        break;
                }
            }

            return min;
        }
    }
}
