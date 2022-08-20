using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Domain;
using Rick_and_Morty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rick_and_Morty.Infrastructure.Persistence.Seeds
{
    public static class DefaultData
    {
        public static async Task SeedCharactersAsync(IRickAndMortyContext context)
        {
            var count = context.Characters.Count();

            if (count != 0) return;

            var characters = await GetCharacters(context);

            await context.Characters.AddRangeAsync(characters);
            await context.SaveChangesAsync();

        }

        public static async Task SeedLocationsAsync(IRickAndMortyContext context)
        {
            var count = context.Locations.Count();

            if (count != 0) return;

            var locations = GetLocations();

            await context.Locations.AddRangeAsync(locations);
            await context.SaveChangesAsync();

        }

        public static async Task SeedEpisodesAsync(IRickAndMortyContext context)
        {
            var count = context.Episodes.Count();

            if (count != 0) return;

            var episodes = GetEpisodes();

            await context.Episodes.AddRangeAsync(episodes);
            await context.SaveChangesAsync(); 

        }

        public static async Task SeedCharactersInEpisodeAsync(IRickAndMortyContext context)
        {
            var count = context.EpisodeCharacters.Count();

            if (count != 0) return;

            var episodes = await GetCharactersInEpisode(context);

            await context.EpisodeCharacters.AddRangeAsync(episodes);
            await context.SaveChangesAsync();

        }

        private static async Task<List<Character>> GetCharacters(IRickAndMortyContext context)
        {
            var locations = await context.Locations
                .Where(l => l.IsDelete == false)
                .AsNoTracking()
                .ToListAsync();

            var characters = new List<Character>() 
            {
                new Character() { 
                    FirstName = "Санчез", 
                    LastName = "Рик", 
                    Status = StatusEnums.Alive, 
                    About = "Главный протагонист мультсериала «Рик и Морти». Безумный ученый, чей алкоголизм, безрассудность и социопатия заставляют беспокоиться семью его дочери.",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Рик_Санчез_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Измерение C-137").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Смит",
                    LastName = "Морти",
                    Status = StatusEnums.Alive,
                    About = "Является одним из двух главных героев сериала. Приходится внуком Рику и часто вынужден ходить по пятам на его различных «злоключениях». Морти посещает школу имени Гарри Герпсона вместе со своей сестрой Саммер. Влюблен в Джессику.",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Морти_Смит_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Смит",
                    LastName = "Саммер",
                    Status = StatusEnums.Alive,
                    About = "Старшая сестра Морти. Посещает школу Гарри Герпсона вместе с Морти.",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Саммер_Смит_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Измерение C-137").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Смит",
                    LastName = "Джерри",
                    Status = StatusEnums.Alive,
                    About = "Персонаж мультсериала 'Рик и Морти'. Муж Бет, отец Саммер и Морти. Часто конфликтует с женой и её отцом. Комплексует из-за своего низкого интеллекта.",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Джерри_Смит_002.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Смит",
                    LastName = "Бет",
                    Status = StatusEnums.Alive,
                    About = "Дочь Рика, жена Джерри, мать Морти и Саммер. Она работает лошадиным хирургом в Больнице Святого Экуиса.",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Бет_Смит_002.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "",
                    LastName = "Джессика",
                    Status = StatusEnums.Alive,
                    About = "Ученица средней школы имени Гарри Герпсона. Она популярная девушка в математическом классе Морти, сам Морти влюблён в неё и часто имеет сюрреалистические мечты, почти исключительно сексуальные по своей природе. К большому разочарованию Морти она уже находится в отношениях с Брэдом, с которым она часто испытывает трудности. Ее озвучивает Кэри Уолгрен",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Джессика_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Санчез",
                    LastName = "Диана",
                    Status = StatusEnums.Unknown,
                    About = "Бывшая жена Рика Санчеза и мать Бет, теща Джерри Смита и бабушка Саммер и Морти (от материнской линии). Её имя можно обнаружить в начале 3-го сезона, что также является её первым появлением.",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Диана_Санчез_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Смит",
                    LastName = "Леонард",
                    Status = StatusEnums.Alive,
                    About = "Отец Джерри, муж Джойс, дедушка Саммер и Морти.",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Леонард_Смит_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Измерение C-137").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Смит",
                    LastName = "Джойс",
                    Status = StatusEnums.Alive,
                    About = "Жена Леонарда, мать Джерри, бабушка Морти и Саммер.",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Joyce_in_Anatomy_Park.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Гутерман",
                    LastName = "Тэмми",
                    Status = StatusEnums.Dead,
                    About = "Агент Галактической Федерации под прикрытием. Тэмми учится в старшей школе имени Гарри Герпсона. Она хорошая подруга Саммер Смит.",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Тэмми_Гутерман_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "Вагина",
                    LastName = "Джин",
                    Status = StatusEnums.Alive,
                    About = "Является директором средней школы имени Гарри Герпсона. Его озвучивает Фил Хендри",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Джин_Вагина_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "",
                    LastName = "Нэнси",
                    Status = StatusEnums.Alive,
                    About = "Одноклассница и подруга Саммер.",
                    Gender = GenderEnums.Female,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Нэнси_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "",
                    LastName = "Кровотёк",
                    Status = StatusEnums.Alive,
                    About = "Лидер обитателей Постапокалиптического измерения, где он охотился и убивал тех, кто не относился к его группе людей. Появился в серии «Рикман с камнем».",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Кровотёк_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Постапокалиптическое измерение").Select(l => l.Id).FirstOrDefault(),
                },
                new Character() {
                    FirstName = "",
                    LastName = "Президент",
                    Status = StatusEnums.Alive,
                    About = "Впервые появился в эпизоде ​​«Пора Швифтануться» в качестве второстепенного персонажа и вернулся в «Рикчжурский Мортидат» в качестве главного антагониста.",
                    Gender = GenderEnums.Male,
                    Race = "Человек",
                    ImageName = "http://173.249.20.184:7001/images/Президент_001.jpg",
                    PlaceOfBirthId = locations.Where(l => l.Name == "Земля").Select(l => l.Id).FirstOrDefault(),
                }
            };

            return characters;
        }

        private static List<Location> GetLocations()
        {
            var locations = new List<Location>()
            {
                new Location() { 
                    Name = "Громфлом Прайм", 
                    Type = "Планета", 
                    Measurements = "Измерение подмены", 
                    About = "Ранее это была хорошая для жизни планеты. Но случился кризис, вызванный Риком Санчесом, из-за которого общество громфломитов было полностью разрушено. Воцарилась анархия, а деньги обесценились. Большинство домов были уничтожены, отчего цена на оставшиеся возросла.", 
                    ImageName = "http://173.249.20.184:7001/images/Громфлом_Прайм_001.jpg" 
                },
                new Location() { 
                    Name = "Межпространственная таможня", 
                    Type = "Здание", 
                    Measurements = "", 
                    About = "Межпространственная таможня — локация, впервые показанная в серии «Пилот». Она используется как база для межгалактических путешествий, а также служит таможней между галактиками или вселенными. Контролируется Галактической Федерацией.", 
                    ImageName = "http://173.249.20.184:7001/images/Межпространственная_таможня_001.jpg" 
                },
                new Location() { 
                    Name = "Птичий мир", 
                    Type = "Планета", 
                    Measurements = "Измерение подмены", 
                    About = "Это планета, где живет Птичья личность. Телесигналам с Земли требуется 20 лет, чтобы достичь этой планеты.", 
                    ImageName = "http://173.249.20.184:7001/images/Птичий_мир_001.jpg" 
                },
                new Location() { 
                    Name = "Цитадель Риков", 
                    Type = "", 
                    Measurements = "", 
                    About = "Секретная штаб-квартира, где собирается Совет Риков. Также является местом отдыха и местом встречи Риков и их Морти из различных вселенных.", 
                    ImageName = "http://173.249.20.184:7001/images/Цитадель_Риков.jpg" 
                },
                new Location() { 
                    Name = "Галактическая Федерация", 
                    Type = "Организация", 
                    Measurements = "С-137", 
                    About = "Крупная галактическая или межгалактическая организация, которая контролировала значительные области вселенной.", 
                    ImageName = "http://173.249.20.184:7001/images/Галактическая_Федерация_001.jpg" 
                },
                new Location() { 
                    Name = "Фруппиленд", 
                    Type = "Созданная реальность",
                    Measurements = "Фруппиленд", 
                    About = "Это искусственно созданный Риком Санчезом мир в 1980-е годы. Он сделал его из измененного квантового тессеракта для дочери Бет Смит, когда она была маленькой девочкой. Это место показано в одном эпизоде 'Основы Бет'.", 
                    ImageName = "http://173.249.20.184:7001/images/Фруппиленд_001.jpg" 
                },
                new Location() { 
                    Name = "Мир Шестерёнок", 
                    Type = "Планета", 
                    Measurements = "", 
                    About = "Планета, расположенная в Системе Шестерёнок. Это дом для Шестерёнкоголовых и других людей Шестерёнок, их мир является частью Галактической Федерации.",
                    ImageName = "http://173.249.20.184:7001/images/Мир_Шестерёнок_002.jpg" 
                },
                new Location() { 
                    Name = "Микровселенная", 
                    Type = "Искусственная крошечная вселенная", 
                    Measurements = "Измерение подмены", 
                    About = "Это искусственная микроскопическая вселенная, которая существует внутри Микровселеннского аккумулятора в Летающей тарелке Рика. Это вселенная, заполненная видами инопланетян, которые сформировали регулярное, повседневное общество.Когда инопланетяне работают, они генерируют источник питания, который заряжает аккумулятор для летающей тарелки Рика, а иногда и для его телефона.", 
                    ImageName = "http://173.249.20.184:7001/images/Микровселенная_001.jpg" 
                },
                new Location() { 
                    Name = "Вселенная Блендеров", 
                    Type = "Измерение", 
                    Measurements = "Вселенная Блендеров", 
                    About = "Это вселенная, наполненная блендерами и ничем другим, которая была показана в эпизоде Сказки из Цитадели.", 
                    ImageName = "http://173.249.20.184:7001/images/Вселенная_Блендеров_001.jpg" 
                },
                new Location() { 
                    Name = "Вселенная Блендеров", 
                    Type = "Измерение", 
                    Measurements = "Вселенная Блендеров", 
                    About = "Это вселенная, наполненная блендерами и ничем другим, которая была показана в эпизоде Сказки из Цитадели.", 
                    ImageName = "http://173.249.20.184:7001/images/Вселенная_Блендеров_001.jpg" 
                },
                new Location() {
                    Name = "Измерение C-137",
                    Type = "Мир",
                    Measurements = "C-137",
                    About = "Это мир, который часто называют родным миром для основных Рика и Морти.",
                    ImageName = "http://173.249.20.184:7001/images/Rick-and-morty-rick-potion-number-9-cronenbergs-1280px.jpg"
                },
                new Location() {
                    Name = "Земля",
                    Type = "Планета",
                    Measurements = "",
                    About = "Это планета, на которой проживает человеческая раса, и главное место для персонажей Рика и Морти. Возраст этой Земли более 4,6 миллиардов лет, и она является четвертой планетой от своей звезды.",
                    ImageName = "http://173.249.20.184:7001/images/Земля_001.jpg"
                },
                new Location() {
                    Name = "Постапокалиптическое измерение",
                    Type = "Измерение",
                    Measurements = "Постапокалиптическое измерение",
                    About = "Это версия Земли, населенная выжившими в апокалипсисе. Среди известных жителей измерения — Кровотёк и Элай.",
                    ImageName = "http://173.249.20.184:7001/images/Постапокалиптическое_измерение_001.jpg"
                },
            };

            return locations;
        }

        private static List<Episode> GetEpisodes()
        {
            var episodes = new List<Episode>()
            {
                new Episode()
                {
                    Name = "Пилот",
                    Season = 1,
                    Series = 1,
                    Plot = "Рик отправляется с Морти в путешествие в другое измерение, чтобы найти семена от Мегадеревьев, в то время как Джерри и Бет спорят о влиянии Рика на их сына. Морти помогает Рику нелегально перевезти Мегасемена из параллельного измерения.",
                    Premiere = DateTime.Parse("02.12.2013"),
                    ImageName = "http://173.249.20.184:7001/images/Пилот_001.jpg"
                },
                new Episode()
                {
                    Name = "Пёс-газонокосильщик",
                    Season = 1,
                    Series = 2,
                    Plot = "Рик соорудил шлем, делающий собак умнее, который надевают на своего пса Снаффлса. Рик и Морти отправляются в путешествие по снам людей, где встречают Страшного Тэрри.",
                    Premiere = DateTime.Parse("09.12.2013"),
                    ImageName = "http://173.249.20.184:7001/images/Пёс-газонокосильщик_001.jpg"
                },
                new Episode()
                {
                    Name = "Анатомический парк",
                    Season = 1,
                    Series = 3,
                    Plot = "Джерри собирается отмечать Рождество и приглашает свою семью на праздничный ужин. Пока Джерри со своими родителями и их новым другом Джейкобом отмечают праздник, Рик, уменьшив Морти, отправляет его в тело Рубена. Внутри Морти обнаруживает настоящий анатомический парк, где он знакомится с доктором Ксенон Блумом, охранником Пончо, подростком Энни и Роджером. Система безопасности Рубена отключилась и поэтому все опасные болезни в теле Рубена вышли на свободу.",
                    Premiere = DateTime.Parse("16.12.2013"),
                    ImageName = "http://173.249.20.184:7001/images/Анатомический_парк_001.jpg"
                },
                new Episode()
                {
                    Name = "М. Найт Шьямал-Инопланетяне!",
                    Season = 1,
                    Series = 4,
                    Plot = "Зигерионцы помещают Джерри и Рика в симуляцию, чтобы узнать секрет изготовления концентрированной темной материи.",
                    Premiere = DateTime.Parse("13.01.2014"),
                    ImageName = "http://173.249.20.184:7001/images/М._Найт_Шьямал-Инопланетяне!_001.jpg"
                },
                new Episode()
                {
                    Name = "Мисикс и разрушение",
                    Season = 1,
                    Series = 5,
                    Plot = "Рик дарит семье коробку с мисиксами — существами, которые появляются, чтобы выполнить одно задание и умереть. Рик и Морти отправляются в фэнтезийный мир в поисках приключений, где встречают великанов.",
                    Premiere = DateTime.Parse("20.01.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Мисикс_и_разрушение_001.jpg"
                },
                new Episode()
                {
                    Name = "Напиток Рика №9",
                    Season = 1,
                    Series = 6,
                    Plot = "Рик создает сыворотку, которая должна помочь приворожить Джессику к Морти. Но смешавшись с вирусом гриппа, сыворотка вызывает эпидемию.",
                    Premiere = DateTime.Parse("27.01.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Rick-and-Morty-Season-1-Episode-6-10-4c17.jpg"
                },
                new Episode()
                {
                    Name = "Воспитание Газорпазорпа",
                    Season = 1,
                    Series = 7,
                    Plot = "У Морти рождается сын от секс-робота, купленного Риком. Саммер и Рик отправляются на планету, где был создан секс-робот.",
                    Premiere = DateTime.Parse("10.03.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Воспитание_Газорпазорпа_001.jpg"
                },
                new Episode()
                {
                    Name = "Скандалы, Рик и расследования",
                    Season = 1,
                    Series = 8,
                    Plot = "Рик дарит семье прибор, позволяющий увидеть себя в альтернативной реальности, и улучшает телевизор, который начинает принимать все каналы Вселенной.",
                    Premiere = DateTime.Parse("17.03.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Скандалы,_Рик_и_расследования_001.jpg"
                },
                new Episode()
                {
                    Name = "Надвигается нечто риканутое",
                    Season = 1,
                    Series = 9,
                    Plot = "Морти должен сделать научный проект для школы, в чём ему помогает его отец Джерри, предлагая построить модель Солнечной системы. Джерри настаивает на том, что Плутон это планета, после чего его и Морти похищают плутонианцы. Саммер устраивается работать в магазин проклятых вещей.",
                    Premiere = DateTime.Parse("24.03.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Надвигается_нечто_риканутое_001.jpg"
                },
                new Episode()
                {
                    Name = "Близкие риконтакты риковой степени",
                    Season = 1,
                    Series = 10,
                    Plot = "Совет Риков обвиняет Рика в убийстве других альтернативных версий Рика.",
                    Premiere = DateTime.Parse("07.04.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Amfhioopko7i680t2kzx.jpg"
                },
                new Episode()
                {
                    Name = "Риксованный бизнес",
                    Season = 1,
                    Series = 11,
                    Plot = "Бетти и Джерри уезжают в тематический отпуск, посвященный Титанику». Рик и Саммер устраивают домашнюю вечеринку, в результате которой дом оказывается в ужасном состоянии. Рик замораживает время, чтобы успеть всё исправить до приезда Бетти и Джерри.",
                    Premiere = DateTime.Parse("14.04.2014"),
                    ImageName = "http://173.249.20.184:7001/images/Риксованный_бизнес_001.jpg"
                },
                new Episode()
                {
                    Name = "Рик во времени",
                    Season = 2,
                    Series = 1,
                    Plot = "Рик, Морти и Саммер размораживают время и возвращают все на круги своя. Но реальность разбивается на два (4, 8 и т. д.) параллельных мира, а Рик, Морти и Саммер оказываются в бесконечном небытие. В привычной реальности в это время Джерри сбивает оленя, а Бетти приходится его экстренно спасать.",
                    Premiere = DateTime.Parse("26.07.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Рик_во_времени_001.jpg"
                },
                new Episode()
                {
                    Name = "Успеть до Морти-ночи",
                    Season = 2,
                    Series = 2,
                    Plot = "Рик учит Морти управлять космическим кораблем. Вместе с Джерри они прилетают на планету с множеством игровых автоматов. Джерри попадает в детский сад для Джерри из всех возможных миров. Морти узнает, что Рик торгует оружием и осуждает его. Морти пытается спасти разумное облако газа, но все не так просто, как кажется…",
                    Premiere = DateTime.Parse("02.08.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Успеть_до_Морти-ночи_001.jpg"
                },
                new Episode()
                {
                    Name = "Аутоэротическая ассимиляция",
                    Season = 2,
                    Series = 3,
                    Plot = "Рик, Морти и Саммер откликнулись на космический сигнал бедствия, чтобы ограбить корабль. На корабле они встречают Юнити, существо коллективного разума. Позже становится ясно, что Юнити — бывшая девушка Рика. Ученый-алкоголик решает вспомнить былые времена и затусить» с Юнити. Саммер устраивает на планете Юнити межрасовую войну, призывая существ стать собой». Джерри и Бетти в пылу ссоры находят в подвале своего дома инопланетянина. После пламенной речи Саммер, Юнити понимает, что Рик плохо на неё влияет и бросает его, после чего Рик засыпает во время неудачной попытки суицида.",
                    Premiere = DateTime.Parse("09.08.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Аутоэротическая_ассимиляция_001.jpg"
                },
                new Episode()
                {
                    Name = "Вспомнить Вэ Сэ Йо",
                    Season = 2,
                    Series = 4,
                    Plot = "В доме Смитов поселяются инопланетные паразиты — быстро размножающиеся существа, принимающие облик самых разных гуманоидов и животных, которые способны воздействовать на разум, заставляя людей верить, что они были в их жизни всегда. Отчаянно пытающегося исправить ситуацию Рика, который стремится найти способ отличить паразитов от реальных членов и друзей семьи, самого обвиняют в том, что он паразит, и предлагают казнить…",
                    Premiere = DateTime.Parse("16.08.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Вспомнить_Вэ_Сэ_Йо_001.jpg"
                },
                new Episode()
                {
                    Name = "Пора Швифтануться",
                    Season = 2,
                    Series = 5,
                    Plot = "К Земле приближается гигантская голова и телепортирует планету в далёкий космос для участия в музыкальном конкурсе. Выбывшие планеты уничтожаются. Рик и Морти выходят на связь с правительством Америки и пытаются придумать достойную песню, чтобы не потерпеть поражение. В это время директор Вагина принимает головы, наблюдающие за ходом конкурса, за божественные существа и, неправильно интерпретируя их эмоции, основывает новую религию, делая вид, что выполняет их волю.",
                    Premiere = DateTime.Parse("23.08.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Пора_Швифтануться_001.jpg"
                },
                new Episode()
                {
                    Name = "Наверное, Рики сошли с ума",
                    Season = 2,
                    Series = 6,
                    Plot = "Рик, Морти и Саммер развлекаются просмотром полнометражного фильма о Яйцелюбах в параллельной вселенной. Собираясь лететь домой, Рик обнаруживает, что в его летающей тарелке не работает генератор энергии. Оказывается, его транспорт питает целая минивселенная, жители которой вырабатывают электричество на специальных тренажёрах. Желая выяснить, в чём дело, Рик берёт с собой Морти и отправляется в минивселенную, оставляя защитную систему летающей тарелки охранять Саммер любой ценой».",
                    Premiere = DateTime.Parse("30.08.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Наверное,_Рики_сошли_с_ума_001.jpg"
                },
                new Episode()
                {
                    Name = "Большой переполох в маленьком Санчезе",
                    Season = 2,
                    Series = 7,
                    Plot = "Джерри и Бетт опять ссорятся. Устав от их бесконечных скандалов, Рик отвозит их в инопланетный центр психологической помощи семейным парам. При помощи новейших технологий психологи создают живых существ, соответствующих представлениям супругов друг о друге, для большей наглядности. Однако когда дело доходит до Джерри с женой, их существа сбегают, сея панику и хаос. В это время Рик переселяет свой разум в тело своего клона-подростка, чтобы помочь Саммер и Морти убить заведшегося в школе вампира, но так увлекается, что не хочет возвращаться в старое тело…",
                    Premiere = DateTime.Parse("13.09.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Большой_переполох_в_маленьком_Санчезе_001.jpg"
                },
                new Episode()
                {
                    Name = "Межвселенский Кабель 2: Искушение Судьбы",
                    Season = 2,
                    Series = 8,
                    Plot = "Джерри подхватывает инопланетный вирус по вине Рика, и его доставляют в инопланетную больницу, где представлены лучшие врачи Галактики. Вылечив Джерри, инопланетяне предлагают ему помочь спасти жизнь политическому деятелю Шримпли Пиблзу, борющемуся за мир. Вместо сердца Пиблзу планируется пересадить пенис Джерри. Между тем Рик, Морти и Саммер коротают время в зале ожидания просмотром телепередач из параллельных вселенных.",
                    Premiere = DateTime.Parse("20.09.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Межвселенский_Кабель_2_Искушение_Судьбы_001.jpg"
                },
                new Episode()
                {
                    Name = "Судная Ночь",
                    Season = 2,
                    Series = 9,
                    Plot = "Рик и Морти летят в космосе. Какое-то существо вмазывается в лобовое стекло. Они высаживаются на одной планете, чтобы убрать труп на лобовом стекле. Но после заката начинается самое интересное — судная ночь.",
                    Premiere = DateTime.Parse("27.09.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Судная_Ночь_001.jpg"
                },
                new Episode()
                {
                    Name = "Свадебные Сквончеры",
                    Season = 2,
                    Series = 10,
                    Plot = "Рик с семьей летит на свадьбу Птичьей Личности и Тэмми. Прилетев на планету Сквонч» Рик узнает, что Тэмми не та, за кого себя выдает…",
                    Premiere = DateTime.Parse("04.10.2015"),
                    ImageName = "http://173.249.20.184:7001/images/Свадебные_Сквончеры_001.jpg"
                }
            };

            return episodes;
        }

        private static async Task<List<EpisodeCharacters>> GetCharactersInEpisode(IRickAndMortyContext context)
        {
            var episodes = await context.Episodes
                .Where(e => e.IsDelete == false)
                .AsNoTracking()
                .ToListAsync();

            var characters = await context.Characters
                .Where(e => e.IsDelete == false)
                .AsNoTracking()
                .ToListAsync();

            var episodeCharacters = new List<EpisodeCharacters>()
            {
                new EpisodeCharacters()
                { 
                    EpisodeId = episodes.Where(e => e.Season == 1 && e.Series == 1).Select(e => e.Id).FirstOrDefault(),
                    CharacterId = characters.Where(c => c.FirstName == "Санчез" && c.LastName == "").Select(c => c.Id).FirstOrDefault()
                }
            };

            return episodeCharacters;
        }
    }
}
