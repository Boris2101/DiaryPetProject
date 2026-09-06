using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        //5. Создал класс ApplicationDbContext, который наследуется от DbContext. В конструкторе класса вызывается базовый конструктор с параметром options, который содержит настройки подключения к БД.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //6. Создал свойство DbSet<DiaryEntry>, которое будет представлять таблицу в БД для модели DiaryEntry.
        //Затем нужно выполнить add-migration и update-database, чтобы таблица была создана в БД (это делается через консоль NuGet).
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        //8. Переопределил метод OnModelCreating, который вызывается при создании модели данных. В данном случае вызывается базовый метод, который выполняет стандартные настройки модели.
        //При помощи данного метода произведу "посев" первичных данных в таблицу DiaryEntries
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //9. Использую метод HasData для добавления начальных данных в таблицу DiaryEntries. В данном случае добавляется одна запись с Id = 1, Title = "First Entry", Content = "This is my first diary entry." и Created = DateTime.Now.
            //затем нужно выполнить add-migration и update-database, чтобы данные были добавлены в БД.
            modelBuilder.Entity<DiaryEntry>().HasData(
                new DiaryEntry
                {
                    Id = 1,
                    Title = "First Entry",
                    Content = "This is my first diary entry.",
                    Created = new DateTime(2026,08,16)
                }
                );
        }



    }
}
