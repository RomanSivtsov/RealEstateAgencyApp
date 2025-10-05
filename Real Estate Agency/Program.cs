using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Real_Estate_Agency.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EFCore_RealEstateAgency
{
    public class Program
    {
        static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine(sqltext);
            Console.WriteLine("Записи: ");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static void Select(RealEstateAgencyContext db)
        {
            // LINQ Query 1: Выборка сделок для объектов типа 'квартира' за 2025 год из представления ViewAllDeals
            var queryLINQ1 = from d in db.ViewAllDeals
                             where d.PropertyType == "квартира" && d.DealDate.Year == 2025
                             orderby d.DealId descending
                             select new
                             {
                                 Код_сделки = d.DealId,
                                 Адрес_объекта = d.PropertyAddress,
                                 Сумма_сделки = d.DealAmount,
                                 Месяц = d.DealDate.Month
                             };

            string comment = "1. Результат выполнения запроса на выборку отсортированных записей из" +
                " представления View_AllDeals (тип 'квартира' и год 2025): \r\n";
            Print(comment, queryLINQ1.Take(5).ToList());

            // LINQ Query 2: Группировка сделок по агентам с подсчетом общей суммы из представления ViewAgentSalesReport
            var queryLINQ2 = from r in db.ViewAgentSalesReports
                             select new
                             {
                                 Код_агента = r.AgentId,
                                 Имя_агента = r.AgentName,
                                 Общая_сумма_сделок = r.TotalSalesAmount,
                                 Средняя_сумма_сделки = r.AverageDealAmount
                             };

            comment = "2. Результат выполнения запроса на выборку сгруппированных записей из" +
                " представления View_AgentSalesReport с общей и средней суммой сделок: \r\n";
            Print(comment, queryLINQ2.Take(5).ToList());

            // LINQ Query 3: Выборка объектов недвижимости с определенными полями
            var queryLINQ3 = from p in db.Properties
                             orderby p.PropertyId descending
                             select new
                             {
                                 Адрес_объекта = p.PropertyAddress,
                                 Тип_объекта = p.PropertyType,
                                 Площадь = p.PropertyArea,
                                 Количество_комнат = p.PropertyRooms,
                                 Цена = p.PropertyPrice
                             };

            comment = "3. Результат выполнения запроса на выборку записей из таблицы Properties с выводом определенных полей: \r\n";
            Print(comment, queryLINQ3.Take(5).ToList());

            // LINQ Query 4: Выборка среднего времени продажи из представления View_AverageSaleTime
            var queryLINQ4 = from v in db.ViewAverageSaleTimes
                             select new
                             {
                                 Среднее_время_продажи_дней = v.AverageDaysToSale,
                                 Всего_проданных_объектов = v.TotalSoldProperties
                             };

            comment = "4. Результат выполнения запроса на выборку среднего времени продажи из представления View_AverageSaleTime: \r\n";
            Print(comment, queryLINQ4.ToList());
        }

        static void Insert(RealEstateAgencyContext db)
        {
            // Создать нового владельца
            Owner owner = new Owner
            {
                OwnerName = "Иванов Иван Иванович",
                OwnerContacts = "+79991234567, ivanov@example.com"
            };

            // Создать нового агента
            Agent agent = new Agent
            {
                AgentName = "Петров Петр Петрович",
                AgentContacts = "+79997654321, petrov@example.com",
                AgentRequirements = "Опытный агент"
            };

            // Создать нового клиента
            Client client = new Client
            {
                ClientName = "Сидоров Алексей Алексеевич",
                ClientContacts = "+79998765432, sidorov@example.com",
                ClientRequirements = "Ищет квартиру в центре"
            };

            // Добавить в DbSet и сохранить, чтобы получить ID
            db.Owners.Add(owner);
            db.Agents.Add(agent);
            db.Clients.Add(client);
            db.SaveChanges();

            // Создать новый объект недвижимости с использованием хранимой процедуры
            var property = new Property
            {
                PropertyAddress = "ул. Ленина, д. 10",
                PropertyType = "квартира",
                PropertyArea = 65.5f,
                PropertyRooms = 2,
                PropertyPrice = 5500000.00m,
                PropertyDescription = "Уютная квартира в центре",
                PropertyPhoto = "http://photo100.jpg",
                PropertyStatus = "в продаже",
                OwnerId = owner.OwnerId,
                PropertyCreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            // Добавить объект недвижимости
            db.Properties.Add(property);
            db.SaveChanges();

            // Проверка на существование связанных записей перед созданием сделки
            if (!db.Properties.Any(p => p.PropertyId == property.PropertyId && p.OwnerId == owner.OwnerId))
            {
                Console.WriteLine("Ошибка: Объект недвижимости не принадлежит указанному владельцу.");
                return;
            }
            if (!db.Clients.Any(c => c.ClientId == client.ClientId))
            {
                Console.WriteLine("Ошибка: Клиент не существует.");
                return;
            }
            if (!db.Agents.Any(a => a.AgentId == agent.AgentId))
            {
                Console.WriteLine("Ошибка: Агент не существует.");
                return;
            }

            // Создать новую сделку
            Deal deal = new Deal
            {
                PropertyId = property.PropertyId,
                OwnerId = owner.OwnerId,
                ClientId = client.ClientId,
                AgentId = agent.AgentId,
                DealDate = DateOnly.FromDateTime(DateTime.Now),
                DealAmount = 5600000.00m
            };

            // Добавить в DbSet
            db.Deals.Add(deal);
            db.SaveChanges();

            // Обновление требований клиента с использованием хранимой процедуры
            db.Database.ExecuteSqlRaw("EXEC dbo.uspUpdateClientRequirements @ClientID, @NewRequirements",
                new Microsoft.Data.SqlClient.SqlParameter("@ClientID", client.ClientId),
                new Microsoft.Data.SqlClient.SqlParameter("@NewRequirements", "Ищет квартиру до 6 млн"));
        }

        static void Update(RealEstateAgencyContext db)
        {
            // Обновление объекта недвижимости с использованием хранимой процедуры
            string propertyAddress = "ул. Ленина, д. 10";
            var property = db.Properties
                .FirstOrDefault(p => p.PropertyAddress == propertyAddress);
            if (property != null)
            {
                db.Database.ExecuteSqlRaw("EXEC dbo.uspUpdatePropertyStatus @PropertyID, @NewStatus",
                    new Microsoft.Data.SqlClient.SqlParameter("@PropertyID", property.PropertyId),
                    new Microsoft.Data.SqlClient.SqlParameter("@NewStatus", "продано"));
                property.PropertyPrice = 5700000.00m;
                db.SaveChanges();
            }

            // Обновление связанных сделок
            string ownerName = "Иванов Иван Иванович";
            var deals = db.Deals
                .Include(d => d.Property)
                .Join(db.Owners,
                      d => d.OwnerId,
                      o => o.OwnerId,
                      (d, o) => new { Deal = d, Owner = o })
                .Where(jo => jo.Deal.Property.PropertyAddress == propertyAddress && jo.Owner.OwnerName == ownerName)
                .Select(jo => jo.Deal);
            foreach (var deal in deals)
            {
                deal.DealAmount = 5800000.00m;
            }

            // Сохранить изменения
            db.SaveChanges();
        }

        static void Delete(RealEstateAgencyContext db)
        {
            // Удаление объекта недвижимости (сделки удалятся автоматически из-за ON DELETE CASCADE)
            string propertyAddress = "ул. Ленина, д. 10";
            var property = db.Properties
                .Where(p => p.PropertyAddress == propertyAddress);
            db.Properties.RemoveRange(property);

            // Сохранить изменения
            db.SaveChanges();
        }

        public static void Main(string[] args)
        {
            // Настройка конфигурации
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Настройка DI
            var services = new ServiceCollection();
            var connectionString = configuration.GetConnectionString("RealEstateAgency") ??
                throw new InvalidOperationException("Connection string 'RealEstateAgency' not found.");
            services.AddDbContext<RealEstateAgencyContext>(options =>
                options.UseSqlServer(connectionString));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RealEstateAgencyContext>();

                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Select(db);
                Console.WriteLine("====== Будет выполнена вставка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Insert(db);
                Console.WriteLine("====== Выборка после вставки ========");
                Select(db);
                Console.WriteLine("====== Будет выполнено обновление данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Update(db);
                Console.WriteLine("====== Выборка после обновления ========");
                Select(db);
                Console.WriteLine("====== Будет выполнено удаление данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Delete(db);
                Console.WriteLine("====== Выборка после удаления ========");
                Select(db);
            }
            Console.Read();
        }
    }
}
