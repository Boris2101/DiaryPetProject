using DiaryApp.Data;
using DiaryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.Controllers
{
    //8. Создал контроллер
    public class DiaryEntriesController : Controller
    {

        //10. Создал объект AppllicationDbContext, который будет использоваться для взаимодействия с БД.
        //В конструкторе контроллера передаю объект ApplicationDbContext через dependency injection,
        //тем самым не создавая объект ApplicationDbContext вручную, а используя уже созданный объект, который настроен в Program.cs
        private readonly ApplicationDbContext _db;

        public DiaryEntriesController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            //11. При помощи кода ниже создал список моделей DiaryEntry, который будет использоваться для отображения данных в представлении.
            //При помощи _db (это ссылка на БД) получаю все записи из таблицы DiaryEntries и преобразую их в список моделей DiaryEntry.
            //Модель DiaryEntry содержит свойства Id, Title, Content и Created, которые соответствуют столбцам таблицы DiaryEntries в БД.
            //Что неудивительно, так как раньше сам и создал таблицу на основе модели DiaryEntry, котоая содержит колонки Id, Title, Content и Created.
            List<DiaryEntry> objDiaryEntryList = _db.DiaryEntries.ToList();
            return View(objDiaryEntryList);
        }

        //12. Создаю новый метод действия Create, который будет вести на одноименное представление Create.cshtml, где пользователь сможет создать новую запись в дневнике.
        public IActionResult Create()
        {
            return View();
        }
        //13. Создаю новый метод действия Create, который будет обрабатывать POST-запросы с данными новой записи в дневнике. Данные будут поступать из представления Create.
        [HttpPost]
        public IActionResult Create(DiaryEntry obj)//принимаю здесь объект DiaryEntry, поступивший из представления Create
        {
            _db.DiaryEntries.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            //Также при помощи _db.DiaryEntries.Add(obj) поместил в БД поступившие из представления данные, а затем сохранил в БД изменения
        }
    }
}
