using Barcoding.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZXing.QrCode.Internal;
using Barcoding.Services;

namespace Barcoding.Services
{
    public static class CompositionService
    {

        static SQLiteAsyncConnection dbComposition;

        public static async Task Init()
        {
            if (dbComposition != null)
                return;


            // Разные способы считывания базы данных из рахных источников

            //var databasePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.Resources.Raw.{"sampleDB.db"}";

            //var databasePath = System.IO.Path.Combine(appDataDirectory, "DBases", "sampleDB.db");

            //var databasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBases", "sampleDB.db");

            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "sampleDB.db");

            //var databasePath = Path.Combine(Android.App.Application.Context.FilesDir.AbsolutePath, "DBases", "sampleDB.db");

            // C:\Users\Admin\source\repos\Barcoding\Barcoding\sampleDB.db

            //var dataPath = @"C:\Users\Admin\source\repos\Barcoding\Barcoding\sampleDB.db";

            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "yourExistingDatabase.db");

            //var appDataDirectory = FileSystem.AppDataDirectory;


            var dbName = "ProductDataBase.db"; // Имя вашего файла базы данных

            var assembly = Assembly.GetExecutingAssembly();
            // Путь к встроенному ресурсу
            var resourcePath = $"{assembly.GetName().Name}.Resources.Raw.{dbName}";

            // Создание временного файла для работы с SQLite
            var tempFilePath = Path.Combine(FileSystem.AppDataDirectory, dbName);


            
            
            if (!File.Exists(tempFilePath))
            {

                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream == null)
                        throw new FileNotFoundException("Database file not found.");



                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                    }


                    // Создание соединения с базой данных
                    //dbComposition = new SQLiteAsyncConnection(tempFilePath);

                    //await dbComposition.CreateTableAsync<Compositions>();

                }
            }
            
            
            dbComposition = new SQLiteAsyncConnection(tempFilePath);
            //dbComposition = new SQLiteAsyncConnection(resourcePath);
            //await dbComposition.CreateTableAsync<Compositions>();

            //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "sampleDB.db");

            //dbComposition = new SQLiteAsyncConnection(databasePath);

            //await dbComposition.CreateTableAsync<Compositions>();

            // если база данных уже есть, то данный метод проверит это и не будет создавать новую

        }

        /*
        public static async Task AddProduct(string Fabricator, string Name)
        {
            await Init();
            var Product = new Product()
            {
                Fabricator = Fabricator,
                Name = Name
            };

            await db.InsertAsync(Product);


        }
        */

        public static async Task<Compositions> GetProduct( string Barcode )
           {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();

            var result = await dbComposition.Table<Compositions>().FirstOrDefaultAsync(c => c.Barcode == Barcode);
      
            return result;

            //var query = db.Table<Product>();
            //var result = await query.ToListAsync();

        }



        public static async Task<string> GetProductName(string barcode)
        {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();

            var result = await dbComposition.Table<Compositions>().FirstOrDefaultAsync(c => c.Barcode == barcode);


            return result.Title;

            //var query = db.Table<Product>();
            //var result = await query.ToListAsync();

        }

        public static async Task<string> GetProductDescription(string name)
        {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();

            var result = await dbComposition.Table<Ingridients>().FirstOrDefaultAsync(c => c.Name == name);

            
            return result.Description;
            

            //var query = db.Table<Product>();
            //var result = await query.ToListAsync();

        }

        public static async Task<bool> GetProductColor(string name)
        {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();

            var result = await dbComposition.Table<Ingridients>().FirstOrDefaultAsync(c => c.Name == name);
            return result.Color == "Red";

        }


        public static async Task<Ingridients> GetIngridient(string ingridient)
        {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();
            //var Ingridient = await dbComposition.Table<Ingridients>().FirstOrDefaultAsync(c => c.Name == ingridient);

            var existingIngridient = await dbComposition.Table<Ingridients>().FirstOrDefaultAsync(c => c.Name == ingridient);

            if (existingIngridient == null)
            {
                existingIngridient = new Ingridients
                {
                    Name = ingridient,
                    Description = "0",
                    Color = "Black"
                };
            }

            return existingIngridient;

            //var query = db.Table<Product>();
            //var result = await query.ToListAsync();

        }

        public static async Task<Rating> GetRate(string Barcode)
        {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();

            var Rt = await dbComposition.Table<Rating>().FirstOrDefaultAsync(c => c.Barcode == Barcode);


            return Rt;

            //var query = db.Table<Product>();
            //var result = await query.ToListAsync();

        }

        public static async Task<List<Rating>> TopThree( string category )
        {
            await Init();

            // Используем SQL-запрос для оптимизации

            // Категория передаётся параметром (?), а не вклеивается в строку — защита от SQL-инъекции
            var query = "SELECT * FROM Rating WHERE ProductRating >= 4 AND Category = ? ORDER BY RANDOM() LIMIT 3";


            var result = await dbComposition.QueryAsync<Rating>(query, category);

            return result ?? new List<Rating>();
        }

        public static async Task<IEnumerable<News>> GetNews()
        {
            await Init();

            //var result = await dbComposition.Table<Compositions>().ToListAsync();

            var Rt1 = await dbComposition.Table<News>().ToListAsync();

            int MaxId = Rt1.Count - 10 ;

            var Rt2 = Rt1.Where(x => x.Id >= MaxId);

            return Rt2;

            //var query = db.Table<Product>();
            //var result = await query.ToListAsync();

        }

        public static async Task AddNews(News newsItem)
        {
            await Init();
            // Просто добавляем новость в таблицу
            await dbComposition.InsertAsync(newsItem);
        }

        public static async Task<bool> DeleteNewsById(int id)
        {
            await Init();

            // Находим новость по ID
            var newsToDelete = await dbComposition.Table<News>()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (newsToDelete == null)
                return false; // Новость не найдена

            // Удаляем новость
            var result = await dbComposition.DeleteAsync(newsToDelete);

            // result содержит количество удаленных строк (1 или 0)
            return result == 1;
        }

        //  Написать команду для получения Рейтинговых товаров 

    }
}
