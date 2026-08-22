using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models
{
    public class DiaryEntry
    {
        //1. Создал модель - класс с полями. Впоследствии создам таблицу в БД с помощью миграции,
        //колонки которой будут совпадать с полями модели.
        //2. Добавил атрибуты валидации для полей модели. Атрибут [Required] указывает, что поле обязательно для заполнения.
        //3. Какой умный ителисенс! Он сам подсказывает, что нужно добавить using System.ComponentModel.DataAnnotations; для использования атрибута [Required].
        //4 пункт в файле appsettings.json - строка подключения к БД. В моем случае это локальная БД SQL Server Express.
        //5 пункт в файле ApplicationDbContext
        //6 пункт в файле ApplicationDbContext
        //7 пункт в файле Program.cs 
        //8 пункт в файле ApplicationDbContext
        //9 пункт в файле ApplicationDbContext
        //10 пункт в файле DiaryEntriesController
        //11 пункт в файле DiaryEntriesController
        //12 пункт в файле DiaryEntriesController
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public DateTime Created {  get; set; }
    }
}
