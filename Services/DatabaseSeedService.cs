using Data;
using Data.Models;
using Data.Records;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DatabaseSeedService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly UserManager<SkillBoxUser> userManager;

        public DatabaseSeedService(SkillBoxDbContext dbContext,
            UserManager<SkillBoxUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task SeedAsync()
        {
            if (!await dbContext.Categories.AnyAsync())
            {
                Random rnd = new Random();
                var urlStart = "https://images.pexels.com/photos/";
                var urlBetween = "/pexels-photo-";
                var urlEnd = "?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1";
                var extension = ".jpeg";

                #region Phone numbers
                string[] phoneNumbers = new string[25];
                for (int i = 0; i < phoneNumbers.Count(); i++)
                {
                    string rndNums = rnd.Next(0, 10).ToString();
                    for (int j = 0; j < 7; j++)
                    {
                        rndNums += rnd.Next(0, 10).ToString();
                    }
                    phoneNumbers[i] = "3598" + rndNums;
                }
                #endregion

                #region Citites
                var cityLastId = BaseRecord.GetLastId<City>();
                List<City> cities = new List<City>();
                for (int i = 0; i < 25; i++)
                {
                    cities.Add(BaseRecord.GetById<City>(rnd.Next(1, cityLastId + 1))!);
                }
                #endregion

                #region Ordinary users
                var ordinaryUser1 = new SkillBoxUser
                {
                    UserName = "georgivasilev82",
                    NormalizedUserName = "GEORGIVASILEV82",
                    Email = "georgivasilev82@gmail.com",
                    NormalizedEmail = "GEORGIVASILEV82@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Георги",
                    LastName = "Василев",
                    PhoneNumber = phoneNumbers[0],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[0],
                };
                await userManager.CreateAsync(ordinaryUser1, "123456");

                var ordinaryUser2 = new SkillBoxUser
                {
                    UserName = "elizaivanova96",
                    NormalizedUserName = "ELIZAIVANOVA96",
                    Email = "elizaivanova96@gmail.com",
                    NormalizedEmail = "ELIZAIVANOVA96@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Елиза",
                    LastName = "Ивановa",
                    PhoneNumber = phoneNumbers[1],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[1],
                };
                await userManager.CreateAsync(ordinaryUser2, "123456");
                #endregion

                #region Admins
                var admin1 = new SkillBoxUser
                {
                    UserName = "pavelangelov94",
                    NormalizedUserName = "PAVELANGELOV94",
                    Email = "pavelangelov94@gmail.com",
                    NormalizedEmail = "PAVELANGELOV94@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Павел",
                    LastName = "Ангелов",
                    PhoneNumber = phoneNumbers[2],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[2],
                };
                await userManager.CreateAsync(admin1, "123456");

                var admin2 = new SkillBoxUser
                {
                    UserName = "ninapetrova97",
                    NormalizedUserName = "NINAPETROVA97",
                    Email = "ninapetrova97@gmail.com",
                    NormalizedEmail = "NINAPETROVA97@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Нина",
                    LastName = "Петрова",
                    PhoneNumber = phoneNumbers[3],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[3],
                };
                await userManager.CreateAsync(admin2, "123456");
                #endregion

                #region Commenters
                var commenter1 = new SkillBoxUser
                {
                    UserName = "popov88",
                    NormalizedUserName = "POPOV88",
                    Email = "popov88@gmail.com",
                    NormalizedEmail = "POPOV88@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Виктор",
                    LastName = "Попов",
                    PhoneNumber = phoneNumbers[4],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[4],
                };
                await userManager.CreateAsync(commenter1, "123456");

                var commenter2 = new SkillBoxUser
                {
                    UserName = "georgiev90",
                    NormalizedUserName = "GEORGIEV90",
                    Email = "georgiev90@gmail.com",
                    NormalizedEmail = "GEORGIEV90@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Андрей",
                    LastName = "Георгиев",
                    PhoneNumber = phoneNumbers[5],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[5],
                };
                await userManager.CreateAsync(commenter2, "123456");

                var commenter3 = new SkillBoxUser
                {
                    UserName = "dimitrova93",
                    NormalizedUserName = "DIMITROVA93",
                    Email = "dimitrova93@gmail.com",
                    NormalizedEmail = "DIMITROVA93@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Йоана",
                    LastName = "Димитрова",
                    PhoneNumber = phoneNumbers[6],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[6],
                };
                await userManager.CreateAsync(commenter3, "123456");

                var commenter4 = new SkillBoxUser
                {
                    UserName = "vulchev89",
                    NormalizedUserName = "VULCHEV89",
                    Email = "vulchev89@gmail.com",
                    NormalizedEmail = "VULCHEV89@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Мартин",
                    LastName = "Вълчев",
                    PhoneNumber = phoneNumbers[7],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[7],
                };
                await userManager.CreateAsync(commenter4, "123456");

                var commenter5 = new SkillBoxUser
                {
                    UserName = "ilieva92",
                    NormalizedUserName = "ILIEVA92",
                    Email = "ilieva92@gmail.com",
                    NormalizedEmail = "ILIEVA92@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Симона",
                    LastName = "Илиева",
                    PhoneNumber = phoneNumbers[8],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[8],
                };
                await userManager.CreateAsync(commenter5, "123456");
                #endregion

                #region Skillers
                var skiller1 = new SkillBoxUser
                {
                    UserName = "ivanivanov91",
                    NormalizedUserName = "IVANIVANOV91",
                    Email = "ivanivanov91@gmail.com",
                    NormalizedEmail = "IVANIVANOV91@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    PhoneNumber = phoneNumbers[9],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[9],
                };
                await userManager.CreateAsync(skiller1, "123456");

                var skiller2 = new SkillBoxUser
                {
                    UserName = "mariapetrova88",
                    NormalizedUserName = "MARIAPETROVA88",
                    Email = "mariapetrova88@gmail.com",
                    NormalizedEmail = "MARIAPETROVA88@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Мария",
                    LastName = "Петрова",
                    PhoneNumber = phoneNumbers[10],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[10],
                };
                await userManager.CreateAsync(skiller2, "123456");

                var skiller3 = new SkillBoxUser
                {
                    UserName = "petervasilev87",
                    NormalizedUserName = "PETERVASILEV87",
                    Email = "petervasilev87@gmail.com",
                    NormalizedEmail = "PETERVASILEV87@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Петър",
                    LastName = "Василев",
                    PhoneNumber = phoneNumbers[11],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[11],
                };
                await userManager.CreateAsync(skiller3, "123456");

                var skiller4 = new SkillBoxUser
                {
                    UserName = "elenageorgieva86",
                    NormalizedUserName = "ELENAGEORGIEVA86",
                    Email = "elenageorgieva86@gmail.com",
                    NormalizedEmail = "ELENAGEORGIEVA86@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Елена",
                    LastName = "Георгиева",
                    PhoneNumber = phoneNumbers[12],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[12],
                };
                await userManager.CreateAsync(skiller4, "123456");

                var skiller5 = new SkillBoxUser
                {
                    UserName = "georgigeorgiev83",
                    NormalizedUserName = "GEORGIGEORGIEV83",
                    Email = "georgigeorgiev83@gmail.com",
                    NormalizedEmail = "GEORGIGEORGIEV83@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Георги",
                    LastName = "Георгиев",
                    PhoneNumber = phoneNumbers[13],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[13],
                };
                await userManager.CreateAsync(skiller5, "123456");

                var skiller6 = new SkillBoxUser
                {
                    UserName = "nadezhdaivanova89",
                    NormalizedUserName = "NADEZHDAIVANOVA89",
                    Email = "nadezhdaivanova89@gmail.com",
                    NormalizedEmail = "NADEZHDAIVANOVA89@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Надежда",
                    LastName = "Иванова",
                    PhoneNumber = phoneNumbers[14],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[14],
                };
                await userManager.CreateAsync(skiller6, "123456");

                var skiller7 = new SkillBoxUser
                {
                    UserName = "dimitardimitrov85",
                    NormalizedUserName = "DIMITARDIMITROV85",
                    Email = "dimitardimitrov85@gmail.com",
                    NormalizedEmail = "DIMITARDIMITROV85@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Димитър",
                    LastName = "Димитров",
                    PhoneNumber = phoneNumbers[15],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[15],
                    WebsiteName = "NewHorizons"
                };
                await userManager.CreateAsync(skiller7, "123456");

                var skiller8 = new SkillBoxUser
                {
                    UserName = "victoriaangelova90",
                    NormalizedUserName = "VICTORIAANGELOVA90",
                    Email = "victoriaangelova90@gmail.com",
                    NormalizedEmail = "VICTORIAANGELOVA90@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Виктория",
                    LastName = "Ангелова",
                    PhoneNumber = phoneNumbers[16],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[16],
                };
                await userManager.CreateAsync(skiller8, "123456");

                var skiller9 = new SkillBoxUser
                {
                    UserName = "stefanstefanov84",
                    NormalizedUserName = "STEFANSTEFANOV84",
                    Email = "stefanstefanov84@gmail.com",
                    NormalizedEmail = "STEFANSTEFANOV84@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Стефан",
                    LastName = "Стефанов",
                    PhoneNumber = phoneNumbers[17],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[17],
                };
                await userManager.CreateAsync(skiller9, "123456");

                var skiller10 = new SkillBoxUser
                {
                    UserName = "annavasileva92",
                    NormalizedUserName = "ANNAVASILEVA92",
                    Email = "annavasileva92@gmail.com",
                    NormalizedEmail = "ANNAVASILEVA92@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Анна",
                    LastName = "Василева",
                    PhoneNumber = phoneNumbers[18],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[18],
                };
                await userManager.CreateAsync(skiller10, "123456");

                var skiller11 = new SkillBoxUser
                {
                    UserName = "kirilpetrov93",
                    NormalizedUserName = "KIRILPETROV93",
                    Email = "kirilpetrov93@gmail.com",
                    NormalizedEmail = "KIRILPETROV93@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Кирил",
                    LastName = "Петров",
                    PhoneNumber = phoneNumbers[19],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[19],
                };
                await userManager.CreateAsync(skiller11, "123456");

                var skiller12 = new SkillBoxUser
                {
                    UserName = "milenanikolova94",
                    NormalizedUserName = "MILENANIKOLOVA94",
                    Email = "milenanikolova94@gmail.com",
                    NormalizedEmail = "MILENANIKOLOVA94@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Милена",
                    LastName = "Николова",
                    PhoneNumber = phoneNumbers[20],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[20],
                };
                await userManager.CreateAsync(skiller12, "123456");

                var skiller13 = new SkillBoxUser
                {
                    UserName = "nikolaynikolov95",
                    NormalizedUserName = "NIKOLAYNIKOLOV95",
                    Email = "nikolaynikolov95@gmail.com",
                    NormalizedEmail = "NIKOLAYNIKOLOV95@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Николай",
                    LastName = "Николов",
                    PhoneNumber = phoneNumbers[21],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[21],
                };
                await userManager.CreateAsync(skiller13, "123456");

                var skiller14 = new SkillBoxUser
                {
                    UserName = "radostinadimitrova96",
                    NormalizedUserName = "RADOSTINADIMITROVA96",
                    Email = "radostinadimitrova96@gmail.com",
                    NormalizedEmail = "RADOSTINADIMITROVA96@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Радостина",
                    LastName = "Димитрова",
                    PhoneNumber = phoneNumbers[22],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[22],
                };
                await userManager.CreateAsync(skiller14, "123456");

                var skiller15 = new SkillBoxUser
                {
                    UserName = "vladimirvladimirov97",
                    NormalizedUserName = "VLADIMIRVLADIMIROV97",
                    Email = "vladimirvladimirov97@gmail.com",
                    NormalizedEmail = "VLADIMIRVLADIMIROV97@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Владимир",
                    LastName = "Владимиров",
                    PhoneNumber = phoneNumbers[23],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    City = cities[23],
                };
                await userManager.CreateAsync(skiller15, "123456");

                var skiller16 = new SkillBoxUser
                {
                    UserName = "stanimirageorgieva98",
                    NormalizedUserName = "STANIMIRAGEORGIEVA98",
                    Email = "stanimirageorgieva98@gmail.com",
                    NormalizedEmail = "STANIMIRAGEORGIEVA98@GMAIL.COM",
                    EmailConfirmed = true,
                    FirstName = "Станимира",
                    LastName = "Георгиева",
                    PhoneNumber = phoneNumbers[24],
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female,
                    City = cities[24],
                };
                await userManager.CreateAsync(skiller16, "123456");
                #endregion

                #region Categories
                var category1 = new Category()
                {
                    Name = "Домашни услуги",
                    MainImage = $"{urlStart}{12278557}{urlBetween}{12278557}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category1);

                var category2 = new Category()
                {
                    Name = "Почистване",
                    ParentCategory = category1,
                    MainImage = $"{urlStart}{5217899}{urlBetween}{5217899}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category2);

                var category3 = new Category()
                {
                    Name = "Други",
                    ParentCategory = category1,
                    MainImage = $"{urlStart}{4506274}{urlBetween}{4506274}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category3);

                var category4 = new Category()
                {
                    Name = "Градинарство и домашни любимци",
                    MainImage = $"{urlStart}{3265437}{urlBetween}{3265437}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category4);

                var category5 = new Category()
                {
                    Name = "Професионални услуги",
                    MainImage = $"{urlStart}{730564}{urlBetween}{730564}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category5);

                var category6 = new Category()
                {
                    Name = "Лични услуги",
                    ParentCategory = category5,
                    MainImage = $"{urlStart}{927022}{urlBetween}{927022}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category6);

                var category7 = new Category()
                {
                    Name = "Консултантски услуги",
                    ParentCategory = category5,
                    MainImage = $"{urlStart}{4960379}{urlBetween}{4960379}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category7);

                var category8 = new Category()
                {
                    Name = "Събития и Развлечения",
                    MainImage = $"{urlStart}{2774556}{urlBetween}{2774556}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category8);

                var category9 = new Category()
                {
                    Name = "Събития",
                    ParentCategory = category8,
                    MainImage = $"{urlStart}{668137}{urlBetween}{668137}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category9);

                var category10 = new Category()
                {
                    Name = "Развлечения",
                    ParentCategory = category8,
                    MainImage = $"{urlStart}{587741}{urlBetween}{587741}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category10);

                var category11 = new Category()
                {
                    Name = "Технически услуги",
                    MainImage = $"{urlStart}{2582937}{urlBetween}{2582937}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category11);

                var category12 = new Category()
                {
                    Name = "Здраве и Красота",
                    MainImage = $"{urlStart}{863926}{urlBetween}{863926}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category12);

                var category13 = new Category()
                {
                    Name = "Здраве",
                    ParentCategory = category12,
                    MainImage = $"{urlStart}{327098}{urlBetween}{327098}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category13);

                var category14 = new Category()
                {
                    Name = "Красота",
                    ParentCategory = category12,
                    MainImage = $"{urlStart}{20139713}{urlBetween}{20139713}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category14);

                var category15 = new Category()
                {
                    Name = "Образование",
                    MainImage = $"{urlStart}{4145153}{urlBetween}{4145153}{extension}{urlEnd}"
                };
                await dbContext.Categories.AddAsync(category15);
                #endregion

                #region Services
                string[] names = new string[52]
                {
                    "Почистване на килими",
                    "Почистване на прозорци",
                    "Почистване на апартаменти",
                    "Миене на коли",
                    "Поддръжка на градина",
                    "Гледане на деца",
                    "Гледане на кучета или котки",
                    "Направа на сайт и поддръжка",
                    "Поправка на покриви",
                    "Ремонт на коли",
                    "Монтаж и поддръжка на парални и сушилни",
                    "Изготвяне и доставка на букети",
                    "Рисуване на карикатури",
                    "Водопроводчик",
                    "Електротехник",
                    "Хамалски услуги",
                    "Езикови уроци",
                    "Поправка на телефони",
                    "Шивашки услуги",
                    "Фризьорски услуги",
                    "Масаж",
                    "Физио терапия",
                    "Частен йога учител",
                    "Финансови съвети ,счетоводство",
                    "Брокерски услуги",
                    "Личен асистент",
                    "Частен диетолог",
                    "Частен фитнес инструктор",
                    "Поддръжка на социални медии",
                    "Монтаж на мебели",
                    "Маникюр и педикюр",
                    "Частни уроци по учебни предмети",
                    "Фотографски услуги",
                    "Охрана",
                    "Адвокатски услуги",
                    "Личен шофьор",
                    "Гримьор",
                    "Кариерен съветник (career coach)",
                    "Уроци по танци",
                    "Уроци по китара",
                    "Психолог",
                    "Поправка на черна и бяла техника",
                    "Превод",
                    "Видео обработка и монтаж",
                    "Логопет",
                    "DJ",
                    "Аниматори за детско събитие",
                    "Курс по самоотбрана",
                    "Изработка на торти по поръчка",
                    "Боядисване",
                    "Вашият козметик",
                    "Професионален готвач"
                };

                string[] descriptions = new string[52]
                {
                    "Добре дошли в нашата услуга за почистване на килими! Ние се грижим за безупречната чистота и свежест на вашите килими с усмивка. С нашите професионални методи и оборудване, вашите килими ще бъдат безпогрешно чисти и красиви. Оставете нас да се погрижим за тях, като ви осигурим спокойствие и удовлетворение от резултата. Свържете се с нас днес и осигурете си чистота и уют в дома си!",
                    "Почистване на прозорци! Ние се грижим за ярката и чиста атмосфера във вашия дом или офис. С професионални методи и внимателно внимание към всеки детайл, ние осигуряваме кристална яснота и блестящо чисти прозорци. Оставете ни да направим светлината да проникне във вашия живот с яркост и свежест!",
                    "Пълно почистване на апартаменти! Ние сме тук, за да направим вашият дом просторен, уютен и безупречно чист. С нашето внимание към детайлите и професионално качество на работа, ще се погрижим за всеки ъгъл на вашия апартамент. Освободете се от стреса на почистването и се наслаждавайте на безгрижието във вашия свеж и приятен дом!",
                    "Ние предлагаме услуга за професионално миене на коли, която ще ви осигури блясък и чистота както отвън, така и отвътре. С висококачествени продукти и внимание към всеки детайл, ние ще ви осигурим перфектно почистена кола, която се радва на безупречен вид. Ние ще вземем колата от вашия адрес мръсна и ще ви я върнем на същия адрес като чисто нова. Предоставете ни възможността да преобразим вашата кола в оазис на чистота и комфорт.",
                    "Запазете вашата градина в безупречно състояние с нашата услуга за поддръжка! Ние се грижим за всеки елемент от вашата градина с любов и професионализъм. От косене на тревата и подрязване на храсти до поддържане на цветята и използване на висококачествени торове - ние сме тук, за да направим вашата градина да процъфтява. Доверете ни се да превърнем вашия външен спейс в истински рай за отдих и красота!",
                    "Прецизна грижа за вашите деца! Като опитен и отговорен човек, предлагам услуга за гледане на деца с любов и внимание. От игри в дома до разходки на открито и помощ с учебни задачи, аз съм тук, за да се погрижа за вашите малки съкровища, като им осигуря безопасна и забавна среда. Вие можете да се чувствате спокойни, като знаете, че вашите деца са в добри ръце.",
                    "Грижа и внимание за вашите любими домашни любимци! Като ентусиазиран пет ситър, предлагам услуга за гледане на кучета и котки с грижа и обич. От разходки в парка до игри в дома и хранене, аз съм тук, за да се грижа за вашите пухкави приятели, като им осигуря внимание и уют във вашия дом. Вие може да се отпуснете, като знаете, че вашите домашни любимци са в безопасни и любящи ръце.",
                    "Като опитен уеб дизайнер и разработчик, предлагам услуга за създаване на персонализиран уебсайт според вашите нужди и предпочитания. От началното проектиране до реализацията и последващата поддръжка, аз ще се грижа за всеки аспект на вашия уебсайт, така че той да остава актуален, функционален и привлекателен за вашите потребители. Вие можете да се фокусирате на своя бизнес, докато аз вземам грижа за онлайн присъствието ви.",
                    "Аз съм квалифициран специалист по покриви, предлагам услуга за професионално ремонтиране и поддръжка на вашия покрив. Независимо дали става въпрос за поправка на пукнати или изтекли места, замяна на бити тухли или регулиране на капаки, аз съм тук, за да ви осигуря качествени и надеждни решения за вашата къща или бизнес. Доверете се на моите умения и опит, за да ви предоставя професионално обслужване и спокойствие относно вашия покрив.",
                    "Ремонт и поддръжка на вашия автомобил - ето какво предлагам! С многогодишен опит като автомобилен механик, грижата за вашата кола е в добри ръце. Независимо дали става въпрос за малки поправки или сериозни ремонти, аз съм на ваше разположение за всякаква помощ. Без значение дали се нуждаете от замяна на части, изправяне на каросерията или диагностика на проблеми, ще се погрижа автомобилът ви да бъде във форма и отново на пътя. Вие можете да се чувствате спокойни, че вашият автомобил е в добри ръце.",
                    "Поддръжка и монтаж на перални и сушилни - предоставям тази услуга с грижа и професионализъм. Независимо дали ви трябва поправка на вашия сушилник или инсталиране на нова пералня, аз съм тук, за да ви помогна. Със специализиран опит и внимание към всеки детайл, гарантираме, че вашите уреди ще функционират безпроблемно. Оставете ми да се погрижа за перфектната работа на вашите перални и сушилни, за да ви осигуря спокоен и удобен ежедневен живот.",
                    "Изготвяне и доставка на букети - ние предлагаме тази услуга с любов и внимание към детайлите. Независимо от повод или събитие, ние сме тук, за да ви помогнем да изразите чувствата си с красиви цветя. Със специален акцент върху естетика и качество, всеки букет е уникален и внимателно изработен от нашият опитен екип. Оставете нас да се погрижим за изработката и доставката на вашия персонализиран букет, за да зарадвате вашия получател и да направите специалния ден още по-специален.",
                    "Изработка на карикатури по поръчка - предлагам тази услуга с любов към рисуването и внимание към детайлите. Независимо от повода - подарък за рожден ден, сватба или юбилей, моят артистичен талант е тук, за да ви помогне да зарадвате вашия получател с уникална и забавна карикатура. С моя професионализъм и внимателен подход, всеки портрет е персонализиран и внимателно изработен. Ще създам уникално произведение на изкуството, което ще запечати спомените и ще достави усмивки на лицата на всички.",
                    "Ремонт и инсталиране на водопроводи - предлагам тази услуга с професионализъм и надеждност. Независимо дали имате нужда от поправка на изтичане или инсталиране на нови тръби, моят опит и уменият са тук, за да ви помогнат. Със специализирани умения и внимание към всеки детайл, гарантирам бързо и качествено решение на вашите проблеми с водопроводите. Доверете се на мен, за да ви осигуря безопасно и надеждно водоснабдяване във вашия дом или бизнес.",
                    "Ремонт и инсталация на електрически мрежи - предлагам тази услуга с професионализъм и отговорност. Независимо дали имате нужда от поправка на електрически проблеми във вашата електрическа мрежа или инсталиране на нови компоненти, моите умения и опит са на ваше разположение. Със специализирани умения и внимание към всеки детайл, гарантирам качествено и надеждно решение на всяка електротехническа задача.",
                    "Пренасяне и преместване на битово и офис оборудване - предоставям тази услуга с грижа и професионализъм. Независимо от това дали имате нужда от помощ при местене на мебели, електроуреди или други тежки предмети, моите услуги като хамал са на разположение. Със специализирани умения и внимание към вашите вещи, гарантирам бързо и ефективно преместване на вашите вещи на време. Не сте сами в това преместване.",
                    "Специализиран съм в обучение на различни езици и съм на разположение да ви помогна да подобрите вашия говор, писане, разбиране и четене. Със структурирани уроци и индивидуален подход, гарантирам успешното ви усвояване на новия език.",
                    "При всякакви проблеми свързани с телефони и таблети като счупен екран, лоша батерия или бавно зареждане. Можете да ме потърсите моментално. Аз ще дойда до адреса ви, ще направя бърза инспекция на телефона ви и ще се уверя че работи перфектно до 2-4 дни.",
                    "Преправяне на булчински и бални рокли, но също и на панталони и блузи. Вече няма нужда да изхвърляте дрехите които са ви омаляли или прекалено големи. Петно или скъсано- мога да помогна и с това. Изпратете ми имейл с кратко описание на ситуацията, за да дойда подготвена в дома ви с всичко необходимо за поправката на платовете.",
                    "Със своя опит и креативност, аз работя внимателно с всеки клиент, за да осигуря удовлетворителни резултати, които отразяват вашата индивидуалност и стил. Вярвам в важността на доброто обслужване и личната връзка с клиентите си, затова винаги се стремя да създам уютна и приятна обстановка. Всички материали които използвам са с био произход и са напълно безопасни за хора с алегрии или непоносимост към бои. Нека заедно работим за постигане на вашия идеален външен вид и да направим всеки момент при нас специален и незабравим.",
                    "Разкрийте дълбоката релаксация и възстановяване с моите масажни услуги. Предлагам широка гама масажи- отпускащи, интензевни, арома терапия, болкоуспокояваши и раздвижващи. Не се колебайте да се свържете и да си запазите час възможно най-скоро.",
                    "С комбинация от различни техники и дълбоко разбиране за работата с мускулите и тъканите, аз създавам индивидуални програми за всяка отделна сесия. Моят цел е да ви осигуря релаксация, като същевременно помагам на вашето тяло да се възстанови и възобнови. С моята професорска степен и опит в различни държави (Германия, Австрия, Нидерландия, Словакия), съм готова да започна лечението на вашите болежки. Дори не е необходимо да напускате комфорта на вашия дом, аз ще дойда на място с всичко необходимо за сесията.",
                    "Персонализирани йога сесии - като частен йога учител, създавам индивидуални програми, които отговарят на вашите нужди и цели. Със своя опит и внимание към вашия уникален физически и емоционален статус, създаваме уроци, които са напълно насочени към вашето благополучие и развитие. Независимо дали искате да подобрите гъвкавостта си, да намалите стреса или да поддържате общото си здравословно състояние, аз съм тук, за да ви подкрепя и насърчавам.",
                    "Моят подход е ориентиран към вашия уникален ситуационен контекст и ви помага да разберете вашите възможности за инвестиране, осигуряване на пенсия, управление на дългове и други финансови въпроси. Със своя опит и професионализъм, аз ви предоставям ясни и разбираеми съвети, които ви помагат да вземете информирани решения за вашето бъдеще. Позволете ми да ви бъда партньор във вашия финансов път и да ви помогна да постигнете финансовата стабилност и благополучие, което желаете.",
                    "С активно изследване на пазара и внимателен анализ на вашите предпочитания и цели, аз ви предоставям обективни съвети през целия процес на покупка или продажба на имот. Независимо дали търсите дом за семейството си, инвестиционен обект или комерсиално пространство, моят ангажимент е да ви помогна да намерите най-доброто решение за вас.",
                    "Вече не успявате да се справите с всичките ви задачи, имейли, запазване на срещи и т.н. Ежедневието ви е напрегнато и живеете в притеснение, че винаги забравяте нещо- мисля че мога да ви помогна да сложите живота си в правилния ред с моите невероятни способности по планиране и управление на задачи. Всеки от клиентите ви ще получи отговор на време, а вие никога няма да пропускате среща дори в личния ви живот.",
                    "Заедно ще разглеждаме вашите текущи хранителни навици, здравословни проблеми и фитнес цели, за да разберем какви промени могат да бъдат направени за постигане на желаните резултати. Независимо дали търсите да отслабнете, да подобрите енергийно ниво или да поддържате определена хранителна режима.",
                    "Искате ли да влезете във форма бързо и без да губите времето учейки цялата теория зад всяко упражнение. Можете да се възползвате от моите знания в залата и да ви помогна с персонализирана програма според вашата текуща форма и желания резултат.",
                    "Като вашият социален медийен мениджър, аз предлагам индивидуални решения и стратегии за управление на вашите социални медии. Със своя професионален опит и креативност в областта на дигиталния маркетинг, работя с вас, за да създадем персонализиран план за присъствие в социалните медии, който отговаря на вашите цели и нужди. Чрез анализ на вашата целева аудитория, конкурентите във вашата ниша и вашите собствени маркетингови цели, аз разработвам стратегия за съдържание и публикации, които ще ангажират вашата аудитория и увеличат видимостта на вашата марка в социалните медии.",
                    "Ако имате нужда от помощ със сглобяването на мебели поради ред причини, например не ви достига младост, сила или просто не ви бива в тези неща. Не се колебайте да се свържете с мен, имам опит в сглобяването на кухни, гардероби и спални.",
                    "Чрез внимателно избрани продукти и внимание към детайлите, гарантирам, че вашите нокти са оформени перфектно и изглеждат невероятно. Независимо дали търсите класически, елегантен маникюр или креативен и модерен дизайн, аз мога да ви го осигуря във вашия дом за максимум един час. Ако все още нямате вдъхновение, не се притеснявайте аз ще ви предложа много и различни дизайни с декорации и изграждане. Това важи и за педикюри.",
                    "И вашето дете ли се затруднява със задачите по математика, български и биология? Всеки родител би опитал да помогне на своя ученик, но понякога това е мислията невъзможна...материала е просто някак прекалено труден за обяснение. На помощ идвам аз, професионалист по учебния материал от 1-10 клас.",
                    "Искате ли вашите моменти ще бъдат заснети с внимание към детайлите и емоциите. Независимо дали става въпрос за сватба, портретна сесия, семейно събитие или корпоративен ивент, аз съм тук, за да запечатам най-добрите моменти от вашето събитие. Фотосесии с различни декори на закрито, но не само. Запазете си час възможно най-скоро за да обсъдим най-новите трендове във фотографията.",
                    "Чрез използване на най-съвременните технологии за сигурност и разработване на индивидуална стратегия за предотвратяване на рискове, гарантирам високо ниво на безопасност и защита за вас и вашите имущества. Мога да ви следвам където пожелаете или мога да наблюдавам дома ви онлайн. Избора е ваш.",
                    "Персонализирани адвокатски услуги, насочени към вашите нужди. Осигурявам ясни и обективни съвети, за да ви помогна да решите вашите правни проблеми. Моят подход е да бъда ваш партньор през целия процес, осигурявайки ви професионално и етично обслужване при бракоразводни дела, бизнес дела, подялба на имоти, измами и други.",
                    "Чрез висок стандарт на професионализъм и дискретност, гарантирам вашето удобство и поверителност по време на пътуване. Независимо дали става въпрос за лични или служебни ангажименти, моят цел е да ви предоставям удобство и спокойствие по време на вашите пътувания. Няма кой да закара децата на училище, няма проблем, аз мога да направя това! Часа няма значение, аз ще бъда на разположение за вас.",
                    "Предлагам персонализиран грим, който подчертава вашата натурална красота и излъчва стил и увереност. Правя грим подходящ за ежедневието, балове, сватби, рожденни дни, годишини, но също и подготовка за фотосесии. Ако не сте убедени в моите способности винаги можем да направим пробен грим преди събитието.",
                    "Открийте своя професионален път с помощта на кариерния съветник! Като вашият личен кариерен наставник, аз ще ви помогна да изградите ясно разбиране за вашите професионални цели, да идентифицирате вашите силни страни и интереси, както и да определите най-подходящите за вас кариерни възможности. Заедно ще подготвим вашата автобиография и мотивационно писмо, както и ще се подготвим за интервю, за да се уверим че винаги ще давате правилния отговор на коварните въпроси на отдел човешки ресуси.",
                    "Заплетете се в ритъма с уроците по танци! Като вашият танцов инструктор, ще ви покажа как да изразите своята страст към движението и музиката чрез танц. С моя опит и индивидуален подход, ще ви науча на основите на различни стилове - от салса и ча-ча-ча до хип-хоп и съвременен танц.",
                    "Предлагам индивидуални уроци по китара, които ще ви въведат в магията на музиката. С моята страст към инструмента и преподавателски опит, аз ще ви насоча в света на акордите, скалите и ритъма, като ви помогна да развиете техниката си и да откриете вашия уникален стил. Ще ви предоставя индивидуални уроци, които ще ви насърчават да изразите себе си чрез музиката и да се наслаждавате на процеса на учене. На разположение в дома ви.",
                    "Имате ли лоши сънища или наприятни преживявания напоследък, чувствате ли се тревожни и тъжни на моменти? Ако можете да се разпозаете в някои от тези симптоми, то тогава ви моля да се обърнете към мен, вашия професионален психолог, които е винаги отворен да ви изслуша и помогне да достигнете до корена на проблема и да го разрешите. Винаги сте добре дошли в кабинета ми, но също така правя визитации по домовете, както и срещи онлайн. От колко сесии имате нужда точно ще определим през първата ни среща.",
                    "Предлагам професионални услуги за диагностика, ремонт и поддръжка на различни уреди, включително перални машини, съдомиялни машини, хладилници, фризери, котлони, фурни и други. Всеки уред който може да бъде спасен от изхвърляне, ще бъде поправен за по-малко пари от закупуването на нов.",
                    "Като вашият сътрудник в областта на езиковите услуги, предоставям професионални преводачески услуги за превод на текстове от един език на друг. Със своята езикова компетентност и културна чувствителност, аз осигурявам точен и качествен превод на вашите документи, съобщения или материали. Срещу допълнително заплащане мога да ви придружа на бизнес срещи или конференции в България и в чужбина.",
                    "С използване на монтаж, корекция на цветовете и добавяне на специални ефекти, създавам видеоклипове, които впечатляват зрителите и предават вашето послание с яснота и емоция. Имам опит в лични събития, бизнес презентации или рекламни клипове, целта ми е да предоставям качествени и професионални видеопродукции.",
                    "Със специализация в речевото развитие, предлагам индивидуални уроци и терапия за подобряване на комуникационните умения на децата. Моят подход включва търпеливо внимание към нуждите на всяко дете и използване на ефективни техники за развитие на речта и самочувствие в общуването. Ако вашето дете е в детска градина и изпитва затруднения с изговарянето на определени думи, не се колебайте да се свържете с мен. Провеждам срещите в дома или в кабинета ми.",
                    "Моят професионален подход включва не само предоставяне на качествена музика, но и умело възпроизвеждане на настроението на вашето събитие. Позволете ми да направя вашата вечер незабравима и пълна с музикални емоции и танци. Успешно боравя с всички жанрове музика и умея да ги миксирам според настроението на гостите. В повечето случаи смяната ми продължава 5 часа, но можем да се уговорим и за повече ако партито трябва да продължи по-дълго.",
                    "Нашите аниматори са опитни и креативни, готови да създадат вълшебна атмосфера и да развеселят децата с множество забавни игри, лица за боядисване, възможности за творчество и интерактивни представления. Със забавни и разнообразни активности, ние създаваме уникално и незабравимо изживяване за децата и техните родители.",
                    "Персонализиран курс по самоотбрана - като вашият инструктор по самозащита, аз предоставям индивидуални уроци и обучение за увереност и безопасност в различни ситуации. Със своя опит и експертиза в областта на бойните изкуства и самозащита, аз ви уча на техники и стратегии за предпазване на себе си и вашите близки в защитена среда, които може да използвате на улицата при нужда.",
                    "Моят подход е да бъда внимателен към вашите желания и изисквания, като същевременно внимавам да създам торта, която отразява вашата индивидуалност и стил. Защото вида не е всичко, аз използвам най-добрите продукти на пазара за изработката на тортите и ги пека в деня на събитието, за да гарантирам че са винаги пресни.",
                    "Ако имате една или няколко стаи за боядисване и бихте искали всяка капка боя да попадне на правилното място на стената, моля свържете се.",
                    "Страдате ли от кожни проблеми като упорито акне, зачервявания, брадавици и т.н. Спрете да изпробвате домашни методи и се доверете на експерт да разреши вашите проблеми. Няма кожно заболяване което няма решение, свържете се с мен чрез тази обява и си запазете час.",
                    "Как са готварските ви умения? Ако не са много добри или имате нужда от помощ за приготвянето на важна вечеря удома, не се колебайте да се свържете с мен. За мен готвенето е не само работа, но и удоволствие."
                };

                int[] mainImgIds = new int[52]
                {
                    6195122,
                    18266080,
                    3768910,
                    4870664,
                    2013782,
                    296301,
                    2215599,
                    196655,
                    1453799,
                    2244746,
                    5591460,
                    1179026,
                    4649254,
                    8486923,
                    5691589,
                    4246202,
                    5905923,
                    1388947,
                    1266139,
                    3738339,
                    3757942,
                    275768,
                    1375883,
                    265087,
                    8469937,
                    4427430,
                    15319047,
                    1552242,
                    533446,
                    5805487,
                    791157,
                    714698,
                    320617,
                    18275559,
                    6077326,
                    1135379,
                    3089849,
                    4344878,
                    2188012,
                    1407322,
                    4101143,
                    3822850,
                    5238126,
                    1117132,
                    5896577,
                    844928,
                    7180617,
                    7045483,
                    461431,
                    994164,
                    3738349,
                    1267320
                };

                int[] imgSet1 = new int[52]
                {
                    3965509,
                    4239091,
                    4239031,
                    4218867,
                    1023402,
                    1449934,
                    7055937,
                    574071,
                    209235,
                    279949,
                    4993073,
                    5409707,
                    380709,
                    4194858,
                    163100,
                    2652945,
                    5428002,
                    196653,
                    5467466,
                    3356170,
                    7598366,
                    5794059,
                    3094230,
                    210574,
                    8292826,
                    357514,
                    15319038,
                    414029,
                    243925,
                    4554425,
                    3997379,
                    4145153,
                    1391786,
                    1119152,
                    5668772,
                    3160544,
                    8558242,
                    5439453,
                    358010,
                    1656066,
                    5699466,
                    16385070,
                    5217882,
                    927444,
                    3769981,
                    625644,
                    3071456,
                    6253310,
                    2531546,
                    3990359,
                    3851790,
                    175753
                };

                int[] imgSet2 = new int[52]
                {
                    38325,
                    713297,
                    4099467,
                    3775124,
                    2433500,
                    1001914,
                    1870301,
                    546819,
                    3771111,
                    3822843,
                    8774424,
                    1058771,
                    17959072,
                    7859953,
                    4792521,
                    4247770,
                    159581,
                    1476321,
                    3738088,
                    3993449,
                    3757952,
                    5794024,
                    3823039,
                    210607,
                    8470803,
                    4308096,
                    15319043,
                    2247179,
                    927629,
                    4554426,
                    3422099,
                    3184317,
                    442573,
                    207574,
                    618158,
                    7433,
                    4911192,
                    5989933,
                    209948,
                    625788,
                    4101164,
                    1476318,
                    6281880,
                    1051544,
                    5905703,
                    417458,
                    7099947,
                    8612011,
                    1702373,
                    221027,
                    3212164,
                    1040685
                };

                var services = new List<SkillBoxService>();
                var serviceLastId = BaseRecord.GetLastId<ServiceStatus>();
                for (int i = 0; i < 52; i++)
                {
                    var service = new SkillBoxService()
                    {
                        Name = names[i],
                        Description = descriptions[i],
                        MainImage = $"{urlStart}{mainImgIds[i]}{urlBetween}{mainImgIds[i]}{extension}{urlEnd}",
                        Images = $"{urlStart}{imgSet1[i]}{urlBetween}{imgSet1[i]}{extension}{urlEnd}|" +
                        $"{urlStart}{imgSet2[i]}{urlBetween}{imgSet2[i]}{extension}{urlEnd}|",
                        ServiceStatus = BaseRecord.GetById<ServiceStatus>(rnd.Next(0, serviceLastId + 1))!
                    };
                    services.Add(service);
                }

                //Relationships and prices (discount)
                SetCustomEntityProperties(
                    services[0], category2, skiller1, 20);
                SetCustomEntityProperties(
                    services[1], category2, skiller9, 10);
                SetCustomEntityProperties(
                    services[2], category2, skiller3, 30);
                SetCustomEntityProperties(
                    services[3], category2, skiller2, 30, 5);
                SetCustomEntityProperties(
                    services[4], category4, skiller6, 5);
                //
                SetCustomEntityProperties(
                    services[5], category6, skiller15, 20);
                SetCustomEntityProperties(
                    services[6], category4, skiller7, 8);
                services[6].WebsiteName = "NewHorizons";
                SetCustomEntityProperties(
                    services[7], category11, skiller12, 35, 3);
                SetCustomEntityProperties(
                    services[8], category11, skiller5, 25000, 3000);
                SetCustomEntityProperties(
                    services[9], category11, skiller14, 500, 350);
                //
                SetCustomEntityProperties(
                    services[10], category11, skiller11, 30, 27);
                SetCustomEntityProperties(
                    services[11], category9, skiller16, 50, 15);
                services[11].WebsiteName = "Buketi.bg";
                SetCustomEntityProperties(
                    services[12], category9, skiller4, 30, 4);
                SetCustomEntityProperties(
                    services[13], category11, skiller13, 35);
                SetCustomEntityProperties(
                    services[14], category10, skiller10, 50);
                //
                SetCustomEntityProperties(
                    services[15], category3, skiller8, 30);
                SetCustomEntityProperties(
                    services[16], category15, skiller1, 27, 2);
                SetCustomEntityProperties(
                    services[17], category11, skiller9, 50);
                SetCustomEntityProperties(
                    services[18], category14, skiller3, 25);
                SetCustomEntityProperties(
                    services[19], category14, skiller2, 20);
                //
                SetCustomEntityProperties(
                    services[20], category13, skiller6, 40, 7);
                SetCustomEntityProperties(
                    services[21], category13, skiller15, 38);
                SetCustomEntityProperties(
                    services[22], category13, skiller7, 29, 1);
                SetCustomEntityProperties(
                    services[23], category7, skiller12, 232);
                SetCustomEntityProperties(
                    services[24], category7, skiller5, 178);
                //
                SetCustomEntityProperties(
                    services[25], category6, skiller14, 15);
                SetCustomEntityProperties(
                    services[26], category13, skiller11, 19, 3);
                SetCustomEntityProperties(
                    services[27], category14, skiller16, 30);
                SetCustomEntityProperties(
                    services[28], category7, skiller4, 150, 34);
                SetCustomEntityProperties(
                    services[29], category3, skiller13, 26);
                //
                SetCustomEntityProperties(
                    services[30], category14, skiller10, 30, 1.50m);
                SetCustomEntityProperties(
                    services[31], category15, skiller8, 24);
                SetCustomEntityProperties(
                    services[32], category9, skiller1, 40, 8);
                SetCustomEntityProperties(
                    services[33], category6, skiller9, 20);
                SetCustomEntityProperties(
                    services[34], category7, skiller3, 300);
                //
                SetCustomEntityProperties(
                    services[35], category6, skiller2, 30);
                SetCustomEntityProperties(
                    services[36], category14, skiller6, 50, 2.43m);
                SetCustomEntityProperties(
                    services[37], category7, skiller15, 100, 15);
                SetCustomEntityProperties(
                    services[38], category15, skiller7, 4, 1);
                SetCustomEntityProperties(
                    services[39], category15, skiller12, 15, 1);
                //
                SetCustomEntityProperties(
                    services[40], category6, skiller5, 50);
                SetCustomEntityProperties(
                    services[41], category11, skiller14, 40);
                SetCustomEntityProperties(
                    services[42], category7, skiller11, 60);
                SetCustomEntityProperties(
                    services[43], category9, skiller16, 40, 0.50m);
                SetCustomEntityProperties(
                    services[44], category15, skiller4, 30);
                //
                SetCustomEntityProperties(
                    services[45], category10, skiller13, 200);
                SetCustomEntityProperties(
                    services[46], category10, skiller10, 100);
                SetCustomEntityProperties(
                    services[47], category15, skiller8, 30, 4.23m);
                SetCustomEntityProperties(
                    services[48], category9, skiller1, 29, 111.90m);
                SetCustomEntityProperties(
                    services[49], category3, skiller9, 50);
                //
                SetCustomEntityProperties(
                    services[50], category13, skiller10, 60, 10.50m);
                SetCustomEntityProperties(
                    services[51], category3, skiller1, 20);

                services.ForEach(async s => await dbContext.Services.AddAsync(s));

                #endregion

                #region Chats
                var chat1 = new Chat()
                {
                    Name = $"{skiller1.FirstName}, {skiller7.FirstName}",
                    Service = services[0]
                };
                await dbContext.Chats.AddAsync(chat1);

                var chat2 = new Chat()
                {
                    Name = $"{skiller8.FirstName}, {ordinaryUser1.FirstName}",
                    Service = services[15]
                };
                await dbContext.Chats.AddAsync(chat2);

                var chat3 = new Chat()
                {
                    Name = $"{skiller1.FirstName}, {commenter5.FirstName}",
                    Service = services[16]
                };
                await dbContext.Chats.AddAsync(chat3);

                var chat4 = new Chat()
                {
                    Name = $"{skiller3.FirstName}, {ordinaryUser2.FirstName}",
                    Service = services[18]
                };
                await dbContext.Chats.AddAsync(chat4);

                var chat5 = new Chat()
                {
                    Name = $"{skiller6.FirstName}, {commenter3.FirstName}",
                    Service = services[36]
                };
                await dbContext.Chats.AddAsync(chat5);

                var chat6 = new Chat()
                {
                    Name = $"{skiller15.FirstName}, {commenter4.FirstName}",
                    Service = services[37]
                };
                await dbContext.Chats.AddAsync(chat6);

                var chat7 = new Chat()
                {
                    Name = $"{skiller3.FirstName}, {ordinaryUser1.FirstName}",
                    Service = services[34]
                };
                await dbContext.Chats.AddAsync(chat7);

                var chat8 = new Chat()
                {
                    Name = $"{skiller7.FirstName}, {commenter1.FirstName}",
                    Service = services[6]
                };
                await dbContext.Chats.AddAsync(chat8);

                var chat9 = new Chat()
                {
                    Name = $"{skiller5.FirstName}, {commenter2.FirstName}",
                    Service = services[24]
                };
                await dbContext.Chats.AddAsync(chat9);

                var chat10 = new Chat()
                {
                    Name = $"{skiller4.FirstName}, {commenter5.FirstName}",
                    Service = services[44]
                };
                await dbContext.Chats.AddAsync(chat10);
                #endregion

                #region ChatUsers - [Chats - Participants]
                var chatUser1 = new ChatUser
                {
                    Chat = chat1,
                    User = skiller1 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser1);

                var chatUser2 = new ChatUser
                {
                    Chat = chat1,
                    User = skiller7 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser2);

                var chatUser3 = new ChatUser
                {
                    Chat = chat2,
                    User = skiller8 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser3);

                var chatUser4 = new ChatUser
                {
                    Chat = chat2,
                    User = ordinaryUser1 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser4);

                var chatUser5 = new ChatUser
                {
                    Chat = chat3,
                    User = skiller1 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser5);

                var chatUser6 = new ChatUser
                {
                    Chat = chat3,
                    User = commenter5 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser6);

                var chatUser7 = new ChatUser
                {
                    Chat = chat4,
                    User = skiller3 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser7);

                var chatUser8 = new ChatUser
                {
                    Chat = chat4,
                    User = ordinaryUser2 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser8);

                var chatUser9 = new ChatUser
                {
                    Chat = chat5,
                    User = skiller6, //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser9);

                var chatUser10 = new ChatUser
                {
                    Chat = chat5,
                    User = commenter3, //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser10);

                var chatUser11 = new ChatUser
                {
                    Chat = chat6,
                    User = skiller15 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser11);

                var chatUser12 = new ChatUser
                {
                    Chat = chat6,
                    User = commenter4 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser12);

                var chatUser13 = new ChatUser
                {
                    Chat = chat7,
                    User = skiller3 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser13);

                var chatUser14 = new ChatUser
                {
                    Chat = chat7,
                    User = ordinaryUser1 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser14);

                var chatUser15 = new ChatUser
                {
                    Chat = chat8,
                    User = skiller7, //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser15);

                var chatUser16 = new ChatUser
                {
                    Chat = chat8,
                    User = commenter1, //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser16);

                var chatUser17 = new ChatUser
                {
                    Chat = chat9,
                    User = skiller5 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser17);

                var chatUser18 = new ChatUser
                {
                    Chat = chat9,
                    User = commenter2 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser18);

                var chatUser19 = new ChatUser
                {
                    Chat = chat10,
                    User = skiller4 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser19);

                var chatUser20 = new ChatUser
                {
                    Chat = chat10,
                    User = commenter5 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser20);
                #endregion

                #region Chat groups
                var chatGroup1 = new Chat()
                {
                    Name = $"{skiller1.FirstName}, {commenter3.FirstName}",
                    Service = services[48]
                };
                await dbContext.Chats.AddAsync(chatGroup1);

                var chatGroup2 = new Chat()
                {
                    Name = $"{skiller1.FirstName}, {commenter5.FirstName}",
                    Service = services[16]
                };
                await dbContext.Chats.AddAsync(chatGroup2);

                var chatGroup3 = new Chat()
                {
                    Name = $"{skiller3.FirstName}, {skiller12.FirstName}",
                    Service = services[34]
                };
                await dbContext.Chats.AddAsync(chatGroup3);
                #endregion

                #region ChatUsers - [Chats - Participants]
                var chatUser21 = new ChatUser
                {
                    Chat = chatGroup1,
                    User = skiller1 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser21);

                var chatUser22 = new ChatUser
                {
                    Chat = chatGroup1,
                    User = commenter3 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser22);

                var chatUser23 = new ChatUser
                {
                    Chat = chatGroup1,
                    User = commenter1 //Participant3
                };
                await dbContext.ChatUsers.AddAsync(chatUser23);

                var chatUser24 = new ChatUser
                {
                    Chat = chatGroup1,
                    User = ordinaryUser2 //Participant4
                };
                await dbContext.ChatUsers.AddAsync(chatUser24);

                var chatUser25 = new ChatUser
                {
                    Chat = chatGroup1,
                    User = skiller14 //Participant5
                };
                await dbContext.ChatUsers.AddAsync(chatUser25);
                //
                var chatUser26 = new ChatUser
                {
                    Chat = chatGroup2,
                    User = skiller1 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser26);

                var chatUser27 = new ChatUser
                {
                    Chat = chatGroup2,
                    User = commenter5 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser27);

                var chatUser28 = new ChatUser
                {
                    Chat = chatGroup2,
                    User = commenter4 //Participant3
                };
                await dbContext.ChatUsers.AddAsync(chatUser28);
                //
                var chatUser29 = new ChatUser
                {
                    Chat = chatGroup3,
                    User = skiller3 //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser29);

                var chatUser30 = new ChatUser
                {
                    Chat = chatGroup3,
                    User = skiller12 //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser30);

                var chatUser31 = new ChatUser
                {
                    Chat = chatGroup3,
                    User = skiller13 //Participant3
                };
                await dbContext.ChatUsers.AddAsync(chatUser31);
                #endregion

                #region UserMessages
                var userMessage1 = new UserMessage()
                {
                    Owner = skiller7,
                    Chat = chat1,
                    Content = "Здравей искам да ми почистиш килима?"
                };
                await dbContext.UserMessages.AddAsync(userMessage1);

                var userMessage2 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chat1,
                    Content = "Мога да дойда на място и да го взема. Кога?"
                };
                await dbContext.UserMessages.AddAsync(userMessage2);

                var userMessage3 = new UserMessage()
                {
                    Owner = skiller7,
                    Chat = chat1,
                    Content = "Утре в 9 и половина става ли?"
                };
                await dbContext.UserMessages.AddAsync(userMessage3);

                var userMessage4 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chat1,
                    Content = "Да, даже по-добре, за да имам време да го изпера днес."
                };
                await dbContext.UserMessages.AddAsync(userMessage4);

                var userMessage5 = new UserMessage()
                {
                    Owner = ordinaryUser1,
                    Chat = chat2,
                    Content = "Здравейте, трябва да се преместя в новата ми къща на 1 Април. Може ли да ми помогнете за 2-3 часа с някои от големите ми мебели?"
                };
                await dbContext.UserMessages.AddAsync(userMessage5);

                var userMessage6 = new UserMessage()
                {
                    Owner = skiller8,
                    Chat = chat2,
                    Content = "Да, разбира се, целя ми 1 Април е свободен засега, така че мога да остана допълнително ако се налага."
                };
                await dbContext.UserMessages.AddAsync(userMessage6);

                var userMessage7 = new UserMessage()
                {
                    Owner = ordinaryUser1,
                    Chat = chat2,
                    Content = "Супер, разбрахме се, елате към 10 часа сутрнита, за да имаме достатъчно време. Ще ви споделя локация."
                };
                await dbContext.UserMessages.AddAsync(userMessage7);

                var userMessage8 = new UserMessage()
                {
                    Owner = skiller8,
                    Chat = chat2,
                    Content = "Идеално, ще бъда там! До скоро."
                };
                await dbContext.UserMessages.AddAsync(userMessage8);

                var userMessage9 = new UserMessage()
                {
                    Owner = commenter5,
                    Chat = chat3,
                    Content = "Добър ден! Клалифициран ли сте да подготвяте ученици за IELTS изпита по англииски? Синът ми иска да учи в чужбина, а англииският му изобщо не е добър. Можете ли да помогнете като го подготвите за формата?"
                };
                await dbContext.UserMessages.AddAsync(userMessage9);

                var userMessage10 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chat3,
                    Content = "Добър ден и на вас! Имам опит и с трите формата на изпити които се изискват за чужбина. В момента помагам на много ученици и не мисля че мога да включа някой нов, но от май месец нататъка ще имам едно свободно място. Искате ли го? Два дни в седмицата."
                };
                await dbContext.UserMessages.AddAsync(userMessage10);

                var userMessage11 = new UserMessage()
                {
                    Owner = commenter5,
                    Chat = chat3,
                    Content = "Да, идеално."
                };
                await dbContext.UserMessages.AddAsync(userMessage11);

                var userMessage12 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chat3,
                    Content = "Добре, в такъв случай ще ви пиша след месец, за да изберем двата дни в седмицата, в които ще проведем уроците."
                };
                await dbContext.UserMessages.AddAsync(userMessage12);

                var userMessage13 = new UserMessage()
                {
                    Owner = ordinaryUser2,
                    Chat = chat4,
                    Content = "Здравейте, пиша ви във връзка с поправка на булчинска рокля. Купих роклята преди половин година, но за съжаление от тогава досега съм напълняла и роклята вече не ми става. Мислите ли че можете да ми помогнете и някак да я разширите? Цената няма значение, трябва просто да я оправим бързо защото сватбата е след 2 месеца. "
                };
                await dbContext.UserMessages.AddAsync(userMessage13);

                var userMessage14 = new UserMessage()
                {
                    Owner = skiller3,
                    Chat = chat4,
                    Content = "Да, разбирам проблема ви, не сте първата която се свързва с мен за тази задача. Няма проблем, можем да измислим нещо, но трябва да видя роклята първо и ще ви кажа."
                };
                await dbContext.UserMessages.AddAsync(userMessage14);

                var userMessage15 = new UserMessage()
                {
                    Owner = ordinaryUser2,
                    Chat = chat4,
                    Content = "Добре, ще ви я донеса в понеделник преди обяд."
                };
                await dbContext.UserMessages.AddAsync(userMessage15);

                var userMessage16 = new UserMessage()
                {
                    Owner = skiller3,
                    Chat = chat4,
                    Content = "Страхотно! Не се притеснявайте, ще я оправим преди сватбата."
                };
                await dbContext.UserMessages.AddAsync(userMessage16);

                var userMessage17 = new UserMessage()
                {
                    Owner = commenter3,
                    Chat = chat5,
                    Content = "Здравейте, сватбата ми е след 2 месеца и търся някой който иска да ми направи първо пробен грим и после на самия ден. Но само при условие че имате биологични гримове, тъй като много лесно правя алергии."
                };
                await dbContext.UserMessages.AddAsync(userMessage17);

                var userMessage18 = new UserMessage()
                {
                    Owner = skiller6,
                    Chat = chat5,
                    Content = "Хубаво е че споменахте за алергиите защото моите гримове са качествени, но не са биологични."
                };
                await dbContext.UserMessages.AddAsync(userMessage18);

                var userMessage19 = new UserMessage()
                {
                    Owner = commenter3,
                    Chat = chat5,
                    Content = "Добре, в такъв случай май няма да успеем да работим заедно за съжаление. Познавате ли някой друг гримьор, който има такива гримове."
                };
                await dbContext.UserMessages.AddAsync(userMessage19);

                var userMessage20 = new UserMessage()
                {
                    Owner = skiller6,
                    Chat = chat5,
                    Content = "Не, за съжаление не познавам никой толкова специализиран. Извинете, приятен ден и успех."
                };
                await dbContext.UserMessages.AddAsync(userMessage20);

                var userMessage21 = new UserMessage()
                {
                    Owner = commenter4,
                    Chat = chat6,
                    Content = "Имам голяма нужда от помощ за намирането на работа, кандидатствам всеки ден и никога не ми се обаждат. Може ли да ми помогнете."
                };
                await dbContext.UserMessages.AddAsync(userMessage21);

                var userMessage22 = new UserMessage()
                {
                    Owner = skiller15,
                    Chat = chat6,
                    Content = "Да, това е работата ми. Като за начало изпратете ми автобиографията си и мотивационното писмо, за да мога да видя дали имаме много работа по тях."
                };
                await dbContext.UserMessages.AddAsync(userMessage22);

                var userMessage23 = new UserMessage()
                {
                    Owner = commenter4,
                    Chat = chat6,
                    Content = "Добре, изпращам веднага, а кога ще можем да проведем сесия за да обсъдим потенциални работни места."
                };
                await dbContext.UserMessages.AddAsync(userMessage23);

                var userMessage24 = new UserMessage()
                {
                    Owner = skiller15,
                    Chat = chat6,
                    Content = "Чакайте да проверя... мисля че идния вторник съм свободен да се видим. До тогава."
                };
                await dbContext.UserMessages.AddAsync(userMessage24);

                var userMessage25 = new UserMessage()
                {
                    Owner = ordinaryUser1,
                    Chat = chat7,
                    Content = "Здравейте, имам проблем с хазяйн, който не иска да ми върне депозита от апартамента. Имам договора и всички необходими документи, но ми трябва адвокат който да внесе случая в съда и да ме защитава ако отсрещната страна оспорва. Колко добър сте в жилищно право?"
                };
                await dbContext.UserMessages.AddAsync(userMessage25);

                var userMessage26 = new UserMessage()
                {
                    Owner = skiller3,
                    Chat = chat7,
                    Content = "Повярвайте не сте първия ми случай, в който хазяина просто си прибира парите в джоба и никога не ги връща. Не се притесявайте, щом имате всички документи, със сигурност ще спечелим в съда. Нека да се срещнем за една сесия преди да внесем делото за да ми разкажете подробно всичко и да ми дадете документите. Кога сте свободна?!"
                };
                await dbContext.UserMessages.AddAsync(userMessage26);

                var userMessage27 = new UserMessage()
                {
                    Owner = ordinaryUser1,
                    Chat = chat7,
                    Content = "Да се срещнем още утре ако е възможно."
                };
                await dbContext.UserMessages.AddAsync(userMessage27);

                var userMessage28 = new UserMessage()
                {
                    Owner = skiller3,
                    Chat = chat7,
                    Content = "Да, мисля че ще бъде! Ще ви се обадя утре сутрин да потвърдя, че утре имам дело по друг случай."
                };
                await dbContext.UserMessages.AddAsync(userMessage28);

                var userMessage29 = new UserMessage()
                {
                    Owner = commenter1,
                    Chat = chat8,
                    Content = "Добър ден, свързвам се с вас защото искаме да отиваме на почивка семейно и няма кой да гледа любимото ни куче. Свободен ли си от 8 до 15 май?"
                };
                await dbContext.UserMessages.AddAsync(userMessage29);

                var userMessage30 = new UserMessage()
                {
                    Owner = skiller7,
                    Chat = chat8,
                    Content = "Да, няма проблем мога да поема ангажимента. Колко голамо е кучето?"
                };
                await dbContext.UserMessages.AddAsync(userMessage30);

                var userMessage31 = new UserMessage()
                {
                    Owner = commenter1,
                    Chat = chat8,
                    Content = "Пинчър е, много послушен. През тази една седмица може да останете в къщата ни заедно с кучето."
                };
                await dbContext.UserMessages.AddAsync(userMessage31);

                var userMessage32 = new UserMessage()
                {
                    Owner = skiller7,
                    Chat = chat8,
                    Content = "Добре, разбрахме се, в такъв случай ме очаквайте на 8 май сутринта на адреса ви. Хубав ден!"
                };
                await dbContext.UserMessages.AddAsync(userMessage32);

                var userMessage33 = new UserMessage()
                {
                    Owner = commenter2,
                    Chat = chat9,
                    Content = "Здравейте, Г-н Георгиев! Свързвам се с вас, тъй като искам да си закупя къща. Моите предпочитания са да е ново строителство, поне 1 спалня и половина и да не е повече от 100 000 лв. Мислите ли че звучи реалистично?"
                };
                await dbContext.UserMessages.AddAsync(userMessage33);

                var userMessage34 = new UserMessage()
                {
                    Owner = skiller5,
                    Chat = chat9,
                    Content = "Здравейте и на вас, след няколко минути ще ви изпратя всички имоти, които имам в момента за продажба, разгледайте ги и ми кажете, ако ви хареса нещо. В случай че нищо не ви хареса, мога да започна да търся за вас."
                };
                await dbContext.UserMessages.AddAsync(userMessage34);

                var userMessage35 = new UserMessage()
                {
                    Owner = commenter2,
                    Chat = chat9,
                    Content = "Добре, добра идея нека да направим и така е ще говорим после."
                };
                await dbContext.UserMessages.AddAsync(userMessage35);

                var userMessage36 = new UserMessage()
                {
                    Owner = skiller5,
                    Chat = chat9,
                    Content = "Добре разбрахме се, пишете какво мислите за имотите. Лек ден!"
                };
                await dbContext.UserMessages.AddAsync(userMessage36);

                var userMessage37 = new UserMessage()
                {
                    Owner = commenter5,
                    Chat = chat10,
                    Content = "Добър ден, чух много добри отзиви за вас и реших да се свържа."
                };
                await dbContext.UserMessages.AddAsync(userMessage37);

                var userMessage38 = new UserMessage()
                {
                    Owner = skiller4,
                    Chat = chat10,
                    Content = "Здравейте, радвам се да го чуя. Какъв е проблема на детето ви?"
                };
                await dbContext.UserMessages.AddAsync(userMessage38);

                var userMessage39 = new UserMessage()
                {
                    Owner = commenter5,
                    Chat = chat10,
                    Content = "Не може да казва Р. Забелязахме го от преди време и очаквахме да се оправи от самосебе си, но за съжаление ще имаме нужда от вашата професионална помощ."
                };
                await dbContext.UserMessages.AddAsync(userMessage39);

                var userMessage40 = new UserMessage()
                {
                    Owner = skiller4,
                    Chat = chat10,
                    Content = "Разбирам, не се притеснявайте, ще поправим този лек говорен дефект. Защо направо не ми се обадите?"
                };
                await dbContext.UserMessages.AddAsync(userMessage40);
                //
                var userMessage41 = new UserMessage()
                {
                    Owner = commenter3,
                    Chat = chatGroup1,
                    Content = "Здравейте, свързваме се с вас по случай наближаващата ни сватба. В чата добавих младоженеца- Виктор Попов, нашия сватбен агент Елиза Иванова, и майка ми Радостина Димитрова."
                };
                await dbContext.UserMessages.AddAsync(userMessage41);

                var userMessage42 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chatGroup1,
                    Content = "Добър ден на всички ви, радвам се че се запознаваме поради такъв хубав случай. Бихте ли споделили идеите ви за торта и колко гости мислите да поканите, за да предвидим каква да бъде тортата и колко ще ви струва."
                };
                await dbContext.UserMessages.AddAsync(userMessage42);

                var userMessage43 = new UserMessage()
                {
                    Owner = ordinaryUser2,
                    Chat = chatGroup1,
                    Content = "Това което младоженците вече споделиха, че харесват е тъмно синия цвят, мислят да го използват много в декорацията на сватбата. Харесват и бели рози, което също ще бъде част от декорацията, така че мисля че ако внесем малко цвят в тортата няма да е лошо."
                };
                await dbContext.UserMessages.AddAsync(userMessage43);

                var userMessage44 = new UserMessage()
                {
                    Owner = skiller14,
                    Chat = chatGroup1,
                    Content = "Да, харесва ми идеята и на мен, но да не е прекалено шарено, може би 1-2 цвята най-много. Абе най-добре розово. А гостите ще бъдат 120 човека, така че 130 парчета торта."
                };
                await dbContext.UserMessages.AddAsync(userMessage44);

                var userMessage45 = new UserMessage()
                {
                    Owner = commenter1,
                    Chat = chatGroup1,
                    Content = "А, само да не е всичко розово. По-добре по-неутрален цвят."
                };
                await dbContext.UserMessages.AddAsync(userMessage45);

                var userMessage46 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chatGroup1,
                    Content = "Мисля че разбирам какво имате наум, ще ви помоля за малко търпение в следващите 2 дни когато ще изработя 3 различни дизайна на тортата и ще ви ги изпратя. Когато си харесате дизейн, може да направим и избор на вкус и т.н До скоро и ако имате въпроси, моля обадете се."
                };
                await dbContext.UserMessages.AddAsync(userMessage46);
                //
                var userMessage47 = new UserMessage()
                {
                    Owner = commenter5,
                    Chat = chatGroup2,
                    Content = "Добър ден г-н Иванов. Свързваме се с вас защото и двамата искаме да учим в чужбина след 12 клас, но англииският ни не е на много добро ниво."
                };
                await dbContext.UserMessages.AddAsync(userMessage47);

                var userMessage48 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chatGroup2,
                    Content = "Здравейте ученици, кой клас сте и за кое ниво трябва да се подготвите?"
                };
                await dbContext.UserMessages.AddAsync(userMessage48);

                var userMessage49 = new UserMessage()
                {
                    Owner = commenter4,
                    Chat = chatGroup2,
                    Content = "Сега сме 10ти клас и нивото ни е колко A2, но трябва да го повишим до C1, защото това е което се изисква от университетите."
                };
                await dbContext.UserMessages.AddAsync(userMessage49);

                var userMessage50 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chatGroup2,
                    Content = "Разбирам, в такиъв случай може ли да споделите адрес на някой от вас където мога да дойда и да проведем бърз тест, за да знаем със сигурност нивото? Вторник другата седмица в 11 добре ли е?"
                };
                await dbContext.UserMessages.AddAsync(userMessage50);

                var userMessage51 = new UserMessage()
                {
                    Owner = commenter5,
                    Chat = chatGroup2,
                    Content = "Да, можете да дойдете на този адрес. Вторник в 11 е удобно и за двама ни. Има ли нещо което трябва да подготвим за изпита?"
                };
                await dbContext.UserMessages.AddAsync(userMessage51);

                var userMessage52 = new UserMessage()
                {
                    Owner = skiller1,
                    Chat = chatGroup2,
                    Content = "Не, нищо не е необходимо засега. Ще се видим значи другата седмица във вторник в 11 часа."
                };
                await dbContext.UserMessages.AddAsync(userMessage52);
                //
                var userMessage53 = new UserMessage()
                {
                    Owner = skiller12,
                    Chat = chatGroup3,
                    Content = "Здравейте г-н Василев! Свързваме се с вас по препоръка на наш познат, които вече работи с вас по бракоразводно дело. Нашия случай е същия, ние искаме да се разведем, но имаме много имоти заедно и фирма за подялба, което прави нещата малко по-сложни."
                };
                await dbContext.UserMessages.AddAsync(userMessage53);

                var userMessage54 = new UserMessage()
                {
                    Owner = skiller3,
                    Chat = chatGroup3,
                    Content = "Приятно ми е да се запознаем, какво точно имате за подялба и какви са вашите предпочитания, имате ли план кой ще остане във фирмата например?"
                };
                await dbContext.UserMessages.AddAsync(userMessage54);

                var userMessage55 = new UserMessage()
                {
                    Owner = skiller13,
                    Chat = chatGroup3,
                    Content = "Тъй като аз създадох фирмата, мисля че е редно аз да продължа да я управлявам, което означама че мога да заплатя на Милена нейната част от фирмата и тя да напусне. Останалата част от имотите мисля че можем да продадем и да си разделим по равно, тъй като сме се сдобили с тях по време на брака."
                };
                await dbContext.UserMessages.AddAsync(userMessage55);

                var userMessage56 = new UserMessage()
                {
                    Owner = skiller12,
                    Chat = chatGroup3,
                    Content = "Да, аз също съм съгласна с това, може да продадем всичко и да оставим по един апартамент на всеки от нас, който има същата стойност като другия. Мисля че така е честно."
                };
                await dbContext.UserMessages.AddAsync(userMessage56);

                var userMessage57 = new UserMessage()
                {
                    Owner = skiller3,
                    Chat = chatGroup3,
                    Content = "В такъв случай мисля че можем да започнем процеса от утре. Хубаво е че вече сте обсъдили и решили всичко предварително. Аз ще се заема с подробностите и ще ви изпратя по-късно линк, чрез който да ми изпратите всичките си документи."
                };
                await dbContext.UserMessages.AddAsync(userMessage57);

                var userMessage58 = new UserMessage()
                {
                    Owner = skiller13,
                    Chat = chatGroup3,
                    Content = "Добре, ще очакваме. Ако имате въпроси, не се колебайте да ни се обадите."
                };
                await dbContext.UserMessages.AddAsync(userMessage58);
                #endregion

                //TODO Add Reviews
                await dbContext.SaveChangesAsync();
            }
        }
        private static void SetCustomEntityProperties(
            SkillBoxService service,
            Category category,
            SkillBoxUser owner,
            decimal price,
            decimal discount = 0)
        {
            service.Category = category;
            service.Owner = owner;
            service.OwnerName = $"{owner.FirstName} {owner.LastName}";
            service.PhoneNumber = owner.PhoneNumber;
            service.City = owner.City;
            service.Price = price;
            service.Discount = discount;
        }
    }
}