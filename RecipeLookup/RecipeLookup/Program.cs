using System;
using System.Collections.Generic;
using System.Text;
class Recipe
{
    public string Name { get; set; }    
    public double Speed { get; set; }
    public double Temperature { get; set; }
    public double Pressure { get; set; }

}

class RecipeLookUp
{
    private static Dictionary<string, Recipe> keyRecipe = new Dictionary<string, Recipe>();

    public static void AddRecipe(Recipe recipe)
    {
        if (keyRecipe.TryGetValue(recipe.Name, out Recipe input))
        {
            input.Name = recipe.Name;
            input.Speed = recipe.Speed;
            input.Temperature = recipe.Temperature;
            input.Pressure = recipe.Pressure;   

            Console.WriteLine($"Update Name={input.Name} - Speed={input.Speed} - Temperature={input.Temperature} - Pressure={input.Pressure}");

        }
        else
        {
            //Recipe product = new Recipe();
            keyRecipe.Add(recipe.Name, recipe);
            Console.WriteLine($"Added Name={recipe.Name} - Speed={recipe.Speed} - Temperature={recipe.Temperature} - Pressure={recipe.Pressure}");

        }    
    }

    public static void FindRecipe(string name)
    {
        if(keyRecipe.ContainsKey(name))
        {
            Recipe recipe = keyRecipe[name];
            Console.WriteLine($"Name={recipe.Name}, Speed={recipe.Speed}, Temperature={recipe.Temperature}, Pressure={recipe.Pressure}");

        } 
        else
        {
            Console.WriteLine($"Không tìm thấy {name}");
        }    
    }

    public static void ShowRecipe()
    {
        if (keyRecipe.Count != 0)
        {
            foreach (var item in keyRecipe)
            {
                Console.WriteLine($"Name={item.Key}, Speed={item.Value.Speed}, Temperature={item.Value.Temperature}, Pressure={item.Value.Pressure}");

            }
        }
        else
        {
            Console.WriteLine($"Danh sách Recipe rỗng!");
        }
    }

    public static void DeleteRecipe(string name)
    {
        if (keyRecipe.ContainsKey(name))
        {
            Recipe recipe = keyRecipe[name];
            keyRecipe.Remove(name);
            Console.WriteLine($"Removed: Name={recipe.Name} - Spedd={recipe.Speed} - Temperature={recipe.Temperature} - Pressure={recipe.Pressure}");

        } 
        else
        {
            Console.WriteLine($"Không tìm thấy Recipe: {name}");
        }    
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        if (!int.TryParse(Console.ReadLine(), out int N) || N < 1) return;

        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0) return;

        }
        for (int i = 0; i < N; i++)
        {
            switch (arr[i][0])
            {
                case "ADD":
                    {
                        if (arr[i].Length == 5)
                        {
                            string name = arr[i][1];
                            if (!double.TryParse(arr[i][2], out double speed)) return;
                            if (!double.TryParse(arr[i][3], out double temperature)) return;
                            if (!double.TryParse(arr[i][4], out double pressure)) return;
                            Recipe recipe = new Recipe();
                            recipe.Name = name;
                            recipe.Speed = speed;
                            recipe.Temperature = temperature;
                            recipe.Pressure = pressure;
                            RecipeLookUp.AddRecipe(recipe);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Lệnh {arr[i][0]} không hợp lệ!");
                            break;
                        }
                    }
                case "FIND":
                    {
                        if (arr[i].Length == 2)
                        {
                            RecipeLookUp.FindRecipe(arr[i][1]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Lệnh {arr[i][0]} không hợp lệ!");
                            break;
                        }
                    }
                case "SHOW":
                    {
                        if (arr[i].Length == 1)
                        {
                            RecipeLookUp.ShowRecipe();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Lệnh {arr[i][0]} không hợp lệ!");
                            break;
                        }
                    }
                case "DELETE":
                    {
                        if (arr[i].Length == 2)
                        {
                            RecipeLookUp.DeleteRecipe(arr[i][1]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Lệnh {arr[i][0]} không hợp lệ!");
                            break;
                        }
                    }
                default:
                    {
                        Console.WriteLine("Lệnh không hợp lệ!");
                        break;
                    }

            }
        }
            
    }
}