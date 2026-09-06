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
        //14. Добавил валидацию на стороне сервера, которая проверяет, что длина заголовка записи больше или равна 3 символам. Если длина заголовка меньше 3 символов, то добавляется ошибка в ModelState и данные не сохраняются в БД.
        {
            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title too short");
            }

            //если данные в модели корректны и подходят под условия валидации, то загружаем их в БД
            else if (ModelState.IsValid)
            {
                _db.DiaryEntries.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

            //Также при помощи _db.DiaryEntries.Add(obj) поместил в БД поступившие из представления данные, а затем сохранил в БД изменения
        }

        //15. Создал новый метод действия, который отвечает за редактирование данных, ранее поступивших в БД.
        //Здесь метод Edit принимает параметр id, который будет использоваться для поиска записи в БД по её идентификатору.
        //Параметр id поступает из представления Index, где выводятся все объекты из БД и у кажного из них по шелчку на кнопку Edit передаём Id.
        //Если запись найдена, то она передаётся в представление Edit.cshtml для редактирования. Если запись не найдена,
        //то возвращается ошибка NotFound.
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            DiaryEntry? diaryEntry = _db.DiaryEntries.Find(id);

            if (diaryEntry == null)
            {
                return NotFound();
            }

            return View(diaryEntry);
        }

        [HttpPost]
        public IActionResult Edit(DiaryEntry obj)//16. Создал метод действия с атрибутом Post, который будет слать в БД обновленные данные из представления Edit
        //принимаю здесь объект DiaryEntry, поступивший из представления Edit
        //Добавил валидацию на стороне сервера, которая проверяет, что длина заголовка записи больше или равна 3 символам. Если длина заголовка меньше 3 символов, то добавляется ошибка в ModelState и данные не сохраняются в БД.
        {
            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title too short");
            }

            //если данные в модели корректны и подходят под условия валидации, то загружаем их в БД
            else if (ModelState.IsValid)
            {
                _db.DiaryEntries.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //17. Создал метод действия Delete, который отвечает за удаление данных из БД
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            DiaryEntry? diaryEntry = _db.DiaryEntries.Find(id);

            if (diaryEntry == null)
            {
                return NotFound();
            }

            return View(diaryEntry);
        }

        [HttpPost]
        public IActionResult Delete(DiaryEntry obj)//18. Создал метод действия с атрибутом Post, который будет удалять из БД данные.
        //принимаю здесь объект DiaryEntry, поступивший из представления Delete
        {

                _db.DiaryEntries.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }

    }
}
